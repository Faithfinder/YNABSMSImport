using System;
using System.Collections.Generic;

using Xamarin.Auth;

namespace YNABSMSImport
{
    internal class YNABConnectionData
    {
        public YNABConnectionData(Account account)
        {
            AccessToken = account.Properties.GetValueOrDefault("access_token", null);
            TokenType = account.Properties.GetValueOrDefault("token_type", null);
            ExpiresIn = TimeSpan.FromSeconds(int.Parse(account.Properties.GetValueOrDefault("expires_in", "0")));
            RefreshToken = account.Properties.GetValueOrDefault("refresh_token", null);
            Scope = account.Properties.GetValueOrDefault("scope", null);
            CreatedAt = DateTimeOffset
                .FromUnixTimeSeconds(int.Parse(account.Properties.GetValueOrDefault("created_at", "0"))).UtcDateTime;
            ExpiresAt = CreatedAt + ExpiresIn;
        }

        public string AccessToken { get; }

        public DateTime CreatedAt { get; }

        public bool Expired
        {
            get { return DateTime.UtcNow >= ExpiresAt; }
        }

        public DateTime ExpiresAt { get; }

        public TimeSpan ExpiresIn { get; }

        public string RefreshToken { get; }

        public string Scope { get; }

        public string TokenType { get; }
    }
}