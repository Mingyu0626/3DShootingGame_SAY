using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] 
    private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Explosion")]
    [SerializeField] 
    private float _explosionRadius = 5f;
    [SerializeField] 
    private int _explosionDamage = 50;
    [SerializeField] 
    private float _explosionForce = 10f;
    [SerializeField] 
    private GameObject _explosionEffect;

    [Header("Physics")]
    [SerializeField] 
    private float _randomForceMin = 5f;
    [SerializeField] 
    private float _randomForceMax = 15f;
    private Rigidbody _rigidbody;

    private Collider[] _hitColliders = new Collider[10];

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
            Destroy(gameObject, 3f);
        }
    }

    private void Explode()
    {
        ApplyExplosionDamage();
        ApplyExplosionForce();
        ApplyRandomForce();
        SpawnExplosionEffect();
    }

    private void ApplyExplosionDamage()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            Collider collider = _hitColliders[i];
            if (collider.CompareTag("Enemy"))
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();
                if (!ReferenceEquals(enemy, null))
                {
                    Damage damage = new Damage()
                    {
                        Value = _explosionDamage,
                        From = gameObject
                    };
                    enemy.TakeDamage(damage);
                }
            }
            else if (collider.CompareTag("Player"))
            {
                PlayerData player = collider.GetComponent<PlayerData>();
                if (!ReferenceEquals(player, null))
                {
                    player.CurrentStamina -= _explosionDamage;
                }
            }
        }
    }

    private void ApplyExplosionForce()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            Collider collider = _hitColliders[i];
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
            {
                CharacterController characterController = collider.GetComponent<CharacterController>();
                if (!ReferenceEquals(characterController, null))
                {
                    Vector3 direction = (collider.transform.position - transform.position).normalized;
                    characterController.Move(direction * _explosionForce * Time.deltaTime);
                }
            }
        }
    }

    private void ApplyRandomForce()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0.5f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
        
        float randomForce = Random.Range(_randomForceMin, _randomForceMax);
        _rigidbody.AddForce(randomDirection * randomForce, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.up, ForceMode.Impulse);
    }

    private void SpawnExplosionEffect()
    {
        if (!ReferenceEquals(_explosionEffect, null))
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        }
    }
} 