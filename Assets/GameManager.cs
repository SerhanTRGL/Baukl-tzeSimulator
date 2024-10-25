using UnityEngine;

public class GameManager : MonoBehaviour {
    private static PlayerInputActions _playerInputActions;
    public static PlayerInputActions PlayerInputs { get { return _playerInputActions; } }

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player_Grounded.Enable();
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
    }

}
