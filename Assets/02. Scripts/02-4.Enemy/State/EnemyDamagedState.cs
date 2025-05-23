using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : IEnemyState
{
    private EnemyController _enemyController;
    private IEnumerator _damagedCoroutine;
    private Vector3 _knockbackDirection;

    public EnemyDamagedState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        _knockbackDirection = (_enemyController.transform.position - _enemyController.Player.transform.position).normalized;
        _damagedCoroutine = DamagedCoroutine();
        _enemyController.StartCoroutineInEnemyState(_damagedCoroutine);

        _enemyController.Agent.ResetPath();
        _enemyController.Agent.isStopped = true;
        SetEnemyMaterialsColor(Color.red);
    }

    public void Update()
    {
        _enemyController.CharacterController.Move(_knockbackDirection * _enemyController.EnemyData.KnockbackSpeed * Time.deltaTime);
    }

    public void Exit()
    {
        if (!ReferenceEquals(_damagedCoroutine, null))
        {
            _enemyController.StopCoroutineInEnemyState(_damagedCoroutine);
            _damagedCoroutine = null;
        }
        _enemyController.Agent.isStopped = false;
        SetEnemyMaterialsColor(Color.white);
    }

    private IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(_enemyController.EnemyData.DamagedTime);
        _enemyController.EnemyStateContext.ChangeState(_enemyController.EnemyStateDict[EEnemyState.Trace]);
        Debug.Log("DamagedState -> TraceState");
    }

    private void SetEnemyMaterialsColor(Color color)
    {
        foreach (var material in _enemyController.EnemyData.MaterialList)
        {
            material.color = color;
        }
    }
} 