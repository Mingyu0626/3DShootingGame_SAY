using UnityEngine;

public class PlayerAttackContext : MonoBehaviour
{
    private IAttack _currentAttackStrategy;
    public IAttack CurrentAttackStrategy 
    { get => _currentAttackStrategy; set => _currentAttackStrategy = value; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentAttackStrategy.Attack();
        }
    }

    public void ChangeAttackStrategy(IAttack attack)
    {
        _currentAttackStrategy = attack;
    }
}
