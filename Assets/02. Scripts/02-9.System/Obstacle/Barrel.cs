using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Explosion")]
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private int _explosionDamage = 50;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private GameObject _explosionEffect;

    [Header("Physics")]
    [SerializeField] private float _randomForceMin = 5f;
    [SerializeField] private float _randomForceMax = 15f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        _currentHealth -= damage.Value;
        
        if (_currentHealth <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 폭발 이펙트 생성
        if (_explosionEffect != null)
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        }

        // 주변 물체에 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            // 적에게 데미지
            if (collider.CompareTag("Enemy"))
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    Damage damage = new Damage()
                    {
                        Value = _explosionDamage,
                        From = gameObject
                    };
                    enemy.TakeDamage(damage);
                }
            }
            // 플레이어에게 데미지
            else if (collider.CompareTag("Player"))
            {
                PlayerData player = collider.GetComponent<PlayerData>();
                if (player != null)
                {
                    player.Stamina -= _explosionDamage;
                }
            }

            // 물리적 힘 적용
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        // 랜덤한 방향으로 날아가기
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0.5f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
        
        float randomForce = Random.Range(_randomForceMin, _randomForceMax);
        _rigidbody.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        // 3초 후 파괴
        Destroy(gameObject, 3f);
    }
} 