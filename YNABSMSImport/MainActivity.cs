using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android;
using Android.Content.PM;
using Xamarin.Auth;
using System;
using YNABConnector;

namespace YNABSMSImport
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            if (CheckSelfPermission(Manifest.Permission.ReceiveSms) != Permission.Granted)
            {
                RequestPermissions(new[] { Manifest.Permission.ReceiveSms }, 0);
            }
            var btnAuthorize = FindViewById<Button>(Resource.Id.btnAuthorize);
            btnAuthorize.Click += BtnbtnAuthorize_Click;
        }

        private void BtnbtnAuthorize_Click(object sender, EventArgs e)
        {
            OAuth2Authenticator auth = new OAuth2Authenticator
                (
                    clientId: ApiKeys.ClientID,
                    scope: "",
                    authorizeUrl: new Uri("https://app.youneedabudget.com/oauth/authorize"),
                    redirectUrl: new Uri("https://localhost")

                );

            auth.Completed += (sender1, eventArgs1) =>
            {
                // UI presented, so it's up to us to dimiss it on Android
                // dismiss Activity with WebView or CustomTabs
                //Finish();

                if (eventArgs1.IsAuthenticated)
                {
                    var handler = new Handler();
                    handler.Post(() => { Toast.MakeText(this, "Authenticated", ToastLength.Long); });
                }
                else
                {
                    // The user cancelled
                }
            };

            StartActivity(auth.GetUI(this));
        }
    }
}