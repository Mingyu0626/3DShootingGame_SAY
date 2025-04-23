using UnityEngine;

public class EnemyPool : ObjectPool<EnemyType, Enemy>
{
    protected override void Awake()
    {
        base.Awake();
    }
} 