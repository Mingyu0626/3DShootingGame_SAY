using System.Collections;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _attackCoroutine;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _attackCoroutine = AttackCoroutine();
        _enemyController.StartCoroutineInEnemyState(_attackCoroutine);
    }

    public void Update()
    {
        if (_enemyController.AttackDistance < Vector3.Distance(_enemyController.transform.position, _enemyController.Player.transform.position))
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
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
            Debug.Log("공격");
            yield return new WaitForSeconds(_enemyController.AttackCoolTime);
        }
    }
} 