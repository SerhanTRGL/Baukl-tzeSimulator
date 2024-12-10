using UnityEngine;

public class DestructibleEnvironment : MonoBehaviour {
    [SerializeField] private GameObject _originalObject;
    [SerializeField] private GameObject _fracturedObject;
    [SerializeField] private bool _isFractured = false;
    void Start() {
        _originalObject.SetActive(true);
        _fracturedObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision collision) {
        if (!_isFractured) {
            _isFractured = true;
            _originalObject.SetActive(false);
            Destroy(GetComponent<BoxCollider2D>());
            _fracturedObject.SetActive(true);
        }
    }

}
