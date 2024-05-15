using UnityEngine;

namespace TestSpace
{
    internal class UserInfoModel
    {
        private const string AuthUsernameKey = "authorizationLogin-guid";
        private const string AuthPassKey = "authorizationPass-guid";

        public bool AccountExists { get => PlayerPrefs.HasKey(AuthUsernameKey) && PlayerPrefs.HasKey(AuthPassKey); }

        public string Login => AccountExists ? PlayerPrefs.GetString(AuthUsernameKey) : string.Empty;
        public string Pass => AccountExists ? PlayerPrefs.GetString(AuthPassKey) : string.Empty;

        public void SaveLogin(string username) => PlayerPrefs.SetString(AuthUsernameKey, username);
        public void SavePass(string password) => PlayerPrefs.SetString(AuthPassKey, password);
    }
}
