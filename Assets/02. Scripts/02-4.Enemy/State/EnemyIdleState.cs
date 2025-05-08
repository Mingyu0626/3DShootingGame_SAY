using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _idleCoroutine;

    public EnemyIdleState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        _idleCoroutine = IdleCoroutine();
        _enemyController.StartCoroutineInEnemyState(_idleCoroutine);
        _enemyController.GetComponent<CharacterController>().enabled = true;
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position);
        if (distance <= _enemyController.EnemyData.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.EnemyStateDict[EEnemyState.Trace]);
            _enemyController.Animator.SetTrigger("IdleToMove");
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
        yield return new WaitForSeconds(_enemyController.EnemyData.IdleToPatrolWaitTime);
        _enemyController.EnemyStateContext.ChangeState(_enemyController.EnemyStateDict[EEnemyState.Patrol]);
        _enemyController.Animator.SetTrigger("IdleToMove");
        Debug.Log("IdleState -> PatrolState");
    }
} 