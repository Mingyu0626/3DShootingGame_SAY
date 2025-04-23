using UnityEngine;

public class EnemyDieState : IEnemyState
{
    private EnemyController _enemyController;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
} 