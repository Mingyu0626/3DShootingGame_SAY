using System.Collections;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private UI_OptionPopup _optionPopup;
    private EGameState _gameState = EGameState.Ready;
    public EGameState GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            if (_gameState == EGameState.Over)
            {
                GameOver();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GameStartFlow());
    }

    private IEnumerator GameStartFlow()
    {
        Time.timeScale = 0f;
        _readyTMP.gameObject.SetActive(true);
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
        _gameState = EGameState.Run;
        _goTMP.DOFade(0f, 0.05f);
    }
    public void Pause()
    {
        _gameState = EGameState.Pause;
        Time.timeScale = 0f;
        _isInputBlocked = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PopupManager.Instance.OpenPopup(EPopupType.UI_OptionPopup, closeCallback: Continue);
    }
    public void Continue()
    {
        _gameState = EGameState.Run;
        Time.timeScale = 1f;
        _isInputBlocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Restart()
    {
        _gameState = EGameState.Run;
        Time.timeScale = 1f;
        _isInputBlocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PopupManager.Instance.ClosePopup();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void GameOver()
    {
        _gameOverTMP.DOFade(1f, 0.05f).SetUpdate(true);
        Time.timeScale = 0f;
        _isInputBlocked = true;
    }
}
