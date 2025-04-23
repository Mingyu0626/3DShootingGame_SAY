using System.Collections;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _patrolCoroutine;
    private int _currentPatrolIndex;

    public EnemyPatrolState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        _currentPatrolIndex = 0;
        _patrolCoroutine = PatrolCoroutine();
        _enemyController.StartCoroutineInEnemyState(_patrolCoroutine);
    }

    public void Update()
    {
        if (Vector3.Distance(_enemyController.Player.transform.position, _enemyController.transform.position) 
        <= _enemyController.EnemyData.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("PatrolState -> TraceState");
        }
    }

    public void Exit()
    {
        if (!ReferenceEquals(_patrolCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_patrolCoroutine);
            _patrolCoroutine = null;
        }
    }

    private IEnumerator PatrolCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = _enemyController.EnemyData.PatrolPoints[_currentPatrolIndex];
            while (Vector3.Distance(_enemyController.transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - _enemyController.transform.position).normalized;
                _enemyController.CharacterController.Move(direction * _enemyController.EnemyData.MoveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(_enemyController.EnemyData.PatrolWaitTime);
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _enemyController.EnemyData.PatrolPoints.Count;
        }
    }
}
