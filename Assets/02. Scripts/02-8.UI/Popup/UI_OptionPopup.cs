using UnityEngine;

public class UI_OptionPopup : UI_Popup
{
    public void OnClickContinueButton()
    {
        PopupManager.Instance.ClosePopup();
    }

    public void OnClickRetryButton()
    {
        GameManager.Instance.Restart();
    }

    public void OnClickQuitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OnClickCreditButton()
    {
        PopupManager.Instance.OpenPopup(EPopupType.UI_CreditPopup, null);
    }
}
