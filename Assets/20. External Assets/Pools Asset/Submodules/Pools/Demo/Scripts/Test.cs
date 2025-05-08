using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private PoolManager _poolManager;

    [SerializeField]
    private EnemyDemo _enemyPrefab;

    private void Start()
    {
        // Getting pool from pool manager.
        // Also has other overloads.
        var enemyPool = _poolManager.GetPool<EnemyDemo>();

        // Find pool of type Enemy and get object from it.
        // Also has other overloads.
        var enemy = _poolManager.GetFromPool<EnemyDemo>();

        // Find pool of type Enemy and returns clone to it.
        // Also has other overloads.
        _poolManager.TakeToPool<EnemyDemo>(enemy);
    }
}
