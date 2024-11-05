using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ODMGear : MonoBehaviour {
    public enum SurfaceDetectionMode {
        RayCast,
        CapsuleCast
    }
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private Rigidbody _playerRb;


    [SerializeField] private Camera _mainCamera;

    [SerializeField] private float _maxCastDistanceFromPlayer = 25f;
    [SerializeField] private float _minCastDistanceFromPlayer = 2f;
    [SerializeField] private SurfaceDetectionMode _currentSurfaceDetectionMode = SurfaceDetectionMode.CapsuleCast;


    [SerializeField][Range(0, 1)] private float _angleLimit = 0.5f;

    [SerializeField] private Vector3 _odmGearPullDirection = Vector3.zero;


    [SerializeField] private ODMGearHook _leftHook;
    [SerializeField] private ODMGearHook _rightHook;
    [SerializeField] private int _castIterations = 5;
    [SerializeField] private float _capsuleCastRadius = 0.5f; //For capsulecast
    [SerializeField] private float _hookMoveSpeed = 2f;
    [SerializeField] private float _hookForceMultiplier = 1f;
    [SerializeField] private float _maxVelocity = 120f;

    #region properties

    public float HookMoveSpeed { get { return _hookMoveSpeed; } }
    public float HookForceMultiplier { get { return _hookForceMultiplier; } }
    public float MaxVelocity { get { return _maxVelocity; } }
    public int CastIterations { get { return _castIterations; } }
    public SurfaceDetectionMode CurrentSurfaceDetectionMode {
        get { return _currentSurfaceDetectionMode; }
    }
    public float AngleLimit { get { return _angleLimit; } }
    public float CapsuleCastRadius { get { return _capsuleCastRadius; } }
    public float MaxCastDistanceFromPlayer { get { return _maxCastDistanceFromPlayer; } }
    public float MinCastDistanceFromPlayer { get { return _minCastDistanceFromPlayer; } }
    #endregion
    private void Start() {
        _playerInputActions = GameManager.PlayerInputs;
        _playerRb = GetComponentInParent<Rigidbody>();
        _mainCamera = Camera.main;

        _playerInputActions.Player_MidAir.ShootHooks.canceled += ShootHooks_canceled;
        _playerInputActions.Player_Grounded.ShootHooks.canceled += ShootHooks_canceled;

        _leftHook.ODMGear = this;
        _rightHook.ODMGear = this;

    }

    private void ShootHooks_canceled(InputAction.CallbackContext obj) {
        _odmGearPullDirection = Vector3.zero;
    }

    public void ApplyHookForce() {
        _leftHook.CalculateHookForce();
        Vector3 leftHookForce = _leftHook.HookForce;

        _rightHook.CalculateHookForce();
        Vector3 rightHookForce = _rightHook.HookForce;

        Vector3 totalForceToApply = leftHookForce + rightHookForce;

        //Temporary!!! -- Rotation logic  when force is applied
        _odmGearPullDirection = totalForceToApply.normalized;
        if (_odmGearPullDirection.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(_odmGearPullDirection);

            float rotationSpeed = 10f;
            _playerRb.MoveRotation(Quaternion.Slerp(_playerRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
        if (_playerRb.linearVelocity.magnitude < _maxVelocity) {
            _playerRb.AddForce(totalForceToApply);
        }
    }
    void FixedUpdate() {
        if (GameManager.PlayerInputs.Player_Grounded.ShootHooks.IsPressed()) {
            Debug.Log("ShootHooks is pressed");
        };
        //Debug.Log("Player velocity magnitude: " + _playerRb.linearVelocity.magnitude); //120 velocity magnitude limit seems sensible
        ApplyHookForce();

        // Check if both attachments are inactive to reset x and z rotation
        if (!_leftHook.IsActivelyFiringHook && !_rightHook.IsActivelyFiringHook) {
            // Only reset x and z axes, keep y-axis as is
            Quaternion targetRotation = Quaternion.Euler(0, _playerRb.rotation.eulerAngles.y, 0);

            float rotationSpeed = 10f;
            _playerRb.MoveRotation(Quaternion.Slerp(_playerRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            switch (_currentSurfaceDetectionMode) {
                case SurfaceDetectionMode.RayCast:
                    ODMGear_DrawRayCasts();
                    break;
                case SurfaceDetectionMode.CapsuleCast:
                    ODMGear_DrawCapsuleCasts();
                    break;
            }
        }
    }

    private void ODMGear_DrawRayCasts() {
        Gizmos.color = Color.red;
        Vector3 playerPos = transform.position;
        Vector3 forwardDir = _mainCamera.transform.forward.normalized;
        Vector3 rightDir = _mainCamera.transform.right.normalized;
        Gizmos.DrawLine(playerPos, playerPos + forwardDir * _maxCastDistanceFromPlayer);
        Gizmos.DrawLine(playerPos, playerPos + rightDir * _maxCastDistanceFromPlayer);
        Gizmos.DrawLine(playerPos, playerPos + -rightDir * _maxCastDistanceFromPlayer);

        if (_castIterations > 0) {
            for (float i = 1f / (float)_castIterations; i < _angleLimit; i += 1f / (float)_castIterations) {
                float powered = Mathf.Pow(1 - i, 5);
                Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;

                // Draw right-side ray
                Gizmos.color = new Color(powered, powered, powered);
                if (_rightHook.HookHitIndex != 0 && i == _rightHook.HookHitIndex) {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(playerPos, playerPos + interVectorR * _maxCastDistanceFromPlayer);

                // Draw left-side ray
                Gizmos.color = new Color(powered, powered, powered);
                if (_leftHook.HookHitIndex != 0 && i == _leftHook.HookHitIndex) {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(playerPos, playerPos + interVectorL * _maxCastDistanceFromPlayer);
            }
        }
    }
    private void ODMGear_DrawCapsuleCasts() {
        Gizmos.color = Color.red;
        Vector3 playerPos = transform.position;
        Vector3 forwardDir = _mainCamera.transform.forward.normalized;
        Vector3 rightDir = _mainCamera.transform.right.normalized;
        Gizmos.DrawLine(playerPos, playerPos + forwardDir * _maxCastDistanceFromPlayer);
        Gizmos.DrawLine(playerPos, playerPos + rightDir * _maxCastDistanceFromPlayer);
        Gizmos.DrawLine(playerPos, playerPos + -rightDir * _maxCastDistanceFromPlayer);

        if (_castIterations > 0) {
            for (float i = 1f / (float)_castIterations; i < _angleLimit; i += 1f / (float)_castIterations) {
                float powered = Mathf.Pow(1 - i, 5);
                Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;

                // Draw right-side capsule
                Gizmos.color = new Color(powered, powered, powered);
                if (_rightHook.HookHitIndex != 0 && i == _rightHook.HookHitIndex) {
                    Gizmos.color = Color.red;
                }
                GizmosExtensions.DrawCapsule(playerPos, interVectorR, _maxCastDistanceFromPlayer, _capsuleCastRadius);

                // Draw left-side capsule
                Gizmos.color = new Color(powered, powered, powered);
                if (_leftHook.HookHitIndex != 0 && i == _leftHook.HookHitIndex) {
                    Gizmos.color = Color.red;
                }
                GizmosExtensions.DrawCapsule(playerPos, interVectorL, _maxCastDistanceFromPlayer, _capsuleCastRadius);
            }
        }
    }
}

