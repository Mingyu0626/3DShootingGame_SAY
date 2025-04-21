using System.Collections;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    private bool _isRolling = false;
    private PlayerData _playerData;
    private CharacterController _characterController;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Roll();
    }
    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerData.StaminaCostForRolling <= _playerData.Stamina && !_isRolling)
        {
            StartCoroutine(RollCoroutine());
        }
    }
    private IEnumerator RollCoroutine()
    {
        _playerData.Stamina -= _playerData.StaminaCostForRolling;
        _isRolling = true;
        _playerData.MoveSpeed *= _playerData.RollingPower;

        yield return new WaitForSeconds(_playerData.RollDuration);

        _playerData.MoveSpeed /= _playerData.RollingPower;
        _isRolling = false;
        yield return null;
    }
}
