using UnityEngine;

public class PlayerData : MonoBehaviour
{
    
    private float _gravity = -9.81f; // 중력
    public float Gravity => _gravity;

    private float _yVelocity = 0f; // 중력 가속도
    public float YVelocity { get => _yVelocity; set => _yVelocity = value; }

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveSpeedMultiplier;
    public float MoveSpeedMultiplier { get => _moveSpeedMultiplier; set => _moveSpeedMultiplier = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [Header("Jump")]
    [SerializeField]
    private float _jumpPower;
    public float JumpPower { get => _jumpPower; set => _jumpPower = value; }

    [Header("Climb")]
    private bool _isClimbing = false;
    public bool IsClimbing { get => _isClimbing; set => _isClimbing = value; }

    [Header("Stamina")]
    [SerializeField]
    private float _stamina = 100f;
    private float _staminaMin = 0f;
    private float _staminaMax = 100f;
    [SerializeField]
    private float _staminaChangeVolume = 20f;
    public float Stamina { get => _stamina; set => _stamina = Mathf.Clamp(value, _staminaMin, _staminaMax); }
    public float StaminaChangeVolume { get => _staminaChangeVolume; set => _staminaChangeVolume = value; }
}
