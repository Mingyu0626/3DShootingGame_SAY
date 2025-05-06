using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Events")]
    public Action <float> PlayerStaminaChanged;
    public Action<float> PlayerHealthPointChanged;
    public Action <int, int> PlayerBombCountChanged;
    public Action <int, int> PlayerBulletCountChanged;
    public Action <float> PlayerReloadGaugeChanged;


    [Header("Basic")]
    private float _gravity = -9.81f;
    public float Gravity => _gravity;

    private float _yVelocity = 0f;
    public float YVelocity { get => _yVelocity; set => _yVelocity = value; }


    [Header("Stat(Stamina & HP)")]
    [SerializeField]
    private float _currentStamina = 100f;
    public float CurrentStamina
    {
        get => _currentStamina;
        set
        {
            _currentStamina = Mathf.Clamp(value, _minStamina, _maxStamina);
            PlayerStaminaChanged?.Invoke(_currentStamina);
        }
    }

    private float _minStamina = 0f;
    public float MinStamina { get => _minStamina; set => _minStamina = value; }

    private float _maxStamina = 100f;
    public float MaxStamina { get => _maxStamina; set => _maxStamina = value; }

    private const float _maxHealthPoint = 100f;
    public float MaxHealthPoint { get => _maxHealthPoint; }

    private float _currentHealthPoint;
    public float CurrentHealthPoint 
    { 
        get => _currentHealthPoint; 
        set
        {
            _currentHealthPoint = Mathf.Clamp(value, 0f, _maxHealthPoint);
            PlayerHealthPointChanged?.Invoke(_currentHealthPoint);
        }
    }


    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [SerializeField]
    private float _moveSpeedMultiplier;
    public float MoveSpeedMultiplier { get => _moveSpeedMultiplier; set => _moveSpeedMultiplier = value; }


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
    public float RollingPower { get => _rollingPower; set => _rollingPower = value; }

    [SerializeField]
    private float _staminaCostForRolling = 20f;
    public float StaminaCostForRolling { get => _staminaCostForRolling; set => _staminaCostForRolling = value; }

    [SerializeField]
    private float _rollDuration = 0.1f;
    public float RollDuration { get => _rollDuration; set => _rollDuration = value; }


    [Header("Climb")]
    [SerializeField]
    private float _staminaCostForClimbing;
    public float StaminaCostForClimbing { get => _staminaCostForClimbing; set => _staminaCostForClimbing = value; }

    private bool _isClimbing = false;
    public bool IsClimbing { get => _isClimbing; set => _isClimbing = value; }


    [Header("Gun Attack")]
    private int _maxBulletCount = 50;
    public int MaxBulletCount { get => _maxBulletCount; set => _maxBulletCount = value; }

    private int _currentBulletCount = 50;
    public int CurrentBulletCount
    {
        get => _currentBulletCount;
        set
        {
            _currentBulletCount = value;
            PlayerBulletCountChanged?.Invoke(_currentBulletCount, _maxBulletCount);
        }
    }
    [SerializeField]
    private ParticleSystem _bulletVFX;
    public ParticleSystem BulletVFX { get => _bulletVFX; set => _bulletVFX = value; }

    [SerializeField]
    private TrailRenderer _bulletTrailPrefab;
    public TrailRenderer BulletTrailPrefab { get => _bulletTrailPrefab; set => _bulletTrailPrefab = value; }
    [SerializeField]
    private float _tracerSpeed = 200f;
    public float TracerSpeed { get => _tracerSpeed; set => _tracerSpeed = value; }

    [SerializeField]
    private GameObject _shootPosition;
    public GameObject ShootPosition { get => _shootPosition; set => _shootPosition = value; }

    private bool _isShooting = false;
    public bool IsShooting { get => _isShooting; set => _isShooting = value; }

    [SerializeField]
    private float _shootingInterval = 0.2f;
    public float ShootingInterval { get => _shootingInterval; set => _shootingInterval = value; }

    [SerializeField]
    private float _continuousShootingCooldown = 5f;
    public float ContinuousShootingCooldown { get => _continuousShootingCooldown; set => _continuousShootingCooldown = value; }


    [Header("Bomb Attack")]
    [SerializeField]
    private GameObject _bombPrefab;
    public GameObject BombPrefab { get => _bombPrefab; set => _bombPrefab = value; }

    private int _maxBombCount = 3;
    public int MaxBombCount { get => _maxBombCount; set => _maxBombCount = value; }

    private int _currentBombCount = 3;
    public int CurrentBombCount 
    { 
        get => _currentBombCount;
        set
        {
            _currentBombCount = value;
            PlayerBombCountChanged?.Invoke(_currentBombCount, _maxBombCount);
        }
    }

    private float _bombHoldStartTime;
    public float BombHoldStartTime { get => _bombHoldStartTime; set => _bombHoldStartTime = value; }




    [SerializeField]
    private float _minBombPower = 10f;
    public float MinBombPower { get => _minBombPower; set => _minBombPower = value; }

    [SerializeField]
    private float _maxBombPower = 40f;
    public float MaxBombPower { get => _maxBombPower; set => _maxBombPower = value; }

    [SerializeField]
    private float _maxHoldTime = 2f;
    public float MaxHoldTime { get => _maxHoldTime; set => _maxHoldTime = value; }


    [Header("Reload")]
    [SerializeField]
    private float _reloadDuration = 2f;
    public float ReloadDuration { get => _reloadDuration; set => _reloadDuration = value; }


    [Header("VFX")]
    [SerializeField]
    private GameObject _muzzleEffect; 
    public GameObject MuzzleEffect { get => _muzzleEffect; set => _muzzleEffect = value; }

    [SerializeField]
    private GameObject _bladeEffect;
    public GameObject BladeEffect { get => _bladeEffect; set => _bladeEffect = value; }

    [SerializeField]
    private GameObject _bombEffect;
    public GameObject BombEffect { get => _bombEffect; set => _bombEffect = value; }

    public void Init()
    {
        _currentStamina = _maxStamina;
        _currentHealthPoint = _maxHealthPoint;
        _currentBulletCount = _maxBulletCount;
        _currentBombCount = _maxBombCount;
    }
}
