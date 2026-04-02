using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private float _moveSpeed = 5f;

    private PlayerInputManager _inputManager;
    private Rigidbody _rigidBody;

    private void Awake() {
        _inputManager = GetComponent<PlayerInputManager>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // x, z축 움직임
        Vector3 move = Vector3.right * _inputManager. Horizontal +
                       Vector3.forward * _inputManager.Vertical;

        // 대각선 움직임 조정
        move.Normalize();

        // move 기반 새 포지션 계산
        Vector3 newPosition = transform.position + (move * _moveSpeed * Time.deltaTime);

        // 이동
        _rigidBody.MovePosition(newPosition);
    }
}
