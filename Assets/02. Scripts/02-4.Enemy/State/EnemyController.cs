using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IEnemyState _idleState, _traceState, _returnState, _attackState, _damagedState, _dieState;
    public IEnemyState IdleState => _idleState;
    public IEnemyState TraceState => _traceState;
    public IEnemyState ReturnState => _returnState;
    public IEnemyState AttackState => _attackState;
    public IEnemyState DamagedState => _damagedState;
    public IEnemyState DieState => _dieState;

    private EnemyStateContext _enemyStateContext;
    public EnemyStateContext EnemyStateContext => _enemyStateContext;

    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    private GameObject _player;
    public GameObject Player => _player;
    private Vector3 _startPosition;
    public Vector3 StartPosition => _startPosition;
    [SerializeField]
    private float _findDistance = 7f;
    public float FindDistance { get => _findDistance; set => _findDistance = value; }

    [SerializeField]
    private float _returnDistance = 5f;
    public float ReturnDistance { get => _returnDistance; set => _returnDistance = value; }

    [SerializeField]
    private float _attackDistance = 2.5f;
    public float AttackDistance { get => _attackDistance; set => _attackDistance = value; }

    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [SerializeField]
    private float _attackCoolTime = 3f;
    public float AttackCoolTime { get => _attackCoolTime; set => _attackCoolTime = value; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterController = GetComponent<CharacterController>();
        _enemyStateContext = new EnemyStateContext(this);
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

        _startPosition = transform.position;
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
} 