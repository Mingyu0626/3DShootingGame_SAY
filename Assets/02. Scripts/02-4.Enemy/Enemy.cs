using UnityEngine;

public enum EnemyState
{
    Idle,
    Trace,
    Return,
    Attack,
    Damaged,
    Die
}

public class Enemy : MonoBehaviour, IProduct
{
    public void Init()
    {
    }


} 