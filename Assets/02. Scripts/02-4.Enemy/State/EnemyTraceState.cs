using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    private EnemyController _enemyController;
    public EnemyTraceState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void Enter()
    {
        
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position);
        
        if (distance > _enemyController.EnemyData.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.ReturnState);
            Debug.Log("TraceState -> ReturnState");
        }
        else if (distance <= _enemyController.EnemyData.AttackDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.AttackState);
            Debug.Log("TraceState -> AttackState");
        }
        else
        {
            Vector3 direction = (_enemyController.Player.transform.position - _enemyController.transform.position).normalized;
            _enemyController.CharacterController.Move(direction * _enemyController.EnemyData.MoveSpeed * Time.deltaTime);
        }
    }

    public void Exit()
    {
    }
} 