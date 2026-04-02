using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable {
    private float _maxHealth;
    private bool _isDead;
    public float Health { get;  private set; }
    public bool IsDead => Health <= 0;

    public UnityEvent OnDead;

    [SerializeField]
    private Image _healthBarImage;

    [SerializeField]
    private GameObject _healthBarRoot;

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
        Health = Health < 0 ? 0 : Health;

        if (Health <= 0 && !_isDead) {
            _isDead = true;
            Die();
        }

        UpdateHelathBar();
    }

    public virtual void Die() {
        OnDead?.Invoke();

        // 체력바 비활성화
        _healthBarRoot.SetActive(false);
    }

    private void UpdateHelathBar() {
        var rectTransform = _healthBarImage.GetComponent<RectTransform>();
        float ratio = Health / _maxHealth;
        rectTransform.sizeDelta = new Vector2(1.4f * ratio, rectTransform.sizeDelta.y);
    }
}