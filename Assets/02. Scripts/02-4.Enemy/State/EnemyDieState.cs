using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IEnemyState
{
    private EnemyController _enemyController;
    private Enemy _enemy;
    private IEnumerator _dieCoroutine;

    [Header("Death Explosion")]
    private float _explosionRadius = 10f;
    private int _explosionDamage = 10;
    private List<Collider> _hitCollidersList = new List<Collider>();

    public EnemyDieState(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemy = _enemyController.GetComponent<Enemy>();
    }

    public void Enter()
    {
        _enemyController.Agent.isStopped = true;
        _dieCoroutine = DieCoroutine();
        _enemyController.StartCoroutineInEnemyState(_dieCoroutine);
        if (_enemyController.EnemyData.EnemyType == EEnemyType.Elite)
        {
            _enemyController.ExplosionEffect();
            DeathExplosion();
        }
    }

    public void Update()
    {
    }

    public void Exit()
    {
        if (!ReferenceEquals(_dieCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_dieCoroutine);
            _dieCoroutine = null;
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2f);
        EnemyPool.Instance.ReturnObject(_enemyController.GetComponent<Enemy>());
        _enemy.SpawnGold();
    }

    private void DeathExplosion()
    {
        Collider[] hitColiiders = Physics.OverlapSphere(_enemyController.transform.position, _explosionRadius);
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
                    From = _enemyController.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
} 