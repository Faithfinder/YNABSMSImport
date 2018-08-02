using System;
using System.Collections.Generic;
using System.Linq;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using Xamarin.Auth;

using YNABConnector;

using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            AcquirePermissions();
            SettingsManager.SaveSetting(UserSetting.TemporaryOtkritie());

            var btnAuthorize = FindViewById<Button>(Resource.Id.btnAuthorize);
            btnAuthorize.Click += BtnAuthorize_Click;

            var btnManageSettings = FindViewById<Button>(Resource.Id.btnManageSettings);
            btnManageSettings.Click += BtnManageSettings_Click;
        }

        private static YNABConnectionData ExtractConnectionData(Account[] accounts)
        {
            YNABConnectionData result = null;
            if (accounts.Any()) result = new YNABConnectionData(accounts.First());
            return result;
        }

        private static OAuth2Authenticator GetConfiguredAuthenticator()
        {
            return new OAuth2Authenticator
            (
                ApiKeys.ClientID,
                ApiKeys.ClientSecret,
                "",
                new Uri(YNABPaths.Authorization),
                accessTokenUrl: new Uri(YNABPaths.AuthCodeGrantFlow),
                redirectUrl: new Uri("https://localhost")
            );
        }

        private static void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            // UI presented, so it's up to us to dimiss it on Android
            // dismiss Activity with WebView or CustomTabs
            //Finish();

            if (eventArgs.IsAuthenticated)
            {
                Console.WriteLine("Authenticated with Ynab");
                AccountStore.Create("password").Save(eventArgs.Account, "Ynab");
            }
            else
            {
                Console.WriteLine("YNAB Authentication didn't pass");
            }
        }

        private void AcquirePermissions()
        {
            if (CheckSelfPermission(Manifest.Permission.ReceiveSms) != Permission.Granted)
                RequestPermissions(new[] { Manifest.Permission.ReceiveSms }, 0);
        }

        private void BtnAuthorize_Click(object sender, EventArgs e)
        {
            var accounts = AccountStore.Create("password").FindAccountsForService("Ynab").ToArray();
            var connectionData = ExtractConnectionData(accounts);
            var authenticationActive = !connectionData?.Expired ?? false;
            if (!authenticationActive)
            {
                var auth = GetConfiguredAuthenticator();

                auth.Completed += OnAuthCompleted;

                StartActivity(auth.GetUI(this));
            }
            else
            {
                Toast.MakeText(this, "Already authenticated!", ToastLength.Long).Show();
            }
        }

        private void BtnManageSettings_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ManageSettingsActivity));
            StartActivity(intent);
        }
    }
}