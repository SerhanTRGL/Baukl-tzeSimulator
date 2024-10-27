using Unity.VisualScripting;
using UnityEngine;

public class ODMGear : MonoBehaviour {
    public enum SurfaceDetectionMode {
        RayCastAndSphereCast,
        CapsuleCast
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform _leftHookShootPoint;
    [SerializeField] private Transform _rightHookShootPoint;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private int _rayIterations = 5;
    [SerializeField] private float _rayDistanceFromPlayer = 25f;
    [SerializeField] private SurfaceDetectionMode _currentSurfaceDetectionMode = SurfaceDetectionMode.CapsuleCast;
    [SerializeField] private float _detectRadius = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    private float _angleLimit = 0.5f;

    [SerializeField]
    private GameObject _potentialLeftHookAttachObject;
    [SerializeField]
    private GameObject _potentialRightHookAttachObject;


    [SerializeField] private float _leftCapsuleWithHit = 0f;
    [SerializeField] private float _rightCapsuleWithHit = 0f;
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    void Start() {
        _mainCamera = Camera.main;
    }

    void FixedUpdate() {
        Vector3 leftHookPlacement;
        Vector3 rightHookPlacement;
        FindHookPlacements(out rightHookPlacement, out leftHookPlacement);

        _leftLineRenderer.SetPosition(0, _leftHookShootPoint.position);
        if (leftHookPlacement != Vector3.zero) {
            _leftLineRenderer.SetPosition(1, leftHookPlacement);
        }
        else {
            _leftLineRenderer.SetPosition(1, _leftHookShootPoint.position);
        }

        _rightLineRenderer.SetPosition(0, _rightHookShootPoint.position);
        if (rightHookPlacement != Vector3.zero) {
            _rightLineRenderer.SetPosition(1, rightHookPlacement);
        }
        else {
            _rightLineRenderer.SetPosition(1, _rightHookShootPoint.position);
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
                            _rayDistanceFromPlayer,
                            LayerMask.GetMask("ODM_Attachable"))) {

                            Debug.Log("Right hit distance: " + rightHit.distance);
                            _potentialRightHookAttachObject = rightHit.collider.gameObject;
                            _rightCapsuleWithHit = i;
                            rightHookPlacement = rightHit.point;
                            rightHitFound = true;
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
                            _rayDistanceFromPlayer,
                            LayerMask.GetMask("ODM_Attachable"))) {

                            Debug.Log("Left hit distance: " + leftHit.distance);
                            _potentialLeftHookAttachObject = leftHit.collider.gameObject;
                            _leftCapsuleWithHit = i;
                            leftHookPlacement = leftHit.point;
                            leftHitFound = true;
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
                Gizmos.DrawLine(playerPos, playerPos + forwardDir * _rayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + rightDir * _rayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + -rightDir * _rayDistanceFromPlayer);


                if (_rayIterations > 0) {
                    for (float i = 1f / (float)_rayIterations; i < _angleLimit; i += 1f / (float)_rayIterations) {
                        float powered = Mathf.Pow(1 - i, 5);
                        Gizmos.color = new Color(powered, powered, powered);
                        Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                        Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;
                        Gizmos.DrawLine(playerPos, playerPos + interVectorR * _rayDistanceFromPlayer);
                        Gizmos.DrawLine(playerPos, playerPos + interVectorL * _rayDistanceFromPlayer);

                        Gizmos.color = Color.magenta;
                        Gizmos.DrawWireSphere(playerPos + interVectorR * _rayDistanceFromPlayer, _detectRadius);
                        Gizmos.DrawWireSphere(playerPos + interVectorL * _rayDistanceFromPlayer, _detectRadius);
                    }
                }
            }
            else if (_currentSurfaceDetectionMode == SurfaceDetectionMode.CapsuleCast) {
                Gizmos.color = Color.red;
                Vector3 playerPos = transform.position;
                Vector3 forwardDir = _mainCamera.transform.forward.normalized;
                Vector3 rightDir = _mainCamera.transform.right.normalized;
                Gizmos.DrawLine(playerPos, playerPos + forwardDir * _rayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + rightDir * _rayDistanceFromPlayer);
                Gizmos.DrawLine(playerPos, playerPos + -rightDir * _rayDistanceFromPlayer);

                if (_rayIterations > 0) {
                    for (float i = 1f / (float)_rayIterations; i < _angleLimit; i += 1f / (float)_rayIterations) {
                        float powered = Mathf.Pow(1 - i, 5);

                        Vector3 interVectorR = Vector3.Lerp(forwardDir, rightDir, i).normalized;
                        Vector3 interVectorL = Vector3.Lerp(forwardDir, -rightDir, i).normalized;

                        Gizmos.color = new Color(powered, powered, powered);
                        if (i == _rightCapsuleWithHit) {
                            Gizmos.color = Color.red;
                        }
                        DrawCapsule(playerPos, interVectorR, _rayDistanceFromPlayer, _detectRadius);
                        Gizmos.color = new Color(powered, powered, powered);
                        if (i == _leftCapsuleWithHit) {
                            Gizmos.color = Color.red;
                        }
                        DrawCapsule(playerPos, interVectorL, _rayDistanceFromPlayer, _detectRadius);
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
