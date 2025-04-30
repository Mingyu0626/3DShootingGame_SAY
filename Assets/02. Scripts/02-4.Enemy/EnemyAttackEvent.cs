using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    private EnemyController _enemyController;
    private Player _player;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _player = FindFirstObjectByType<Player>();
    }
    public void AttackEvent()
    {
        _player.TakeDamage
            (new Damage
        {
            Value = (int)_enemyController.EnemyData.AttackDamage,
            From = _enemyController.gameObject
        });
    }
}
