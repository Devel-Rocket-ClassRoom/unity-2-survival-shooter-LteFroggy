using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : LivingEntity {
    private readonly int DieHash = Animator.StringToHash("Die");

    private float _deadBodyExistTime = 5.0f;

    private ParticleSystem _enemyHitParticle;

    private AudioSource _audioSource;

    private AudioClip _hitClip;
    private AudioClip _deadClip;
    
    private Animator _animator;

    public void Init(EnemyData enemyData) {
        base.Init(enemyData.Health);

        _enemyHitParticle = GetComponentInChildren<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        // Controller쪽에 볼륨 값 변경 이벤트 추가
        GetComponent<EnemyController>().OnVolumeChanged.AddListener(ChangeVolume);

        _hitClip = enemyData.HitClip;
        _deadClip = enemyData.DeadClip;
        OnDead.AddListener(() => GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>().AddScore(enemyData.Score));
    }
        
    public override void GetDamaged(float amount, Vector3 point, Vector3 normal) {
        // 데미지 처리
        base.GetDamaged(amount, point, normal);
        
        // 피격 파티클 재생
        _enemyHitParticle.transform.position = point;
        _enemyHitParticle.transform.forward = normal;
        _enemyHitParticle.Play();

        // 피격 소리 재생
        if (!IsDead) {
            _audioSource.PlayOneShot(_hitClip);
        }
    }

    public override void Die() {
        base.Die();

        // 사망 애니메이션 재생
        _animator.SetTrigger(DieHash);

        // 사망 오디오 재생
        _audioSource.PlayOneShot(_deadClip);
        
        // 일정 시간 후에 시체 제거
        StartCoroutine(CoDie());
    }

    private IEnumerator CoDie() {
        yield return new WaitForSeconds(_deadBodyExistTime);

        Destroy(gameObject);
    }

    private void ChangeVolume(float value) {
        _audioSource.volume = value;
    }
}
