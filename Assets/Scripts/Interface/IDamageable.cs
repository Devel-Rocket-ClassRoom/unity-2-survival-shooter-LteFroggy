using UnityEngine;

public interface IDamageable {
    public void GetDamaged(float amount, Vector3 point, Vector3 normal);
}