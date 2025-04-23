using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _dieCoroutine;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
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
        // TODO: Implement die logic (e.g., play death animation, drop items, etc.)
        yield return new WaitForSeconds(2f);
        EnemyPool.Instance.ReturnObject(_enemyController.GetComponent<Enemy>());
    }
} 