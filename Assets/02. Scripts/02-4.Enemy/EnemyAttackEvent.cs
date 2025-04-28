using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    [SerializeField]
    private EnemyController _enemyController;

    public void AttackEvent()
    {
        _enemyController.TakeDamage
            (new Damage
        {
            Value = (int)_enemyController.EnemyData.AttackDamage,
            From = _enemyController.gameObject
        });
    }
}
