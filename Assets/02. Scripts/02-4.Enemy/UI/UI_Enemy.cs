using UnityEngine;
using UnityEngine.UI;

public class UI_Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _enemyHealthBar;

    private void Update()
    {
        BillBoard();
    }
    public void BillBoard()
    {
        if (!ReferenceEquals(_enemyHealthBar, null))
        {
            _enemyHealthBar.gameObject.transform.forward = Camera.main.transform.forward;
        }
    }
    public void InitSliderEnemyHealthPoint(float maxHealthPoint)
    {
        if (!ReferenceEquals(_enemyHealthBar, null))
        {
            _enemyHealthBar.maxValue = maxHealthPoint;
            _enemyHealthBar.value = maxHealthPoint;
        }
    }
    public void SetSliderEnemyHealthPoint(float healthPoint)
    {
        if (!ReferenceEquals(_enemyHealthBar, null))
        {
            _enemyHealthBar.value = healthPoint;
        }
    }
}
