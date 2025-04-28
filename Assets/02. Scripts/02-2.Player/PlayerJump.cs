using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool _isSingleJumping = false;
    private bool _isDoubleJumping = false;
    private PlayerData _playerData;
    private CharacterController _characterController;
    private Animator _animator;


    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        CheckIsGround();
        Jump();
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!_isSingleJumping)
            {
                _playerData.YVelocity = _playerData.JumpPower;
                _isSingleJumping = true;
                JumpAnimation();
            }
            else if (!_isDoubleJumping)
            {
                _playerData.YVelocity = _playerData.JumpPower;
                _isDoubleJumping = true;
                JumpAnimation();
            }
        }
    }
    private void CheckIsGround()
    {
        if (_characterController.isGrounded)
        {
            _isSingleJumping = false;
            _isDoubleJumping = false;
        }
    }
    private void JumpAnimation()
    {
        Debug.Log("Jump Animation");
        _animator.SetTrigger("Jump");
    }
}
