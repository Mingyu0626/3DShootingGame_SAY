using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool _isSingleJumping = false;
    private bool _isDoubleJumping = false;
    private PlayerData _playerData;
    private CharacterController _characterController;


    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _characterController = GetComponent<CharacterController>();
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
            }
            else if (!_isDoubleJumping)
            {
                _playerData.YVelocity = _playerData.JumpPower;
                _isDoubleJumping = true;
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
}
