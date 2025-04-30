using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _enemyHealthBar;
    [SerializeField]
    private float _refreshDelay = 0.5f;

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
    public void InitEnemyHP(float maxHealthPoint)
    {
        if (!ReferenceEquals(_enemyHealthBar, null))
        {
            _enemyHealthBar.maxValue = maxHealthPoint;
            _enemyHealthBar.value = maxHealthPoint;
        }
    }
    public void RefreshEnemyHP(float healthPoint)
    {
        if (!ReferenceEquals(_enemyHealthBar, null))
        {
            _enemyHealthBar.value = healthPoint;
        }
    }
    public void RefreshEnemyHPOnDelay(float healthPoint)
    {
        _enemyHealthBar.DOValue(healthPoint, _refreshDelay).SetEase(Ease.Linear);
    }
}
