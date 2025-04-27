using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [Header("Damaged Effect - UI Screen")]
    [SerializeField]
    private Image _damagedEffectImage;
    [SerializeField]
    private float _damagedEffectFadeInTime = 0.05f;
    [SerializeField]
    private float _damagedEffectFadeOutTime = 0.5f;

    public void DamagedEffect()
    {
        _damagedEffectImage.DOFade(1f, _damagedEffectFadeInTime)
            .OnComplete(() =>
            {
                _damagedEffectImage.DOFade(0f, _damagedEffectFadeOutTime);
            });
    }
}
