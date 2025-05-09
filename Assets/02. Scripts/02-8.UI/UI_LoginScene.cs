using System;
using System.Text;
using System.Security.Cryptography;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private const string PREFIX = "ID_";
    private const string SALT = "20010626";

    private void Awake()
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);
        LoginCheck();
    }

    public void OnClickGotoRegisterButton()
    {
        _loginPanel.SetActive(false);
        _registerPanel.SetActive(true);
        _loginInputFields.ResultTMP.text = "";
        _registerInputFields.ResultTMP.text = "";
    }

    public void OnClickGotoLoginButton()
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);
        _loginInputFields.ResultTMP.text = "";
        _registerInputFields.ResultTMP.text = "";
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
        PlayerPrefs.SetString(PREFIX + id, Encryption(password + SALT));
        //PlayerPrefs.SetString("Id", id);
        //PlayerPrefs.SetString("Password", password);

        // PlayerAccountInfo로 클래스화하여 저장
        //PlayerAccountInfo accountInfo = new PlayerAccountInfo
        //{
        //    Id = id,
        //    Password = password
        //};

        //string json = JsonUtility.ToJson(accountInfo);
        //PlayerPrefs.SetString("PlayerAccountInfo", json);
        //PlayerPrefs.Save();


        // 5. 로그인 창으로 돌아간다. 이때 아이디는 자동 입력되어 있다.
        _loginInputFields.InputFieldId.text = id;
        _loginInputFields.ResultTMP.text = "";
        OnClickGotoLoginButton();
    }

    public void Login()
    {
        // 1. 아이디 입력을 확인한다.
        string id = _loginInputFields.InputFieldId.text;
        if (string.IsNullOrEmpty(id))
        {
            _loginInputFields.ResultTMP.text = "아이디를 입력해주세요.";
            return;
        }

        // 2. 비밀번호 입력을 확인한다.
        string password = _loginInputFields.InputFieldPassword.text;
        if (string.IsNullOrEmpty(password))
        {
            _loginInputFields.ResultTMP.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 3. PlayerPrefs.Get을 이용해서 아이디와 비밀번호가 맞는지 확인한다.
        if (!PlayerPrefs.HasKey(PREFIX + id))
        {
            _loginInputFields.ResultTMP.text = "아이디 또는 비밀번호가 일치하지 않습니다.";
            return;
        }

        string hashedPassword = PlayerPrefs.GetString(PREFIX + id);
        if (hashedPassword != Encryption(password + SALT))
        {
            _loginInputFields.ResultTMP.text = "아이디 또는 비밀번호가 일치하지 않습니다.";
            return;
        }

        // 4. 로그인 성공
        _loginInputFields.ResultTMP.text = "로그인 성공!";
        SceneManager.LoadScene(1);
    }

    public void LoginCheck()
    {
        string id = _loginInputFields.InputFieldId.text;
        string password = _loginInputFields.InputFieldPassword.text;

        _loginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }

    public string Encryption(string text)
    {
        // 해시 암호화 알고리즘 인스턴스 생성
        SHA256 sha256 = SHA256.Create();

        // OS 혹은 프로그래밍 언어 별로 string을 표현하는 방식이 다 다르므로,
        // UTF8 버전 바이트 배열로 바꿔줘야 한다.
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = sha256.ComputeHash(bytes);

        string resultText = string.Empty;
        foreach (byte b in hash)
        {
            // byte를 다시 string으로 바꿔서 이어붙이기
            resultText += b.ToString("X2");
        }
        return resultText;
    }
}
