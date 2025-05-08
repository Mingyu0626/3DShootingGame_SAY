using System;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    private Action _closeCallback;
    public void Open(Action closeCallback = null)
    {
        gameObject.SetActive(true);
        _closeCallback = closeCallback;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        _closeCallback?.Invoke();
    }
}
