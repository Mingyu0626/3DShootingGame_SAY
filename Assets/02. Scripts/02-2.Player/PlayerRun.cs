using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    private bool _isRunning = false;
    private PlayerData _playerData;
    private PlayerPresenter _playerPresenter;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerPresenter = GetComponent<PlayerPresenter>();
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
            if (0f < _playerData.Stamina && !_isRunning)
            {
                _isRunning = true;
                _playerData.MoveSpeed *= moveSpeedMultiplier;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && _isRunning)
        {
            UseStamina();
            if (_playerData.Stamina == 0f)
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
        _playerPresenter.OnStaminaChanged(-Time.deltaTime * _playerData.StaminaCostForRun);
    }
    private void RecoverStamina()
    {
        _playerPresenter.OnStaminaChanged(Time.deltaTime * _playerData.StaminaCostForRun);
    }
}
