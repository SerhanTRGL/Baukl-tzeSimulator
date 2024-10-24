using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private Rigidbody _playerRb;
    private PlayerInputActions _playerInputActions;
    private PlayerInput _playerInput;
    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _playerRb = GetComponent<Rigidbody>();


        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player_Grounded.Enable();
        _playerInputActions.Player_Grounded.Jump.performed += Jump;
        _playerInputActions.Player_Grounded.Movement.performed += Movement_performed;

        _playerInputActions.UI.Disable();
        _playerInputActions.UI.Submit.performed += Submit;
    }

    private void Update() {
        if (Keyboard.current.tKey.wasPressedThisFrame) {
            _playerInputActions.Disable();
            _playerInputActions.UI.Enable();
            Debug.Log("pressed t");
            _playerInput.SwitchCurrentActionMap("UI");
        }
        if (Keyboard.current.yKey.wasPressedThisFrame) {
            _playerInputActions.Disable();
            _playerInputActions.Player_Grounded.Enable();
            Debug.Log("pressed y");
            _playerInput.SwitchCurrentActionMap("Player_Grounded");
        }
        Debug.Log(_playerInput.currentActionMap);
    }
    private void FixedUpdate() {
        Vector2 inputVector = _playerInputActions.Player_Grounded.Movement.ReadValue<Vector2>();
        float speed = 1f;
        _playerRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    private void Movement_performed(InputAction.CallbackContext context) {
        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 5f;
        _playerRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext callbackContext) {
        Debug.Log(callbackContext);
        if (callbackContext.performed) {
            _playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            Debug.Log("Jump! " + callbackContext.phase);
        }
    }

    public void Submit(InputAction.CallbackContext callbackContext) {
        Debug.Log("Submit " + callbackContext);
    }
}
