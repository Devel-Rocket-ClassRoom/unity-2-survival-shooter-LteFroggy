using System.Collections;
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
        // 총알 발사 소리
        _audioSource.PlayOneShot(_gunShotClip);
        // 라인렌더러 렌더링
        Vector3 impactPoint = GetImpactPoint(_gunBarrelEnd.transform.position, _gunBarrelEnd.transform.forward);
        _bulletTraceRenderer.SetPosition(0, _gunBarrelEnd.transform.position);
        _bulletTraceRenderer.SetPosition(1, impactPoint);
        _bulletTraceRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        _bulletTraceRenderer.enabled = false;
        _shotCoroutine = null;
    }

    private Vector3 GetImpactPoint(Vector3 start, Vector3 direction) {
        Ray ray = new Ray(start, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, _gunData.MaxDistance)) {
            return hit.point;
        } else {
            return start + (direction * _gunData.MaxDistance);
        }
    }
}
