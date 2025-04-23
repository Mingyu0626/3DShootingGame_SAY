using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _damagedCoroutine;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _damagedCoroutine = DamagedCoroutine();
        _enemyController.StartCoroutineInEnemyState(_damagedCoroutine);
    }

    public void Update()
    {
        if (_enemyController.EnemyData.CurrentHealthPoint <= 0)
        {
            _enemyController.EnemyStateContext.ChangeState(_enemyController.DieState);
            Debug.Log("DamagedState -> DieState");
        }
    }

    public void Exit()
    {
        if (!ReferenceEquals(_damagedCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_damagedCoroutine);
            _damagedCoroutine = null;
        }
    }

    private IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(_enemyController.EnemyData.DamagedTime);
        
        _enemyController.EnemyStateContext.ChangeState(_enemyController.TraceState);
            Debug.Log("DamagedState -> TraceState");
    }
} 