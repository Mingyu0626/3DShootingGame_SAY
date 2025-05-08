using UnityEngine;

public interface IAttackStrategy
{
    public void Enter();
    public void Update();
    public void Attack();
    public void AttackAnimation();
}
