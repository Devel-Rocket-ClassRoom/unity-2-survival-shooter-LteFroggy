using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject {
    public float Health = 100;
    public float Damage = 20;
    public float Speed = 10;
    public float AngularSpeed = 720;
    public float RecognitionRange = 20f;
    public float AttackRange = 1.5f;
    public float AttackInterval = 1.5f;
    public AudioClip HitClip;
    public AudioClip DeadClip;
}
