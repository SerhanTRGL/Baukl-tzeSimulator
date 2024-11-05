using UnityEngine;
using UnityEngine.InputSystem;
using static ODMGear;

public class ODMGearHook : MonoBehaviour {
    public enum ODMGearHookSide {
        Left,
        Right
    }

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private ODMGearHookSide _hookSide;
    [SerializeField] private Transform _hookShootPoint;
    [SerializeField] private Vector3 _hookAnchorPoint;
    [SerializeField] private GameObject _hookedObject;
    [SerializeField] private bool _isHookAttached = false;
    [SerializeField] private bool _isActivelyFiringHook = false; //_isXAttachmentOngoing from ODMGear
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private Vector3 _hookForce = Vector3.zero;

    [SerializeField] private ODMGear _odmGear;
    [SerializeField] private float _hookHitIndex = 0f;

    public ODMGearHookSide HookSide {
        get {
            return _hookSide;
        }
    }
    public Transform HookShootPoint {
        get {
            return _hookShootPoint;
        }
    }
    public Vector3 HookAnchorPoint {
        get {
            return _hookAnchorPoint;
        }
    }
    public GameObject HookedObject {
        get {
            return _hookedObject;
        }
    }
    public bool IsHookAttached {
        get {
            return _isHookAttached;
        }
    }
    public bool IsActivelyFiringHook {
        get {
            return _isHookAttached;
        }
    } //_isXAttachmentOngoing from ODMGear
    public LineRenderer LineRenderer {
        get {
            return _lineRenderer;
        }
    }
    public Vector3 HookForce {
        get {
            return _hookForce;
        }
    }
    public ODMGear ODMGear {
        get {
            return _odmGear;
        }
        set {
            _odmGear = value;
        }
    }
    public float HookHitIndex {
        get {
            return _hookHitIndex;
        }
    }
    private void Start() {
        _mainCamera = Camera.main;
        _playerInputActions = GameManager.PlayerInputs;

        _playerInputActions.Player_Grounded.ShootHooks.started += ShootHooks_started;
        _playerInputActions.Player_MidAir.ShootHooks.started += ShootHooks_started;

        _playerInputActions.Player_Grounded.ShootHooks.canceled += ShootHooks_canceled;
        _playerInputActions.Player_MidAir.ShootHooks.canceled += ShootHooks_canceled;

        _hookAnchorPoint = _hookShootPoint.position;
        _lineRenderer.SetPosition(1, _hookAnchorPoint);
    }

    private void ShootHooks_canceled(InputAction.CallbackContext obj) {
        _isHookAttached = false;
        _isActivelyFiringHook = false;
    }

    private void ShootHooks_started(InputAction.CallbackContext obj) {
        FindHookPlacement(out _hookAnchorPoint);
    }


    private void Update() {
        _lineRenderer.SetPosition(0, _hookShootPoint.position);
        if (!_isActivelyFiringHook) {
            _lineRenderer.SetPosition(1, _hookShootPoint.position);
        }

        if (_playerInputActions.Player_Grounded.ShootHooks.IsPressed() || _playerInputActions.Player_MidAir.ShootHooks.IsPressed()) {
            MoveHook();
        }
    }

    private void MoveHook() {
        if (!_isHookAttached && _isActivelyFiringHook) {
            Vector3 hookCurrentPoint = _lineRenderer.GetPosition(1);
            //Move hook towards the anchor point at a fixed speed
            hookCurrentPoint = Vector3.MoveTowards(hookCurrentPoint, _hookAnchorPoint, _odmGear.HookMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(hookCurrentPoint, _hookAnchorPoint) < 0.01f) {
                hookCurrentPoint = _hookAnchorPoint;
                _isHookAttached = true;
            }

            _lineRenderer.SetPosition(1, hookCurrentPoint);
        }
    }

    public void CalculateHookForce() {
        _hookForce = Vector3.zero;
        if (_isHookAttached) {
            _hookForce = (_lineRenderer.GetPosition(1) - _lineRenderer.GetPosition(0)) * _odmGear.HookForceMultiplier * Time.fixedDeltaTime;
        }
    }

    public void FindHookPlacement(out Vector3 hookPlacement) {
        Vector3 playerPos = transform.position;
        Vector3 cameraForward = _mainCamera.transform.forward.normalized;
        Vector3 cameraRight = _mainCamera.transform.right.normalized;

        if (_hookSide == ODMGearHookSide.Left) {
            cameraRight *= -1;
        }

        hookPlacement = _hookShootPoint.position;

        if (_odmGear.CastIterations > 0) {
            bool hitFound = false;
            for (float i = 1f / (float)_odmGear.CastIterations; i < _odmGear.AngleLimit; i += 1f / (float)_odmGear.CastIterations) {
                if (hitFound) {
                    break;
                }

                Vector3 interVector = Vector3.Lerp(cameraForward, cameraRight, i).normalized;
                if (_odmGear.CurrentSurfaceDetectionMode == SurfaceDetectionMode.CapsuleCast) {
                    FindHookPlacementCapsuleCast(ref hookPlacement, ref hitFound, i, interVector);
                }
                else if (_odmGear.CurrentSurfaceDetectionMode == SurfaceDetectionMode.RayCast) {
                    FindHookPlacementRayCast(ref hookPlacement, ref hitFound, i, interVector);
                }
            }

            if (!hitFound) {
                _hookHitIndex = 0f;
            }
        }
    }

    private void FindHookPlacementRayCast(ref Vector3 hookPlacement, ref bool hitFound, float i, Vector3 interVector) {
        RaycastHit castHit;
        float maxDistance = _odmGear.MaxCastDistanceFromPlayer;
        int attachableLayerMask = LayerMask.GetMask("ODM_Attachable");
        if (Physics.Raycast(hookPlacement, interVector, out castHit, maxDistance, attachableLayerMask)) {
            if (castHit.distance > _odmGear.MinCastDistanceFromPlayer) {
                _hookedObject = castHit.collider.gameObject;
                _hookHitIndex = i;
                _isActivelyFiringHook = true;

                hitFound = true;
                hookPlacement = castHit.point;
            }
        }
    }

    private void FindHookPlacementCapsuleCast(ref Vector3 hookPlacement, ref bool hitFound, float i, Vector3 interVector) {
        RaycastHit castHit;
        Vector3 capsulePoint1 = hookPlacement - Vector3.up * _odmGear.CapsuleCastRadius;
        Vector3 capsulePoint2 = hookPlacement + Vector3.up * _odmGear.CapsuleCastRadius;
        float capsuleRadius = _odmGear.CapsuleCastRadius;
        float maxDistance = _odmGear.MaxCastDistanceFromPlayer;
        int attachableLayerMask = LayerMask.GetMask("ODM_Attachable");
        if (Physics.CapsuleCast(capsulePoint1, capsulePoint2, capsuleRadius, interVector, out castHit, maxDistance, attachableLayerMask)) {
            if (castHit.distance > _odmGear.MinCastDistanceFromPlayer) {
                _hookedObject = castHit.collider.gameObject;
                _hookHitIndex = i;
                _isActivelyFiringHook = true;

                hitFound = true;
                hookPlacement = castHit.point;
            }
        }
    }
}


