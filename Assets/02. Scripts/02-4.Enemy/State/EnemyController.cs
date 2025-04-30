using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("State System")]
    private IEnemyState _idleState, _traceState, _returnState, _attackState, _damagedState, _dieState, _patrolState;
    public IEnemyState IdleState => _idleState;
    public IEnemyState TraceState => _traceState;
    public IEnemyState ReturnState => _returnState;
    public IEnemyState AttackState => _attackState;
    public IEnemyState DamagedState => _damagedState;
    public IEnemyState DieState => _dieState;
    public IEnemyState PatrolState => _patrolState;
    private EnemyStateContext _enemyStateContext;
    public EnemyStateContext EnemyStateContext => _enemyStateContext;

    [Header("Components")]
    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;
    private EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;
    private UI_Enemy _uiEnemy;


    [Header("References")]
    private GameObject _player;
    public GameObject Player => _player;
    private Vector3 _startPosition;
    public Vector3 StartPosition => _startPosition;


    [Header("NavMesh")]
    private NavMeshAgent _agent;
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }

    [Header("Animation")]
    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterController = GetComponent<CharacterController>();
        _enemyData = GetComponent<EnemyData>();
        _uiEnemy = GetComponentInChildren<UI_Enemy>();
        _enemyStateContext = new EnemyStateContext(this);

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemyData.MoveSpeed;

        _idleState = new EnemyIdleState(this);
        _traceState = new EnemyTraceState(this);
        _returnState = new EnemyReturnState(this);
        _attackState = new EnemyAttackState(this);
        _damagedState = new EnemyDamagedState(this);
        _dieState = new EnemyDieState(this);
        _patrolState = new EnemyPatrolState(this);

        _animator = GetComponentInChildren<Animator>();

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            if (renderer.material.HasProperty("_Color"))
            {
                _enemyData.MaterialList.Add(renderer.material);
            }
        }
    }

    private void OnEnable()
    {
        _enemyData.CurrentHealthPoint = _enemyData.MaxHealthPoint;
        _uiEnemy.InitEnemyHP(_enemyData.MaxHealthPoint);
        _startPosition = transform.position;
        SetStartState();
    }

    private void Update()
    {
        _enemyStateContext.CurrentState.Update();
    }

    public void StartCoroutineInEnemyState(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopCoroutineInEnemyState(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void TakeDamage(Damage damage)
    {
        _enemyData.CurrentHealthPoint -= damage.Value;
        if (_enemyData.EnemyType == EEnemyType.Elite)
        {
            _uiEnemy.RefreshEnemyHPOnDelay(_enemyData.CurrentHealthPoint);
        }
        else
        {
            _uiEnemy.RefreshEnemyHP(_enemyData.CurrentHealthPoint);
        }
        if (_enemyData.CurrentHealthPoint <= 0)
        {
            _enemyStateContext.ChangeState(_dieState);
            _animator.SetTrigger("Die");
            return;
        }
        else
        {
            _enemyStateContext.ChangeState(_damagedState);
            _animator.SetTrigger("Hit");
            HitEffect();
            BloodEffect();
        }
    }

    private void SetStartState()
    {
        switch (_enemyData.EnemyType)
        {
            case EEnemyType.Normal:
                _enemyStateContext.ChangeState(_idleState);
                break;
            case EEnemyType.AlwaysTrace:
                _enemyStateContext.ChangeState(_traceState);
                break;
            default:
                _enemyStateContext.ChangeState(_idleState);
                break;
        }
    }

    private void HitEffect()
    {
        _enemyData.HitEffect.GetComponent<ParticleSystem>().Play();
    }
    private void BloodEffect()
    {
        _enemyData.BloodEffect.GetComponent<ParticleSystem>().Play();
    }
    public void ExplosionEffect()
    {
        _enemyData.ExplosionEffect.GetComponent<ParticleSystem>().Play();
    }
} 