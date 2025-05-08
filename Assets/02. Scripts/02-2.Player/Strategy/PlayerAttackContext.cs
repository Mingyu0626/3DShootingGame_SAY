using UnityEngine;

public class PlayerAttackContext : MonoBehaviour
{
    private IAttackStrategy _currentAttackStrategy;
    public IAttackStrategy CurrentAttackStrategy 
    { get => _currentAttackStrategy; set => _currentAttackStrategy = value; }

    private void Update()
    {
        _currentAttackStrategy.Update();
    }

    public void ChangeAttackStrategy(IAttackStrategy attack)
    {
        attack.Enter();
        _currentAttackStrategy = attack;
    }
}
