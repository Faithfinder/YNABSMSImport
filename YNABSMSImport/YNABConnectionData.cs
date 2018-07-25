using System;
using System.Collections.Generic;

namespace YNABSMSImport
{
    internal class YNABConnectionData
    {
        public string AccessToken { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool Expired
        {
            get
            {
                return DateTime.UtcNow >= ExpiresAt;
            }
        }

        public DateTime ExpiresAt { get; private set; }
        public TimeSpan ExpiresIn { get; private set; }
        public string RefreshToken { get; private set; }
        public string Scope { get; private set; }
        public string TokenType { get; private set; }

        public YNABConnectionData(Xamarin.Auth.Account account)
        {
            AccessToken = account.Properties.GetValueOrDefault("access_token", null);
            TokenType = account.Properties.GetValueOrDefault("token_type", null);
            ExpiresIn = TimeSpan.FromSeconds(int.Parse(account.Properties.GetValueOrDefault("expires_in", "0")));
            RefreshToken = account.Properties.GetValueOrDefault("refresh_token", null);
            Scope = account.Properties.GetValueOrDefault("scope", null);
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds(int.Parse(account.Properties.GetValueOrDefault("created_at", "0"))).UtcDateTime;
            ExpiresAt = CreatedAt + ExpiresIn;
        }
    }
}