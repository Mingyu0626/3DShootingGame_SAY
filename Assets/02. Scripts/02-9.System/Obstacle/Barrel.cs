
using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
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
    private int _explosionDamage = 5;
    [SerializeField] 
    private float _explosionForce = 10f;

    [Header("Physics")]
    [SerializeField] 
    private float _randomForceMin = 10f;
    [SerializeField] 
    private float _randomForceMax = 20f;
    [SerializeField]
    private float _randomTorqueMin = 10f;
    [SerializeField]
    private float _randomTorqueMax = 20f;
    private Rigidbody _rigidbody;

    private List<Collider> _hitCollidersList = new List<Collider>();

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
        PlayExplosionVFX();
    }

    private void ApplyExplosionDamage()
    {
        Collider[] hitColiiders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider hitCollider in hitColiiders)
        {
            if (hitCollider.CompareTag("Player") || hitCollider.CompareTag("Enemy"))
            {
                _hitCollidersList.Add(hitCollider);
            }
        }

        foreach (Collider hitCollider in _hitCollidersList)
        {
            if (hitCollider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = _explosionDamage,
                    From = gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
        _hitCollidersList.Clear();
    }

    private void ApplyExplosionForce()
    {
        foreach (Collider collider in _hitCollidersList)
        {
            CharacterController characterController = collider.GetComponent<CharacterController>();
            if (!ReferenceEquals(characterController, null))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                characterController.Move(direction * _explosionForce * Time.deltaTime);
            }
        }
    }
    private void ApplyRandomForce()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;

        Vector3 randomForce = (Vector3.up + new Vector3(
            Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f))).normalized
            * Random.Range(_randomForceMin, _randomForceMax);
        _rigidbody.AddForce(randomForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)).normalized 
            * Random.Range(_randomTorqueMin, _randomTorqueMax);
        _rigidbody.AddTorque(randomTorque, ForceMode.Impulse);
    }

    private void PlayExplosionVFX()
    {
        VFX vfx = PoolManager.Instance.GetFromPool<VFX>(EPoolObjectName.VFX_BarrelExplosion.ToString());
        vfx.OnGetFromPool(transform.position);
    }
} 