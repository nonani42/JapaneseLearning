using PlayFab.ClientModels;
using PlayFab;
using System;

namespace TestSpace
{
    public class LoginController
    {
        private const string TITLE_ID = "C3CA7";

        private LoginPanelView _playFabLoginPanelView;
        private UserInfoModel _userInfoModel = new();

        private string _playFabUserID;

        public string Username { get => _userInfoModel.Login; }
        public string UserID { get => _playFabUserID; }

        public event Action<string> OnSuccessfulAuth;

        public LoginController(LoginPanelView playFabLoginPanelView)
        {
            _playFabLoginPanelView = playFabLoginPanelView;
            _playFabLoginPanelView.Hide();
            _playFabLoginPanelView.OnNewLogin += LoginWithUsername;
        }

        public void InitAuth()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
                PlayFabSettings.staticSettings.TitleId = TITLE_ID;

            if (_userInfoModel.AccountExists)
                LoginWithUsername(_userInfoModel.Login);
            else
                _playFabLoginPanelView.Show();
        }

        private void LoginWithUsername(string username)
        {
            _userInfoModel.SaveLogin(username);
            var request = new LoginWithCustomIDRequest
            {
                CustomId = username,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            _playFabLoginPanelView.ShowSuccessResult();
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnAccountInfoSuccess, OnAccountInfoFailure);
        }

        private void OnLoginFailure(PlayFabError error) => _playFabLoginPanelView.ShowFailureResult(error.GenerateErrorReport());

        private void OnAccountInfoSuccess(GetAccountInfoResult result)
        {
            _playFabLoginPanelView.Hide();
            _playFabUserID = result.AccountInfo.PlayFabId;
            OnSuccessfulAuth?.Invoke(_userInfoModel.Login);
        }

        private void OnAccountInfoFailure(PlayFabError error) => _playFabLoginPanelView.ShowFailureResult(error.GenerateErrorReport());

        public void Destroy()
        {
            _playFabLoginPanelView.OnNewLogin -= LoginWithUsername;
        }
    }
}
