using System;
using System.Collections.Generic;
using UnityEngine;

public enum EPopupType
{
    UI_OptionPopup,
    UI_CreditPopup,

    Count
}

public class PopupManager : Singleton<PopupManager>
{
    private Stack<UI_Popup> _openedPopups = new Stack<UI_Popup>();
    public Stack<UI_Popup> OpenedPopups => _openedPopups;

    [SerializeField]
    private List<UI_Popup> _popups = new List<UI_Popup>();
    public List<UI_Popup> Popups => _popups;


    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopup();
        }
    }
    public void OpenPopup(EPopupType popupType, Action closeCallback = null)
    {
        foreach (UI_Popup popup in _popups)
        {
            if (popup.gameObject.name == popupType.ToString())
            {
                popup.Open(closeCallback);
                _openedPopups.Push(popup);
                return;
            }
        }
    }
    public void ClosePopup()
    {
        if (_openedPopups.Count > 0)
        {
            while (true)
            {
                bool opened = _openedPopups.Peek().isActiveAndEnabled;
                _openedPopups.Pop().Close();

                if (opened || _openedPopups.Count == 0)
                {
                    break;
                }
            }
        }
        else
        {
            GameManager.Instance.Pause();
        }
    }
}
