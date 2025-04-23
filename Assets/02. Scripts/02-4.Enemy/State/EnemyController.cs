using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("State System")]
    private IEnemyState _idleState, _traceState, _returnState, _attackState, _damagedState, _dieState;
    public IEnemyState IdleState => _idleState;
    public IEnemyState TraceState => _traceState;
    public IEnemyState ReturnState => _returnState;
    public IEnemyState AttackState => _attackState;
    public IEnemyState DamagedState => _damagedState;
    public IEnemyState DieState => _dieState;
    private EnemyStateContext _enemyStateContext;
    public EnemyStateContext EnemyStateContext => _enemyStateContext;

    [Header("Components")]
    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;
    private EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;
    private GameObject _player;
    public GameObject Player => _player;
    private void Awake()
    {
        _enemyStateContext = new EnemyStateContext(this);
        _characterController = GetComponent<CharacterController>();
        _enemyData = GetComponent<EnemyData>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
        _enemyStateContext.ChangeState(_idleState);

        _enemyData.StartPosition = transform.position;
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
} 