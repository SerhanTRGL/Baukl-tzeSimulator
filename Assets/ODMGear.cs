using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ODMGear : MonoBehaviour {
    public enum SurfaceDetectionMode {
        RayCastAndSphereCast,
        CapsuleCast
    }
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Transform _leftHookShootPoint;
    [SerializeField] private Transform _rightHookShootPoint;

    [SerializeField] private Vector3 _leftAnchorPoint;
    [SerializeField] private Vector3 _rightAnchorPoint;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private int _rayIterations = 5;
    [SerializeField] private float _maxRayDistanceFromPlayer = 25f;
    [SerializeField] private float _minRayDistanceFromPlayer = 2f;
    [SerializeField] private SurfaceDetectionMode _currentSurfaceDetectionMode = SurfaceDetectionMode.CapsuleCast;
    [SerializeField] private float _detectRadius = 0.5f;

    [SerializeField][Range(0, 1)] private float _angleLimit = 0.5f;

    [SerializeField] private GameObject _potentialLeftHookAttachObject;
    [SerializeField] private GameObject _potentialRightHookAttachObject;

    [SerializeField] private bool _isLeftHookAttached = false;
    [SerializeField] private bool _isRightHookAttached = false;

    [SerializeField] private bool _isLeftAttachmentOngoing = false;
    [SerializeField] private bool _isRightAttachmentOngoing = false;

    [SerializeField] private float _leftCapsuleWithHit = 0f;
    [SerializeField] private float _rightCapsuleWithHit = 0f;

    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [SerializeField] private float _hookMoveSpeed = 2f;

    [SerializeField] private float _hookForceMultiplier = 1f;

    [SerializeField] private Vector3 _odmGearPullDirection = Vector3.zero;
    void Start() {
        _playerInputActions = GameManager.PlayerInputs;
        _playerRb = GetComponentInParent<Rigidbody>();
        _mainCamera = Camera.main;

        _playerInputActions.Player_Grounded.ShootHooks.started += ShootHooks_started;
        _playerInputActions.Player_MidAir.ShootHooks.started += ShootHooks_started;
        _playerInputActions.Player_MidAir.ShootHooks.canceled += ShootHooks_canceled;
        _playerInputActions.Player_Grounded.ShootHooks.canceled += ShootHooks_canceled;

        _leftAnchorPoint = _leftHookShootPoint.position;
        _rightAnchorPoint = _rightHookShootPoint.position;

        _leftLineRenderer.SetPosition(1, _leftAnchorPoint);
        _rightLineRenderer.SetPosition(1, _rightAnchorPoint);
    }

    private void ShootHooks_canceled(InputAction.CallbackContext obj) {
        _isLeftHookAttached = false;
        _isRightHookAttached = false;

        _isLeftAttachmentOngoing = false;
        _isRightAttachmentOngoing = false;

        _odmGearPullDirection = Vector3.zero;

    }

    private void ShootHooks_started(InputAction.CallbackContext callbackContext) {
        FindHookPlacements(out _rightAnchorPoint, out _leftAnchorPoint);
    }

    private void Update() {
        _leftLineRenderer.SetPosition(0, _leftHookShootPoint.position);
        _rightLineRenderer.SetPosition(0, _rightHookShootPoint.position);
        if (!_isLeftAttachmentOngoing) {
            _leftLineRenderer.SetPosition(1, _leftHookShootPoint.position);
        }
        if (!_isRightAttachmentOngoing) {
            _rightLineRenderer.SetPosition(1, _rightHookShootPoint.position);
        }

        if (_playerInputActions.Player_Grounded.ShootHooks.IsPressed() || _playerInputActions.Player_MidAir.ShootHooks.IsPressed()) {
            MoveHooks();
        }
    }

    private void MoveHooks() {
        if (!_isLeftHookAttached && _isLeftAttachmentOngoing) {
            Vector3 leftHookCurrentPoint = _leftLineRenderer.GetPosition(1);
            // Move the left hook towards the anchor point at a fixed speed
            leftHookCurrentPoint = Vector3.MoveTowards(leftHookCurrentPoint, _leftAnchorPoint, _hookMoveSpeed * Time.deltaTime);

            // Check if the hook has reached the anchor point
            if (Vector3.Distance(leftHookCurrentPoint, _leftAnchorPoint) < 0.01f) {
                leftHookCurrentPoint = _leftAnchorPoint;
                _isLeftHookAttached = true;
            }

            // Update the line renderer's end point
            _leftLineRenderer.SetPosition(1, leftHookCurrentPoint);
        }

        if (!_isRightHookAttached && _isRightAttachmentOngoing) {
            Vector3 rightHookCurrentPoint = _rightLineRenderer.GetPosition(1);
            // Move the right hook towards the anchor point at a fixed speed
            rightHookCurrentPoint = Vector3.MoveTowards(rightHookCurrentPoint, _rightAnchorPoint, _hookMoveSpeed * Time.deltaTime);

            // Check if the hook has reached the anchor point
            if (Vector3.Distance(rightHookCurrentPoint, _rightAnchorPoint) < 0.01f) {
                rightHookCurrentPoint = _rightAnchorPoint;
                _isRightHookAttached = true;
            }

            // Update the line renderer's end point
            _rightLineRenderer.SetPosition(1, rightHookCurrentPoint);
        }
    }

    public void ApplyHookForce() {
        Vector3 totalForceToApply = Vector3.zero;
        if (_isLeftHookAttached && _isRightHookAttached) { //Both hooks are attached to a surface
            Vector3 leftHookForce = _leftLineRenderer.GetPosition(1) - _leftLineRenderer.GetPosition(0);
            Vector3 rightHookForce = _rightLineRenderer.GetPosition(1) - _rightLineRenderer.GetPosition(0);
            totalForceToApply = (leftHookForce + rightHookForce) * _hookForceMultiplier * Time.fixedDeltaTime;
        }
        if (_isLeftHookAttached && !_isRightAttachmentOngoing) { //Left hook is attached, right is not fired at all
            Vector3 leftHookForce = _leftLineRenderer.GetPosition(1) - _leftLineRenderer.GetPosition(0);
            totalForceToApply = leftHookForce * _hookForceMultiplier * Time.fixedDeltaTime;
        }
        if (_isRightHookAttached && !_isLeftAttachmentOngoing) { //Right hook is attached, left is not fired at all
            Vector3 rightHookForce = _rightLineRenderer.GetPosition(1) - _rightLineRenderer.GetPosition(0);
            totalForceToApply = rightHookForce * _hookForceMultiplier * Time.fixedDeltaTime;
        }

        //Temporary!!!
        _odmGearPullDirection = totalForceToApply.normalized;
        if (_odmGearPullDirection.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(_odmGearPullDirection);

            float rotationSpeed = 10f;
            _playerRb.MoveRotation(Quaternion.Slerp(_playerRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        _playerRb.AddForce(totalForceToApply);
    }
    void FixedUpdate() {
        if (GameManager.PlayerInputs.Player_Grounded.ShootHooks.IsPressed()) {
            Debug.Log("ShootHooks is pressed");
        };
        ApplyHookForce();

        // Check if both attachments are inactive to reset x and z rotation
        if (!_isLeftAttachmentOngoing && !_isRightAttachmentOngoing) {
            // Only reset x and z axes, keep y-axis as is
            Quaternion targetRotation = Quaternion.Euler(0, _playerRb.rotation.eulerAngles.y, 0);

            float rotationSpeed = 10f;
            _playerRb.MoveRotation(Quaternion.Slerp(_playerRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    public void FindHookPlacements(out Vector3 leftHookPlacement, out Vector3 rightHookPlacement) {
        Vector3 playerPos = transform.position;
        Vector3 forwardDir = _mainCamera.transform.forward.normalized;
        Vector3 rightDir = _mainCamera.transform.right.normalized;

        leftHookPlacement = _leftHookShootPoint.position;
        rightHookPlacement = _rightHookShootPoint.position;

        if (_currentSurfaceDetectionMode == SurfaceDetectionMode.CapsuleCast) {
            if (_rayIterations > 0) {
                bool rightHitFound = false;
                bool leftHitFound = false;
                for (float i = 1f / (float)_rayIterations; i < _angleLimit; i += 1f / (float)_rayIterations) {
                    if (rightHitFound && leftHitFound) { //Both hooks can be attached
                        break;
                    }

                    if (!rightHitFound) {
                        RaycastHit rightHit;
                        Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                        if (Physics.CapsuleCast(
                            rightHookPlacement - Vector3.up * _detectRadius,
                            rightHookPlacement + Vector3.up * _detectRadius,
                            _detectRadius,
                            interVectorR,
                            out rightHit,
                            _maxRayDistanceFromPlayer,
                            LayerMask.GetMask("ODM_Attachable"))) {
                            if (rightHit.distance > _minRayDistanceFromPlayer) {
                                Debug.Log("Right hit distance: " + rightHit.distance);
                                _potentialRightHookAttachObject = rightHit.collider.gameObject;
                                _rightCapsuleWithHit = i;
                                rightHookPlacement = rightHit.point;
                                _isRightAttachmentOngoing = true;
                                rightHitFound = true;
                            }
                        }
                    }

                    if (!leftHitFound) {
                        RaycastHit leftHit;
                        Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;
                        if (Physics.CapsuleCast(
                            leftHookPlacement - Vector3.up * _detectRadius,
                            leftHookPlacement + Vector3.up * _detectRadius,
                            _detectRadius,
                            interVectorL,
                            out leftHit,
                            _maxRayDistanceFromPlayer,
                            LayerMask.GetMask("ODM_Attachable"))) {
                            if (leftHit.distance > _minRayDistanceFromPlayer) {
                                Debug.Log("Left hit distance: " + leftHit.distance);
                                _potentialLeftHookAttachObject = leftHit.collider.gameObject;
                                _leftCapsuleWithHit = i;
                                leftHookPlacement = leftHit.point;
                                _isLeftAttachmentOngoing = true;
                                leftHitFound = true;
                            }
                        }
                    }
                }
                if (!rightHitFound) {
                    _rightCapsuleWithHit = 0f;
                }
                if (!leftHitFound) {
                    _leftCapsuleWithHit = 0f;
                }
            }
        }
    }

    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            if (_currentSurfaceDetectionMode == SurfaceDetectionMode.RayCastAndSphereCast) {
                Gizmos.color = Color.red;
                Vector3 playerPos = transform.position;
                Vector3 forwardDir = _mainCamera.transform.forward.normalized;
                Vector3 rightDir = _mainCamera.transform.right.normalized;
                Gizmos.DrawLine(playerPos, playerPos + forwardDir * _maxRayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + rightDir * _maxRayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + -rightDir * _maxRayDistanceFromPlayer);


                if (_rayIterations > 0) {
                    for (float i = 1f / (float)_rayIterations; i < _angleLimit; i += 1f / (float)_rayIterations) {
                        float powered = Mathf.Pow(1 - i, 5);
                        Gizmos.color = new Color(powered, powered, powered);
                        Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                        Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;
                        Gizmos.DrawLine(playerPos, playerPos + interVectorR * _maxRayDistanceFromPlayer);
                        Gizmos.DrawLine(playerPos, playerPos + interVectorL * _maxRayDistanceFromPlayer);

                        Gizmos.color = Color.magenta;
                        Gizmos.DrawWireSphere(playerPos + interVectorR * _maxRayDistanceFromPlayer, _detectRadius);
                        Gizmos.DrawWireSphere(playerPos + interVectorL * _maxRayDistanceFromPlayer, _detectRadius);
                    }
                }
            }
            else if (_currentSurfaceDetectionMode == SurfaceDetectionMode.CapsuleCast) {
                Gizmos.color = Color.red;
                Vector3 playerPos = transform.position;
                Vector3 forwardDir = _mainCamera.transform.forward.normalized;
                Vector3 rightDir = _mainCamera.transform.right.normalized;
                Gizmos.DrawLine(playerPos, playerPos + forwardDir * _maxRayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + rightDir * _maxRayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + -rightDir * _maxRayDistanceFromPlayer);

                if (_rayIterations > 0) {
                    for (float i = 1f / (float)_rayIterations; i < _angleLimit; i += 1f / (float)_rayIterations) {
                        float powered = Mathf.Pow(1 - i, 5);

                        Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                        Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;

                        if (_rightCapsuleWithHit != 0 && i <= _rightCapsuleWithHit) {
                            Gizmos.color = new Color(powered, powered, powered);
                            if (i == _rightCapsuleWithHit) {
                                Gizmos.color = Color.red;
                            }
                            DrawCapsule(playerPos, interVectorR, _maxRayDistanceFromPlayer, _detectRadius);
                        }
                        if (_leftCapsuleWithHit != 0 && i <= _leftCapsuleWithHit) {
                            Gizmos.color = new Color(powered, powered, powered);
                            if (i == _leftCapsuleWithHit) {
                                Gizmos.color = Color.red;
                            }
                            DrawCapsule(playerPos, interVectorL, _maxRayDistanceFromPlayer, _detectRadius);
                        }
                    }
                }
            }
        }
    }
    void DrawCapsule(Vector3 position, Vector3 direction, float height, float radius) {
        Vector3 top = position + direction * (height / 2 - radius);  // Top sphere center
        Vector3 bottom = position;  // Bottom sphere center

        // Draw the top and bottom spheres
        Gizmos.DrawWireSphere(top, radius);
        Gizmos.DrawWireSphere(bottom, radius);

        // Draw the connecting cylinder with lines
        Vector3 right = Vector3.Cross(direction, Vector3.up).normalized * radius;
        Vector3 forward = Vector3.Cross(direction, right).normalized * radius;

        Gizmos.DrawLine(top + right, bottom + right);
        Gizmos.DrawLine(top - right, bottom - right);
        Gizmos.DrawLine(top + forward, bottom + forward);
        Gizmos.DrawLine(top - forward, bottom - forward);
    }
}
