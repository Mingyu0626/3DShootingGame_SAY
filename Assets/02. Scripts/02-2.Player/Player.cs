using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private PlayerData _playerData;
    private PlayerEffect _playerEffect;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerEffect = GetComponent<PlayerEffect>();
    }
    public void TakeDamage(Damage damage)
    {
        _playerData.CurrentHealthPoint -= damage.Value;
        _playerEffect.DamagedEffect();
        if (_playerData.CurrentHealthPoint <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
}
