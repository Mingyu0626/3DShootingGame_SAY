using UnityEngine;

public class EnemyReturnState : IEnemyState
{
    private EnemyController _enemyController;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Update()
    {
        if (Vector3.Distance(_enemyController.transform.position, _enemyController.StartPosition) 
        <= 0.001f)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.IdleState);
            Debug.Log("ReturnState -> IdleState");
        }

        if (Vector3.Distance(_enemyController.transform.position, _enemyController.Player.transform.position) < _enemyController.ReturnDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("ReturnState -> TraceState");
        }
        Return();
    }

    public void Exit()
    {
    }
    private void Return()
    {
        Vector3 direction = (_enemyController.StartPosition - _enemyController.transform.position).normalized;
        _enemyController.CharacterController.Move(direction * _enemyController.MoveSpeed * Time.deltaTime);
    }
} 