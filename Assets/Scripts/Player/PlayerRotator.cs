using UnityEngine;

public class PlayerRotator : MonoBehaviour {
    private PlayerMouseManager _playerMouseManager;

    private void Awake() {
        _playerMouseManager = GetComponent<PlayerMouseManager>();
    }

    private void FixedUpdate() {
        // 마우스 기반으로 위치 찾기
        Vector3 targetPosition = _playerMouseManager.MousePosition;
        // y값은 나와 같도록
        targetPosition.y = transform.position.y;
        // Rotation
        transform.LookAt(targetPosition);
    }
}
