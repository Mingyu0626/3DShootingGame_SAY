using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyController _enemyController;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Update()
    {
        if (Vector3.Distance(_enemyController.transform.position, _enemyController.Player.transform.position) < _enemyController.FindDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("IdleState -> TraceState");
        }
    }

    public void Exit()
    {
    }
} 