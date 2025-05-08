using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("State System")]
    private EnemyStateContext _enemyStateContext;
    public EnemyStateContext EnemyStateContext => _enemyStateContext;

    private Dictionary<EEnemyState, IEnemyState> _enemyStateDict = new Dictionary<EEnemyState, IEnemyState>();
    public Dictionary<EEnemyState, IEnemyState> EnemyStateDict { get => _enemyStateDict; set => _enemyStateDict = value; }

    private Vector3 _startPosition;
    public Vector3 StartPosition => _startPosition;


    [Header("Components")]
    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    private EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;

    private UI_Enemy _uiEnemy;

    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }


    [Header("References")]
    private GameObject _player;
    public GameObject Player => _player;


    [Header("NavMesh")]
    private NavMeshAgent _agent;
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }

    private void Awake()
    {
        _enemyStateContext = new EnemyStateContext(this);
        _enemyStateDict = new Dictionary<EEnemyState, IEnemyState>();
        _enemyStateDict.Add(EEnemyState.Idle, new EnemyIdleState(this));
        _enemyStateDict.Add(EEnemyState.Trace, new EnemyTraceState(this));
        _enemyStateDict.Add(EEnemyState.Return, new EnemyReturnState(this));
        _enemyStateDict.Add(EEnemyState.Attack, new EnemyAttackState(this));
        _enemyStateDict.Add(EEnemyState.Damaged, new EnemyDamagedState(this));
        _enemyStateDict.Add(EEnemyState.Die, new EnemyDieState(this));
        _enemyStateDict.Add(EEnemyState.Patrol, new EnemyPatrolState(this));


        _characterController = GetComponent<CharacterController>();
        _enemyData = GetComponent<EnemyData>();
        _uiEnemy = GetComponentInChildren<UI_Enemy>();
        _animator = GetComponentInChildren<Animator>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemyData.MoveSpeed;

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
            _enemyStateContext.ChangeState(_enemyStateDict[EEnemyState.Die]);
            _animator.SetTrigger("Die");
            return;
        }
        else
        {
            _enemyStateContext.ChangeState(_enemyStateDict[EEnemyState.Damaged]);
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
                _enemyStateContext.ChangeState(_enemyStateDict[EEnemyState.Idle]);
                break;
            case EEnemyType.AlwaysTrace:
                _enemyStateContext.ChangeState(_enemyStateDict[EEnemyState.Trace]);
                break;
            default:
                _enemyStateContext.ChangeState(_enemyStateDict[EEnemyState.Idle]);
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