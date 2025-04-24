using UnityEngine;

public class EnemyPool : ObjectPool<EEnemyType, Enemy>
{
    protected override void Awake()
    {
        base.Awake();
    }
} 