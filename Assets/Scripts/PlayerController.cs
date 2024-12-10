using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    private Rigidbody _playerRb;
    private PlayerInputActions _playerInputActions;

    [SerializeField] private Vector3 _cameraLookDir;
    [SerializeField] private Vector3 _cameraRightDir;
    [SerializeField] private float speed = 25f;

    private void Start() {
        _playerRb = GetComponent<Rigidbody>();

        _playerInputActions = GameManager.PlayerInputs;
        _playerInputActions.Player_Grounded.Jump.performed += Jump;
    }


    private void FixedUpdate() {
        Vector2 inputVector = _playerInputActions.Player_Grounded.Movement.ReadValue<Vector2>();

        _cameraLookDir = Camera.main.transform.forward.normalized;
        _cameraLookDir.y = 0;
        _cameraLookDir.Normalize();

        _cameraRightDir = Camera.main.transform.right;
        _cameraRightDir.y = 0;
        _cameraRightDir.Normalize();

        Vector3 movementDir = (_cameraLookDir * inputVector.y) + (_cameraRightDir * inputVector.x);

        if (movementDir.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(movementDir);

            float rotationSpeed = 10f;
            _playerRb.MoveRotation(Quaternion.Slerp(_playerRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        _playerRb.AddForce(movementDir * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext callbackContext) {
        Debug.Log(callbackContext);
        if (callbackContext.performed) {
            _playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            Debug.Log("Jump! " + callbackContext.phase);
        }
    }
}
