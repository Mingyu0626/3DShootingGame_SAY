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
    }
    public void ThrowBombEvent()
    {
        Debug.Log("ThrowBomb Event");
        _playerAttackController.BombAttack.FireBomb();
    }
}
