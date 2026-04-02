using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField]
    private GameObject _gunBarrelEnd;
    [SerializeField]
    private ParticleSystem _muzzleParticle;
    private LineRenderer _bulletTraceRenderer;

    [SerializeField]
    private GunData _gunData;

    [SerializeField]
    private AudioClip _gunShotClip;
    private AudioSource _audioSource;

    private float _lastShotTime = -2f;

    private Coroutine _shotCoroutine;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _bulletTraceRenderer = _gunBarrelEnd.GetComponent<LineRenderer>();
    }

    private void Start() {
        _bulletTraceRenderer.enabled = false;
    }

    public void Fire() {
        // 발사 가능 체크 및 발사
        if (Time.time >= _lastShotTime + _gunData.ShotInterval) {
            _lastShotTime = Time.time;

            if (_shotCoroutine != null) { StopCoroutine(_shotCoroutine); }
            StartCoroutine(CoFire());
        }
    }

    private IEnumerator CoFire() {
        // 총알 발사 소리 재생
        _audioSource.PlayOneShot(_gunShotClip);

        // 레이캐스팅
        Vector3 impactPoint;
        Ray ray = new Ray(_gunBarrelEnd.transform.position, _gunBarrelEnd.transform.forward);
        // 성공 시
        if (Physics.Raycast(ray, out RaycastHit hit, _gunData.MaxDistance)) { 
            impactPoint = hit.point;
            // 상대가 공격 가능 대상이라면, 데미지 주기
            if (hit.collider.TryGetComponent<LivingEntity>(out var damageable) && !damageable.IsDead) {
                damageable.GetDamaged(_gunData.Damage, hit.point, hit.normal);
            }
        } 
        // 실패 시
        else { impactPoint = _gunBarrelEnd.transform.position + _gunBarrelEnd.transform.forward * _gunData.MaxDistance; }

        // 라인렌더러 렌더링
        _bulletTraceRenderer.SetPosition(0, _gunBarrelEnd.transform.position);
        _bulletTraceRenderer.SetPosition(1, impactPoint);
        _bulletTraceRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        // 라인렌더러 렌더링 종료
        _bulletTraceRenderer.enabled = false;
        _shotCoroutine = null;
    }
}
