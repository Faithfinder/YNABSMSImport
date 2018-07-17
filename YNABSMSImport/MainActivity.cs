using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android;
using Android.Content.PM;

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
            var btnSMSImitator = FindViewById<Button>(Resource.Id.btnSMSImitator);
            btnSMSImitator.Click += BtnSMSImitator_Click;
        }

        private void BtnSMSImitator_Click(object sender, System.EventArgs e)
        {
            Intent message = new Intent("YNABSMSImport.SMSImitation");
            
            message.PutExtra("pdus22", "value");
            SendBroadcast(message);
        }
    }
}

