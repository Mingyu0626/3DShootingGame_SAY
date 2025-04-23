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
    // 기본 Enemy 클래스
    public void Init()
    {
    }
} 