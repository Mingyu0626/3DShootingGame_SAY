using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerAttackController _playerAttackController;

    public void GunAttackEvent()
    {
        Debug.Log("GunAttack Event");
    }
    public void MeleeAttackEvent()
    {
        Debug.Log("MeleeAttack Event");
        _playerAttackController.MeleeAttack.MeleeAttack();
        _playerAttackController.MeleeAttack.AttackVFX();
    }
    public void ThrowBombEvent()
    {
        Debug.Log("ThrowBomb Event");
        _playerAttackController.BombAttack.FireBomb();
    }
}
