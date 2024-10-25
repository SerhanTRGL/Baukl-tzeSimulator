using UnityEngine;

public class GameManager : MonoBehaviour {
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
    }

}
