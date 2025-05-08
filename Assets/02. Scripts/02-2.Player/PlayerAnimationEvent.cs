using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerAttackController _playerAttackController;

    public void GunAttackEvent()
    {
        Debug.Log("GunAttack Event");
        _playerAttackController.AttackStrategyDict[EAttackMode.Gun].Attack();
    }
    public void MeleeAttackEvent()
    {
        Debug.Log("MeleeAttack Event");
        _playerAttackController.AttackStrategyDict[EAttackMode.Melee].Attack();
    }
    public void ThrowBombEvent()
    {
        Debug.Log("ThrowBomb Event");
        _playerAttackController.AttackStrategyDict[EAttackMode.Bomb].Attack();
    }
}
