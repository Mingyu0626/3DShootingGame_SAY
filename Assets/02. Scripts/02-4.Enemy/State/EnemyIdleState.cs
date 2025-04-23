using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _idleCoroutine;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _idleCoroutine = IdleCoroutine();
        _enemyController.StartCoroutineInEnemyState(_idleCoroutine);
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position);
        if (distance <= _enemyController.EnemyData.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("IdleState -> TraceState");
        }
    }

    public void Exit()
    {
        if (!ReferenceEquals(_idleCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_idleCoroutine);
            _idleCoroutine = null;
        }
    }

    private IEnumerator IdleCoroutine()
    {
        yield return new WaitForSeconds(5f);
        _enemyController.EnemyStateContext.ChangeState(_enemyController.PatrolState);
        Debug.Log("IdleState -> PatrolState");
    }
} 