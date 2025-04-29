using UnityEngine;

public class PlayerWallClimb : MonoBehaviour
{
    private bool _canClimb = true;
    private PlayerData _playerData;
    private CharacterController _characterController;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        WallClimb();
        CheckIsGround();
    }
    private void WallClimb()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (_canClimb && 0f <= _playerData.CurrentStamina && (_characterController.collisionFlags & CollisionFlags.Sides) != 0)
            {
                _playerData.IsClimbing = true;
            }
            else if (_playerData.CurrentStamina == 0f)
            {
                _canClimb = false;
                _playerData.IsClimbing = false;
            }
        }
        else
        {
            _playerData.IsClimbing = false;
        }

        if (_playerData.IsClimbing)
        {
            UseStamina();
        }
        if (_playerData.CurrentStamina == 0f)
        {
            _canClimb = false;
            _playerData.IsClimbing = false;
        }
    }
    private void UseStamina()
    {
        _playerData.CurrentStamina -= Time.deltaTime * _playerData.StaminaCostForClimbing;
    }
    private void CheckIsGround()
    {
        if (_characterController.isGrounded)
        {
            _canClimb = true;
        }
    }
}
