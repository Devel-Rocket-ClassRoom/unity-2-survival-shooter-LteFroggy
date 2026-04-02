using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField]
    private EnemyData _enemyData;

    private Transform _target;

    private EnemyHealth _enemyHealth;
    private EnemyMove _enemyMove;
    private EnemySensor _enemySensor;
    private EnemyAttack _enemyAttack;

    private BoxCollider _boxCollider;
    
    private bool _isSinking = false;
    private float _sinkingSpeed = 0.5f;

    private EnemyState _enemyState;

    private void Awake() {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMove = GetComponent<EnemyMove>();
        _enemySensor = GetComponent<EnemySensor>();
        _enemyAttack = GetComponent<EnemyAttack>();

        _boxCollider = GetComponent<BoxCollider>();
        
        Init(_enemyData);

        _enemyHealth.OnDead.AddListener(SetStateToDead);
    }

    public void Init(EnemyData data) {
        _enemyHealth.Init(data);
        _enemyMove.Init(data);
        _enemySensor.Init(data);
        _enemyAttack.Init(data);
    }

    /// 상태머신 변경 시
    public EnemyState EnemyState { 
        get => _enemyState;
        set {
            Debug.Log($"적 상태 변경 : {value}");
            _enemyState = value;
            switch (_enemyState) {
                case EnemyState.Idle:
                    _target = null;
                    _enemyMove.StopFollow();
                    break;

                case EnemyState.Trace:
                    _enemyMove.StartFollow(_target);
                    break;

                case EnemyState.Attack:
                    _enemyMove.StopFollow();
                    break;

                case EnemyState.Dead:
                    _enemyMove.OnDead();
                    _boxCollider.enabled = false;
                    break;
            }
        }
    }
    private void Update() {
        switch (_enemyState) {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Trace:
                UpdateTrace();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
            case EnemyState.Dead:
                UpdateDie();
                break;
        }

        if (_isSinking) {
            transform.position += Vector3.down * _sinkingSpeed * Time.deltaTime;
        }
    }

    private void UpdateIdle() {
        Transform sensingResult = _enemySensor.FindTracingTarget();

        // 주변에 적 생기면 따라가기
        if (sensingResult != null) {
            _target = sensingResult;
            EnemyState = EnemyState.Trace;
            return;
        }
    }

    private void UpdateTrace() {
        // 주변 적 여전히 존재하는지 확인
        Transform sensingResult = _enemySensor.FindTracingTarget();
        // 사라졌다면, 다시 추적 중지
        if (sensingResult == null) {
            EnemyState = EnemyState.Idle;
            return;
        }

        // 공격 범위 내에 들어왔는지 확인 후 상태 변경
        if (_enemySensor.IsAttackable(_target)) {
            EnemyState = EnemyState.Attack;
            return;
        }
    }

    private void UpdateAttack() {
        // 공격 범위 밖으로 나갔는지 확인
        if (!_enemySensor.IsAttackable(_target)) {
            EnemyState = EnemyState.Trace;
            return;
        }

        // 공격
        _enemyAttack.Attack(_target.gameObject);
    }

    private void UpdateDie() {

    }

    // 죽으면 가라앉도록
    public void StartSinking() {
        _isSinking = true;
    }

    public void SetStateToDead() {
        EnemyState = EnemyState.Dead;
    }
}