using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : IEnemyState
{
    private EnemyController _enemyController;

    public EnemyReturnState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        float distanceToPlayer = Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position);
        float distanceToOrigin = Vector3.Distance(_enemyController.StartPosition, _enemyController.transform.position);
        if (distanceToPlayer <= _enemyController.EnemyData.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("ReturnState -> TraceState");
        }
        else if (distanceToOrigin <= 1f)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.IdleState);
            _enemyController.Animator.SetTrigger("MoveToIdle");
            Debug.Log("ReturnState -> IdleState");
        }
        else
        {
            _enemyController.Agent.SetDestination(_enemyController.StartPosition);
        }
    }

    public void Exit()
    {
    }
} 