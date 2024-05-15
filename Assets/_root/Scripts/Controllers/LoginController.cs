using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine;

namespace TestSpace
{
    public class LoginController
    {
        private LoginPanelView _loginPanelView;
        private UserInfoModel _userInfoModel = new();

        private GetUserDataResult _result;

        public GetUserDataResult Result { get => _result; }

        public string Username { get => _userInfoModel.Login; }
        private string Password { get => _userInfoModel.Pass; }

        public event Action<string> OnSuccessfulAuth;

        public LoginController(LoginPanelView playFabLoginPanelView)
        {
            _loginPanelView = playFabLoginPanelView;
            _loginPanelView.Hide();
            _loginPanelView.OnNewUser += LoginWithUsername;
        }

        public void InitAuth()
        {
            _loginPanelView.Show();

            if (_userInfoModel.AccountExists)
            {
                _loginPanelView.FillInSavedCredentials(Username, Password);
                LoginWithUsername(Username, Password);
            }
            else
            {
                _loginPanelView.GetNewUser();
            }
        }

        private void LoginWithUsername(string username, string pass)
        {
            _userInfoModel.SaveLogin(username);
            _userInfoModel.SavePass(pass);
            var request = new LoginWithPlayFabRequest
            {
                Username = username,
                Password = pass,
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult result) => PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnAccountInfoSuccess, OnAccountInfoFailure);

        private void OnLoginFailure(PlayFabError error)
        {
            _loginPanelView.ShowFailureResult(error.GenerateErrorReport());
            var request = new RegisterPlayFabUserRequest
            {
                Username = _userInfoModel.Login,
                Password = _userInfoModel.Pass,
                RequireBothUsernameAndEmail = false,
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            _loginPanelView.ShowSuccessResult();
            LoginWithUsername(Username, Password);
        }

        private void OnRegisterFailure(PlayFabError error) => _loginPanelView.ShowFailureResult(error.GenerateErrorReport());

        private void OnAccountInfoSuccess(GetAccountInfoResult result) => GetData(result.AccountInfo.PlayFabId);

        private void OnAccountInfoFailure(PlayFabError error) => _loginPanelView.ShowFailureResult(error.GenerateErrorReport());

        public void GetData(string playFabID)
        {
            var request = new GetUserDataRequest { PlayFabId = playFabID };
            PlayFabClientAPI.GetUserData(request, OnGetDataSuccess, OnGetDataFailure);
        }

        private void OnGetDataSuccess(GetUserDataResult result)
        {
            _result = result;
            OnSuccessfulAuth?.Invoke(_userInfoModel.Login);
            _loginPanelView.Hide();
        }

        private void OnGetDataFailure(PlayFabError error) => Debug.Log($"{error.GenerateErrorReport()}");

        public void Destroy() => _loginPanelView.OnNewUser -= LoginWithUsername;
    }
}
