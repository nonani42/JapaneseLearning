using System;
using TestSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelView : PanelView
{
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passField;
    [SerializeField] private Button _loginBtn;
    [SerializeField] private TextMeshProUGUI _loginResult;

    public event Action<string, string> OnNewUser;

    private void Start()
    {
        _loginBtn.onClick.AddListener(Login);
        _loginResult.enabled = false;
    }

    private void Login()
    {
        if (!StringIsCorrect(_loginField.text)|| !StringIsCorrect(_passField.text))
            return;
        else
            OnNewUser?.Invoke(_loginField.text, _passField.text);
    }

    private bool StringIsCorrect(string str)
    {
        bool res;
        if (string.IsNullOrEmpty(str))
        {
            _loginResult.text = "Incorrect data";
            res = false;
        }
        else
        {
            _loginResult.text = string.Empty;
            res = true;
        }
        return res;
    }

    public void FillInSavedCredentials(string login, string pass)
    {
        _loginField.text = login;
        _passField.text = pass;
        _loginBtn.interactable = false;
    }

    public void GetNewUser()
    {
        _loginField.text = string.Empty;
        _passField.text = string.Empty;
        _loginBtn.interactable = true;
    }

    public void ShowSuccessResult() => ShowResult(Color.green, "Success");

    public void ShowFailureResult(string error) => ShowResult(Color.red, error);

    private void ShowResult(Color color, string text)
    {
        _loginResult.enabled = true;
        _loginResult.color = color;
        _loginResult.text = text;
    }
}
