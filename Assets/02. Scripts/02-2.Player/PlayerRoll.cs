using System.Collections;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    [Header("Rolling")]
    [SerializeField]
    private float _rollingPower = 10f;
    [SerializeField]
    private float _staminaCostForRolling = 20f;
    [SerializeField]
    private float _rollDuration = 0.1f;
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
        if (Input.GetKeyDown(KeyCode.E) && _staminaCostForRolling <= _playerData.Stamina && !_isRolling)
        {
            StartCoroutine(RollCoroutine());
        }
    }
    private IEnumerator RollCoroutine()
    {
        Debug.Log("구르기");
        _playerData.Stamina -= _staminaCostForRolling;
        _isRolling = true;
        _playerData.MoveSpeed *= _rollingPower;

        yield return new WaitForSeconds(_rollDuration);

        _playerData.MoveSpeed /= _rollingPower;
        _isRolling = false;
        Debug.Log("구르기 끝");
        yield return null;
    }
}
