using UnityEngine;

public class PlayerAttackStateContext
{
    private IPlayerAttackState _currentState;
    public IPlayerAttackState CurrentState { get => _currentState; set => _currentState = value; }
    private PlayerAttackController _playerAttackController;

    public PlayerAttackStateContext(PlayerAttackController controller)
    {
        _playerAttackController = controller;
    }

    public void ChangeState()
    {
        _currentState = new PlayerGunAttackState(_playerAttackController);
        _currentState.Enter();
    }

    public void ChangeState(IPlayerAttackState newState)
    {
        if (!ReferenceEquals(_currentState, null))
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter();
    }
}
