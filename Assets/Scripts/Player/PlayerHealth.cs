using UnityEngine;

class PlayerHelath : LivingEntity {
    private readonly int DieHash = Animator.StringToHash("Die");

    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _hitClip;
    [SerializeField]
    private AudioClip _deadClip;
    
    private void Awake() {
        base.Init();

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void GetDamaged(float amount, Vector3 point, Vector3 normal) {
        base.GetDamaged(amount, point, normal);

        Debug.Log($"플레이어 히트, 남은 체력 : {Health}");
        
        // 히트 사운드
        _audioSource.PlayOneShot(_hitClip);
    }

    public override void Die() {
        base.Die();

        // 사망 사운드
        _audioSource.PlayOneShot(_deadClip);

        // 사망 애니메이션
        _animator.SetTrigger(DieHash);

        // 각종 스크립트 비활성화
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerRotator>().enabled = false;
    }

}