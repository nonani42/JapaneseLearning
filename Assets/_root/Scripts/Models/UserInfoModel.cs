using UnityEngine;

namespace TestSpace
{
    internal class UserInfoModel
    {
        private const string AuthUsernameKey = "authorization-guid";

        public bool AccountExists { get => PlayerPrefs.HasKey(AuthUsernameKey); }

        public string Login => AccountExists ? PlayerPrefs.GetString(AuthUsernameKey) : string.Empty;

        public void SaveLogin(string username) => PlayerPrefs.SetString(AuthUsernameKey, username);
    }
}
