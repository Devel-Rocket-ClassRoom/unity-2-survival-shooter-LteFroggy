using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private readonly int Move = Animator.StringToHash("Move");

    private float _moveSpeed = 5f;

    private PlayerInputManager _inputManager;
    private Animator _animator;
    private Rigidbody _rigidBody;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _inputManager = GetComponent<PlayerInputManager>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // x, z축 움직임
        Vector3 move = Vector3.right * _inputManager. Horizontal +
                       Vector3.forward * _inputManager.Vertical;

        // 대각선 움직임 조정
        move = Vector3.ClampMagnitude(move, 1f);
        
        _animator.SetFloat(Move, move.magnitude);

        // move 기반 새 포지션 계산
        Vector3 newPosition = transform.position + (move * _moveSpeed * Time.deltaTime);

        // 이동
        _rigidBody.MovePosition(newPosition);
    }
}
