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
        float distance = Vector3.Distance(_enemyController.StartPosition, _enemyController.transform.position);
        
        if (distance <= 0.1f)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.IdleState);
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