using UnityEngine;

public class PlayerKnifeAttackState : IPlayerAttackState
{
    private PlayerAttackController _playerAttackController;
    public PlayerKnifeAttackState(PlayerAttackController playerAttackController)
    {
        _playerAttackController = playerAttackController;
    }
    public void Enter()
    {
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerAttackController.PlayerAttackStateContext.
                ChangeState(_playerAttackController.GunAttackState);
            Debug.Log("총 공격 모드로 변경");
        }
    }
    public void Exit()
    {
    }
}
