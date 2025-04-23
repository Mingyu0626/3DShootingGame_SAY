using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    private EnemyController _enemyController;
    private float _moveSpeed = 3.3f;
    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Update()
    {
        if (_enemyController.ReturnDistance < Vector3.Distance(_enemyController.transform.position, _enemyController.Player.transform.position))
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.ReturnState);
            Debug.Log("TraceState -> ReturnState");
        }
        if (Vector3.Distance(_enemyController.transform.position, _enemyController.Player.transform.position) <= _enemyController.AttackDistance)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.AttackState);
            Debug.Log("TraceState -> AttackState");
        }
        Trace();
    }

    public void Exit()
    {
    }

    private void Trace()
    {
        Vector3 direction = 
        (_enemyController.Player.transform.position - _enemyController.transform.position).normalized;
        _enemyController.CharacterController.Move(direction * _moveSpeed * Time.deltaTime);
    }
} 