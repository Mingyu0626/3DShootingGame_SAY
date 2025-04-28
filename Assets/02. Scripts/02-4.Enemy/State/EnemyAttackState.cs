using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private EnemyController _enemyController;
    private EnemyData _enemyData;
    private IEnumerator _attackCoroutine;
    private LayerMask _playerLayer;

    public EnemyAttackState(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemyData = enemyController.EnemyData;
        _playerLayer = LayerMask.GetMask("Player");
    }

    public void Enter()
    {
        _attackCoroutine = AttackCoroutine();
        _enemyController.StartCoroutineInEnemyState(_attackCoroutine);
    }

    public void Update()
    {
        if (_enemyController.EnemyData.AttackDistance < 
        Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position))
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            _enemyController.Animator.SetTrigger("AttackDelayToMove");
            Debug.Log("AttackState -> TraceState");
        }
    }

    public void Exit()
    {
        if (!ReferenceEquals(_attackCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            _enemyController.Animator.SetTrigger("AttackDelayToAttack");
            Collider[] hitColliders = Physics.OverlapSphere
                (_enemyController.transform.position, _enemyData.AttackDistance, _playerLayer);
            foreach (Collider collider in hitColliders)
            {
                if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    Damage damage = new Damage()
                    {
                        Value = (int)_enemyData.AttackRange,
                        From = _enemyController.gameObject
                    };
                    damageable.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Player is not in attack range");
                }
            }
            yield return new WaitForSeconds(_enemyController.EnemyData.AttackCoolTime);
        }
    }
} 