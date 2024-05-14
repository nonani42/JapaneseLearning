using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TestSpace
{
    public class LoginPanelView : PanelView
    {
        [SerializeField] private TMP_InputField _loginField;
        [SerializeField] private Button _loginBtn;
        [SerializeField] private TextMeshProUGUI _loginResult;

        public event Action<string> OnNewLogin;

        public void Start()
        {
            _loginBtn.onClick.AddListener(Login);
            _loginResult.enabled = false;
        }

        private void Login()
        {
            if (!LoginIsCorrect(_loginField.text))
                return;
            else
                OnNewLogin?.Invoke(_loginField.text);
        }

        private bool LoginIsCorrect(string login)
        {
            bool res;
            if (string.IsNullOrEmpty(login))
            {
                _loginResult.text = "Enter login";
                res = false;
            }
            else
            {
                _loginResult.text = string.Empty;
                res = true;
            }
            return res;
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
}