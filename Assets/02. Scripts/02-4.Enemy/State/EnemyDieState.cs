using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _dieCoroutine;

    private Enemy _enemy; 

    public EnemyDieState(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemy = _enemyController.GetComponent<Enemy>();
    }

    public void Enter()
    {
        _dieCoroutine = DieCoroutine();
        _enemyController.StartCoroutineInEnemyState(_dieCoroutine);
    }

    public void Update()
    {
    }

    public void Exit()
    {
        if (!ReferenceEquals(_dieCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_dieCoroutine);
            _dieCoroutine = null;
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2f);
        EnemyPool.Instance.ReturnObject(_enemyController.GetComponent<Enemy>());
        _enemy.SpawnGold();
    }
} 