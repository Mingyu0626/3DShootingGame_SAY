using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnedEnemyInfo
{
    public EEnemyType EnemyType;
    public int Probability;

    public SpawnedEnemyInfo(EEnemyType enemyType, int probability)
    {
        EnemyType = enemyType;
        Probability = probability;
    }
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] 
    private float _spawnInterval = 3f;
    [SerializeField] 
    private float _spawnRadius = 5f;
    [SerializeField] 
    private bool _isSpawning = true;

    [Header("Enemy Probability")]
    [SerializeField] 
    private List<SpawnedEnemyInfo> _spawnedEnemyInfoList = new List<SpawnedEnemyInfo>();

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (ReferenceEquals(_spawnCoroutine, null))
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }

    public void StopSpawning()
    {
        if (!ReferenceEquals(_spawnCoroutine, null))
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // 스폰 위치 계산 (랜덤 원형 범위 내)
        Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        EEnemyType enemyType = PickSpawnEnemyType();
        Enemy enemy = EnemyPool.Instance.GetObject(enemyType, spawnPosition);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.EnemyStateContext.ChangeState(enemyController.EnemyStateDict[EEnemyState.Idle]);
    }

    private EEnemyType PickSpawnEnemyType()
    {
        int randNum = Random.Range(0, 100);
        int probabilityPrefixSum = 0;

        foreach (var info in _spawnedEnemyInfoList)
        {
            probabilityPrefixSum += info.Probability;
            if (randNum < probabilityPrefixSum)
            {
                return info.EnemyType;
            }
        }

        return EEnemyType.Normal; // 기본값
    }
} 