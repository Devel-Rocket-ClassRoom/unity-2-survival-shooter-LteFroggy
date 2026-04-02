using UnityEngine;
using UnityEngine.Events;

public abstract class LivingEntity : MonoBehaviour, IDamageable {
    private float _maxHealth;
    private bool _isDead;
    public float Health { get;  private set; }
    public bool IsDead => Health <= 0;

    public UnityEvent OnDead;

    protected virtual void Init() {
        Init(100);
    }

    protected virtual void Init(float maxHealth) {
        _maxHealth = maxHealth;

        Health = _maxHealth;
        _isDead = false;
    }
    public virtual void GetDamaged(float amount, Vector3 point, Vector3 normal) {
        Health -= amount;

        if (Health <= 0 && !_isDead) {
            _isDead = true;
            Die();
        }
    }

    public virtual void Die() {
        OnDead?.Invoke();
    }
}