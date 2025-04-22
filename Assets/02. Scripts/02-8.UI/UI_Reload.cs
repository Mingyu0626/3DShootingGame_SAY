using TMPro;
using DG.Tweening;
using UnityEngine;

public class UI_Reload : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _reloadingTMP;

    public void ShowReloading()
    {
        _reloadingTMP.text = "재장전중...";
        _reloadingTMP.gameObject.SetActive(true);
        _reloadingTMP.DOFade(1f, 0.5f).From(0f);
    }

    public void HideReloading()
    {
        _reloadingTMP.DOFade(0f, 1f)
            .SetEase(Ease.InExpo)
            .OnKill(() => _reloadingTMP.gameObject.SetActive(false));
    }

    public void ShowReloadSuccess()
    {
        _reloadingTMP.text = "재장전 완료";
        HideReloading();
    }

    public void ShowReloadCancel()
    {
        _reloadingTMP.text = "재장전 취소";
        HideReloading();
    }
}