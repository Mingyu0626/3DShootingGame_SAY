using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Action<float> PlayerStaminaChanged;
    public Action<int, int> PlayerBombCountChanged;
    public Action<int, int> PlayerBulletCountChanged;


    [Header("Basic")]
    private float _gravity = -9.81f;
    public float Gravity => _gravity;

    private float _yVelocity = 0f;
    public float YVelocity { get => _yVelocity; set => _yVelocity = value; }

    [Header("Stamina")]
    [SerializeField]
    private float _stamina = 100f;
    private float _staminaMin = 0f;
    private float _staminaMax = 100f;
    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = Mathf.Clamp(value, _staminaMin, _staminaMax);
            PlayerStaminaChanged?.Invoke(_stamina);
        }
    }
    public float StaminaMin { get => _staminaMin; set => _staminaMin = value; }
    public float StaminaMax { get => _staminaMax; set => _staminaMax = value; }

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveSpeedMultiplier;
    public float MoveSpeedMultiplier { get => _moveSpeedMultiplier; set => _moveSpeedMultiplier = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [Header("Run")]
    [SerializeField]
    private float _staminaCostForRun = 20f;
    public float StaminaCostForRun { get => _staminaCostForRun; set => _staminaCostForRun = value; }

    [Header("Jump")]
    [SerializeField]
    private float _jumpPower;
    public float JumpPower { get => _jumpPower; set => _jumpPower = value; }

    [Header("Rolling")]
    [SerializeField]
    private float _rollingPower = 10f;
    [SerializeField]
    private float _staminaCostForRolling = 20f;
    [SerializeField]
    private float _rollDuration = 0.1f;
    public float RollingPower { get => _rollingPower; set => _rollingPower = value; }
    public float StaminaCostForRolling { get => _staminaCostForRolling; set => _staminaCostForRolling = value; }
    public float RollDuration { get => _rollDuration; set => _rollDuration = value; }

    [Header("Climb")]
    [SerializeField]
    private float _staminaCostForClimbing;
    private bool _isClimbing = false;
    public float StaminaCostForClimbing { get => _staminaCostForClimbing; set => _staminaCostForClimbing = value; }
    public bool IsClimbing { get => _isClimbing; set => _isClimbing = value; }

    [Header("Fire(Bullet & Bomb)")]
    private bool _isBulletFiring = false;
    public bool IsBulletFiring { get => _isBulletFiring; set => _isBulletFiring = value; }

    private int _maxBulletCount = 50;
    private int _currentBulletCount = 50;
    public int MaxBulletCount { get => _maxBulletCount; set => _maxBulletCount = value; }
    public int CurrentBulletCount 
    { 
        get => _currentBulletCount;
        set
        {
            _currentBulletCount = value;
            PlayerBulletCountChanged?.Invoke(_currentBulletCount, _maxBulletCount);
        }
    }

    private int _maxBombCount = 3;
    private int _currentBombCount = 3;
    public int MaxBombCount { get => _maxBombCount; set => _maxBombCount = value; }
    public int CurrentBombCount 
    { 
        get => _currentBombCount;
        set
        {
            _currentBombCount = value;
            PlayerBombCountChanged?.Invoke(_currentBombCount, _maxBombCount);
        }
    }
}
