using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
    private readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    private Animator _animator;
 
    private Transform _followTarget;
    private NavMeshAgent _navMeshAgent;

    private float _lastTargetLocationTime = -2f;
    private float _targetRelocationInterval = 0.1f;

    public void Init(EnemyData data) {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.autoBraking = false;

        _navMeshAgent.speed = data.Speed;
        _navMeshAgent.angularSpeed = data.AngularSpeed;
    }

    public void StartFollow(Transform target) {
        _followTarget = target;

        _navMeshAgent.SetDestination(_followTarget.position);
        _navMeshAgent.isStopped = false;

        _animator.SetBool(IsMovingHash, true);
    }

    public void StopFollow() {
        _navMeshAgent.ResetPath();
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0f;

        _animator.SetBool(IsMovingHash, false);
    }

    public void OnDead() {
        StopFollow();
        _navMeshAgent.enabled = false;
    }

    private void Update() {
        if (_navMeshAgent.enabled &&
            _followTarget != null &&
            !_navMeshAgent.isStopped && 
            Time.time >= _lastTargetLocationTime + _targetRelocationInterval)
        { 
            _lastTargetLocationTime = Time.time;
            _navMeshAgent.SetDestination(_followTarget.position);
        }
    }
}
