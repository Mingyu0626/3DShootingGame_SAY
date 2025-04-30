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
        
        if (_enemyController.EnemyData.FindDistance < distance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.ReturnState);
            Debug.Log("TraceState -> ReturnState");
        }
        else if (distance <= _enemyController.EnemyData.AttackDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.AttackState);
            _enemyController.Animator.SetTrigger("MoveToAttackDelay");
            Debug.Log("TraceState -> AttackState");
        }
        else
        {
            _enemyController.Agent.SetDestination(_enemyController.Player.transform.position);
            Debug.Log($"isStopped: {_enemyController.Agent.isStopped}");
            Debug.Log($"speed: {_enemyController.Agent.speed}");
            Debug.Log($"updatePosition: {_enemyController.Agent.updatePosition}");
            Debug.Log($"updateRotation: {_enemyController.Agent.updateRotation}");
            Debug.Log($"velocity: {_enemyController.Agent.velocity}");
        }
    }

    public void Exit()
    {
    }
} 