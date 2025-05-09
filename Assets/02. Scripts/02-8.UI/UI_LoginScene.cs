using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public class UI_AccountInputFields
{
    public TextMeshProUGUI ResultTMP;
    public TMP_InputField InputFieldId;
    public TMP_InputField InputFieldPassword;
    public TMP_InputField InputFieldPasswordConfirm;
    public Button ConfirmButton; // 로그인 or 회원가입 버튼 연결
}

[Serializable]
public class PlayerAccountInfo
{
    public string Id;
    public string Password;
}


public class UI_LoginScene : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField]
    private GameObject _loginPanel;
    [SerializeField]
    private GameObject _registerPanel;

    [Header("Login")]
    [SerializeField]
    private UI_AccountInputFields _loginInputFields;

    [Header("Register")]
    [SerializeField]
    private UI_AccountInputFields _registerInputFields;


    private void Awake()
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);
    }

    public void OnClickGotoRegisterButton()
    {
        _loginPanel.SetActive(false);
        _registerPanel.SetActive(true);
    }

    public void OnClickGotoLoginButton()
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);
    }

    public void Register()
    {
        // 1. 아이디 입력을 확인한다.
        string id = _registerInputFields.InputFieldId.text;

        if (string.IsNullOrEmpty(id))
        {
            _registerInputFields.ResultTMP.text = "아이디를 입력해주세요.";
            return;
        }

        // 2. 비밀번호 입력을 확인한다.
        string password = _registerInputFields.InputFieldPassword.text;
        if (string.IsNullOrEmpty(password))
        {
            _registerInputFields.ResultTMP.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 3. 비밀번호 확인 입력이 비밀번호 입력과 같은지 체크한다.
        string passwordConfirm = _registerInputFields.InputFieldPasswordConfirm.text;
        if (password != passwordConfirm)
        {
            _registerInputFields.ResultTMP.text = "입력하신 비밀번호가 일치하지 않습니다.";
            return;
        }

        // 4. PlayerPrefs를 이용해서 저장한다.
        PlayerPrefs.SetString("Id", id);
        PlayerPrefs.SetString("Password", password);

        // PlayerAccountInfo로 클래스화하여 저장
        PlayerAccountInfo accountInfo = new PlayerAccountInfo
        {
            Id = id,
            Password = password
        };

        string json = JsonUtility.ToJson(accountInfo);
        PlayerPrefs.SetString("PlayerAccountInfo", json);
        PlayerPrefs.Save();


        // 5. 로그인 창으로 돌아간다. 이때 아이디는 자동 입력되어 있다.
        _loginInputFields.InputFieldId.text = id;
        OnClickGotoLoginButton();
    }
}
