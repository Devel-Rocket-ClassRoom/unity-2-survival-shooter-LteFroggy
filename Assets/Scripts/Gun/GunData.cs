using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject {
    public float Damage = 20;
    public float ShotInterval = 0.05f;
    public float MaxDistance = 50f;
    public int MagAmmo = 30;
    public int InitialAmmo = 360;
    public int MaximumAmmo = 999;
}
