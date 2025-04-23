using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour 
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [Header("Idle & Trace State")]
    [SerializeField] private float _findDistance = 7f;
    public float FindDistance { get => _findDistance; set => _findDistance = value; }

    [Header("Return State")]
    private Vector3 _startPosition;
    public Vector3 StartPosition { get => _startPosition; set => _startPosition = value; }
    [SerializeField] private float _returnDistance = 5f;
    public float ReturnDistance { get => _returnDistance; set => _returnDistance = value; }

    [Header("Attack State")]
    [SerializeField] private float _attackDistance = 2.5f;
    public float AttackDistance { get => _attackDistance; set => _attackDistance = value; }
    [SerializeField] private float _attackCoolTime = 3f;
    public float AttackCoolTime { get => _attackCoolTime; set => _attackCoolTime = value; }

    [Header("Damaged State")]
    [SerializeField] private float _damagedTime = 0.5f;
    public float DamagedTime { get => _damagedTime; set => _damagedTime = value; }

    [Header("Health System")]
    [SerializeField] private int _maxHealthPoint;
    public int MaxHealthPoint { get => _maxHealthPoint; set => _maxHealthPoint = value; }
    private int _currentHealthPoint;
    public int CurrentHealthPoint { get => _currentHealthPoint; set => _currentHealthPoint = value; }
    
    private void OnEnable()
    {
        _currentHealthPoint = _maxHealthPoint;
    }
}
