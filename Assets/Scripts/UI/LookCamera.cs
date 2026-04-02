using UnityEngine;

public class LookCamera : MonoBehaviour {
    private Camera _mainCamera;

    private void Awake() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        transform.forward = _mainCamera.transform.forward;        
    }
}
