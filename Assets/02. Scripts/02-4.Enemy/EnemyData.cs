using UnityEngine;
using System.Collections.Generic;

public class EnemyData : MonoBehaviour 
{
    [SerializeField]
    private EEnemyType _enemyType;
    public EEnemyType EnemyType { get => _enemyType; set => _enemyType = value; }

    [Header("Movement")]
    [SerializeField] 
    private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [Header("Idle & Trace State")]
    [SerializeField] 
    private float _findDistance = 7f;
    public float FindDistance { get => _findDistance; set => _findDistance = value; }

    [SerializeField] 
    private float _idleToPatrolWaitTime = 5f;
    public float IdleToPatrolWaitTime => _idleToPatrolWaitTime;

    [Header("Return State")]
    [SerializeField] 
    private float _returnDistance = 5f;
    public float ReturnDistance { get => _returnDistance; set => _returnDistance = value; }

    [Header("Attack State")]
    [SerializeField]
    private float _attackDistance = 2.5f;
    public float AttackDistance { get => _attackDistance; set => _attackDistance = value; }

    [SerializeField]
    private float _attackDamage = 1f;
    public float AttackDamage { get => _attackDamage; set => _attackDamage = value; }

    [SerializeField]
    private float _attackRange = 1.5f;
    public float AttackRange { get => _attackRange; set => _attackRange = value; }

    [SerializeField] 
    private float _attackCoolTime = 3f;
    public float AttackCoolTime { get => _attackCoolTime; set => _attackCoolTime = value; }

    [Header("Damaged State")]
    [SerializeField] 
    private float _damagedTime = 0.5f;
    public float DamagedTime { get => _damagedTime; set => _damagedTime = value; }

    [SerializeField] 
    private float _knockbackDistance = 2f;
    public float KnockbackDistance { get => _knockbackDistance; set => _knockbackDistance = value; }

    [SerializeField] 
    private float _knockbackSpeed = 10f;
    public float KnockbackSpeed { get => _knockbackSpeed; set => _knockbackSpeed = value; }

    [Header("Health System")]
    [SerializeField] 
    private int _maxHealthPoint;
    public int MaxHealthPoint { get => _maxHealthPoint; set => _maxHealthPoint = value; }

    private int _currentHealthPoint;
    public int CurrentHealthPoint 
    {
        get => _currentHealthPoint; 
        set => _currentHealthPoint = value; 
    }

    [Header("Patrol State")]
    [SerializeField] 
    private float _patrolRadius = 3f;
    public float PatrolRadius => _patrolRadius;

    [SerializeField] 
    private float _patrolWaitTime = 2f;
    public float PatrolWaitTime => _patrolWaitTime;

    private List<Vector3> _patrolPoints = new List<Vector3>();
    public List<Vector3> PatrolPoints { get => _patrolPoints; set => _patrolPoints = value; }

    [Header("VFX")]
    [SerializeField]
    private GameObject _hitEffect;
    public GameObject HitEffect { get => _hitEffect; set => _hitEffect = value; }

    [SerializeField]
    private GameObject _bloodEffect;
    public GameObject BloodEffect { get => _bloodEffect; set => _bloodEffect = value; }

    [SerializeField]
    private GameObject _explosionEffect;
    public GameObject ExplosionEffect { get => _explosionEffect; set => _explosionEffect = value; }

    // [Header("SFX")]

    private void Awake()
    {
        _currentHealthPoint = _maxHealthPoint;
    }
}
