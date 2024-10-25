using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurfaceDetector : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Collider> objectsInCloseProximity;
    public PlayerInputActions _playerInputActions;
    public LineRenderer _lineRenderer;
    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerInputActions = GameManager.PlayerInputs;
        _playerInputActions.Player_Grounded.ShootHooks.started += ShootHooks_performed;
    }

    private void Update() {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + Camera.main.transform.forward * 25f);
    }
    private void ShootHooks_performed(InputAction.CallbackContext callbackContext) {
        Debug.Log("Shooting hooks: " + callbackContext);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ODM_Attachable")) {
            objectsInCloseProximity.Add(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ODM_Attachable")) {
            objectsInCloseProximity.Remove(other);
        }
    }
}
