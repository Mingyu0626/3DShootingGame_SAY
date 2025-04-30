using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _enemyHealthBar;
    [SerializeField]
    private float _refreshDelay = 0.5f;
    private Tween _hpTween;

    private void Update()
    {
        BillBoard();
    }

    public void BillBoard()
    {
        if (_enemyHealthBar != null)
        {
            _enemyHealthBar.transform.forward = Camera.main.transform.forward;
        }
    }

    public void InitEnemyHP(float maxHealthPoint)
    {
        if (_enemyHealthBar != null)
        {
            _enemyHealthBar.maxValue = maxHealthPoint;
            _enemyHealthBar.value = maxHealthPoint;
        }
    }

    public void RefreshEnemyHP(float healthPoint)
    {
        if (_enemyHealthBar != null)
        {
            _enemyHealthBar.value = healthPoint;
        }
    }

    public void RefreshEnemyHPOnDelay(float healthPoint)
    {
        if (!ReferenceEquals(_hpTween, null) && _hpTween.IsActive())
        {
            _hpTween.Kill();
        }
        _hpTween = _enemyHealthBar.DOValue(healthPoint, _refreshDelay).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        if (!ReferenceEquals(_hpTween, null) && _hpTween.IsActive())
        {
            _hpTween.Kill();
        }
    }
}