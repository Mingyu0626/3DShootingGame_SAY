using Redcode.Pools;
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
    private Collider[] _reusableColliders = new Collider[50];

    public EnemyDieState(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemy = _enemyController.GetComponent<Enemy>();
    }

    public void Enter()
    {
        _dieCoroutine = DieCoroutine();
        _enemyController.StartCoroutineInEnemyState(_dieCoroutine);
        _enemyController.GetComponent<CharacterController>().enabled = false;
        _enemyController.Agent.isStopped = true;
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
        PoolManager.Instance.TakeToPool<Enemy>($"Enemy{_enemyController.EnemyData.EnemyType}", _enemy);
        // EnemyPool.Instance.ReturnObject(_enemyController.GetComponent<Enemy>());
        _enemy.SpawnGold();
    }
    private void DeathExplosion()
    {
        int layerMask = LayerMask.GetMask("Player", "Enemy");
        int count = Physics.OverlapSphereNonAlloc(
            _enemyController.transform.position,
            _explosionRadius,
            _reusableColliders,
            layerMask
        );

        for (int i = 0; i < count; i++)
        {
            Collider hitCollider = _reusableColliders[i];
            if (hitCollider == _enemyController.GetComponent<CharacterController>())
            {
                continue;
            }
            if (hitCollider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(new Damage
                {
                    Value = _explosionDamage,
                    From = _enemyController.gameObject
                });
            }
        }
    }
} 