using UnityEngine;

public class ODMGear : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform _leftHookShootPoint;
    [SerializeField] private Transform _rightHookShootPoint;
    [SerializeField] private Camera _mainCamera;
    void Start() {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {

    }

    public void FindHookPlacements(out Vector3 leftHookPlacement, out Vector3 rightHookPlacement) {
        leftHookPlacement = Vector3.zero;
        rightHookPlacement = Vector3.zero;

    }
    private void OnDrawGizmos() {

    }
}
