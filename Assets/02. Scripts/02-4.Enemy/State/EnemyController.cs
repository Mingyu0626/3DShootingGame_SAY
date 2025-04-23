using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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

    [Header("References")]
    private GameObject _player;
    public GameObject Player => _player;
    private Vector3 _startPosition;
    public Vector3 StartPosition => _startPosition;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterController = GetComponent<CharacterController>();
        _enemyData = GetComponent<EnemyData>();
        _enemyStateContext = new EnemyStateContext(this);
    }

    private void OnEnable()
    {
        _enemyData.CurrentHealthPoint = _enemyData.MaxHealthPoint;
    }

    private void Start()
    {
        _idleState = new EnemyIdleState();
        _traceState = new EnemyTraceState();
        _returnState = new EnemyReturnState();
        _attackState = new EnemyAttackState();
        _damagedState = new EnemyDamagedState();
        _dieState = new EnemyDieState();
        _patrolState = new EnemyPatrolState();
        _enemyStateContext.ChangeState(_idleState);

        _startPosition = transform.position;
        GeneratePatrolPoints();
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
        Debug.Log($"Change to DamagedState");
        _enemyStateContext.ChangeState(_damagedState);
    }

    private void GeneratePatrolPoints()
    {
        _enemyData.PatrolPoints.Clear();
        for (int i = 0; i < 3; i++)
        {
            float angle = i * 120f * Mathf.Deg2Rad;
            _enemyData.PatrolPoints.Add(_startPosition + new Vector3(
                Mathf.Cos(angle) * _enemyData.PatrolRadius,
                0f,
                Mathf.Sin(angle) * _enemyData.PatrolRadius
            ));
        }
    }
} 