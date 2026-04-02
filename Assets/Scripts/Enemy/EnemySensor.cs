using UnityEngine;

class EnemySensor : MonoBehaviour {
    private float _recognitionRange;
    private float _attackRange;
    private int _playerLayerMask;

    public void Init(EnemyData data) {
        _recognitionRange = data.RecognitionRange;
        _attackRange = data.AttackRange;

        _playerLayerMask = Layers.PlayerMask;
    }

    public Transform FindTracingTarget() {
        // 주변 적 탐색
        Collider[] colliders = Physics.OverlapSphere(transform.position, _recognitionRange, _playerLayerMask);
        if (colliders.Length == 0) { return null; }

        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (var collider in colliders) {
            float dist = (collider.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist) {
                closest = collider.gameObject;
                minDist = dist;
            }
        }

        return closest.transform;
    }
    public bool IsAttackable(Transform target) {
        if (target == null) {
            return false;
        }

        float dist = (target.position - transform.position).sqrMagnitude;
        return dist <= _attackRange * _attackRange;
    }
}