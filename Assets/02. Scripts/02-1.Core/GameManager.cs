using System.Collections;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private TextMeshProUGUI _readyTMP;
    [SerializeField]
    private TextMeshProUGUI _goTMP;
    [SerializeField]
    private TextMeshProUGUI _gameOverTMP;

    [SerializeField]
    private float _waitTimeBeforeReadyTMPOff = 2f;
    [SerializeField]
    private float _waitTimeBeforeGoTMPOff = 0.5f;

    private bool _isInputBlocked = true;
    public bool IsInputBlocked { get => _isInputBlocked; set => _isInputBlocked = value; }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GameStartFlow());
    }

    private IEnumerator GameStartFlow()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(_waitTimeBeforeReadyTMPOff);
        _readyTMP.DOFade(0f, 0.05f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _goTMP.DOFade(1f, 0.05f).SetUpdate(true);
            });
        yield return new WaitForSecondsRealtime(_waitTimeBeforeGoTMPOff);
        _isInputBlocked = false;
        Time.timeScale = 1f;
        _goTMP.DOFade(0f, 0.05f);
    }
    private void GameOver()
    {
        _gameOverTMP.DOFade(1f, 0.05f).SetUpdate(true);
        Time.timeScale = 0f;
        _isInputBlocked = true;
    }
}
