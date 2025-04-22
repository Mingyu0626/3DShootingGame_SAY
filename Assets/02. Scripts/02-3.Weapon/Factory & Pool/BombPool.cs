using UnityEngine;

public class BombPool : ObjectPool<BombType, Bomb>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
