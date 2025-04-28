using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    [SerializeField]
    private EnemyController _enemyController;
    [SerializeField]
    private Player _player;

    private void Awake()
    {
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
