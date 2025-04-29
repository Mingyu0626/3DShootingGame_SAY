using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    private bool _isRunning = false;
    private PlayerData _playerData;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }
    private void Update()
    {
        Run();
    }
    private void Run()
    {
        float moveSpeedMultiplier = _playerData.MoveSpeedMultiplier;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (0f < _playerData.CurrentStamina && !_isRunning)
            {
                _isRunning = true;
                _playerData.MoveSpeed *= moveSpeedMultiplier;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && _isRunning)
        {
            UseStamina();
            if (_playerData.CurrentStamina == 0f)
            {
                _isRunning = false;
                _playerData.MoveSpeed /= moveSpeedMultiplier;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && _isRunning)
        {
            _isRunning = false;
            _playerData.MoveSpeed /= moveSpeedMultiplier;
            Debug.Log("달리기 종료");
        }
        else
        {
            RecoverStamina();
        }
    }
    private void UseStamina()
    {
        _playerData.CurrentStamina -= Time.deltaTime * _playerData.StaminaCostForRun;
    }
    private void RecoverStamina()
    {
        _playerData.CurrentStamina += Time.deltaTime * _playerData.StaminaCostForRun;
    }
}
