using UnityEngine;
class EnemyAttack : MonoBehaviour {
    private float _attackDamage;
    private float _attackInterval;

    private float _lastAttackTime;

    public void Init(EnemyData data) {
        _attackDamage = data.Damage;
        _attackInterval = data.AttackInterval;

        _lastAttackTime = float.MinValue;
    }

    public void Attack(GameObject target) {
        if (Time.time < _lastAttackTime + _attackInterval) { return; }
        _lastAttackTime =Time.time;

        var damageAble = target.GetComponent<IDamageable>();
        if (damageAble == null) {
            return;
        } else {
            damageAble.GetDamaged(_attackDamage, transform.position, -transform.position);
        }
    }
}