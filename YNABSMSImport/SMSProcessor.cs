using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using YNABSMSImport.ImportSettings;

namespace YNABSMSImport
{
    [Service]
    internal class SMSProcessor : IntentService
    {
        public SMSProcessor() : base("SMSProcessor")
        {
        }

        protected override void OnHandleIntent(Intent intent)
        {
            (string address, string message) = ExtractSMSDataFromIntent(intent);

            var DisplayText = $"From: {address}, Text: {message}";
            NotifyUser(DisplayText);
            if (address == "OTKRITIE" || address == "InternetSMS")
            {
                IDataProviderSettings setting = UserSettings.TemporaryOtkritie(); //TODO SettingsManager.GetSetting(address);
                var template = setting.ChooseTemplate(message);
                template.ProcessMessage(message);
            }
        }

        private (string address, string message) ExtractSMSDataFromIntent(Intent intent)
        {
            return (intent.GetStringExtra("Address"), intent.GetStringExtra("Message"));
        }

        private void NotifyUser(string DisplayText)
        {
            var handler = new Handler(Looper.MainLooper);
            handler.Post(() =>
            {
                Toast.MakeText(Application.Context, DisplayText, ToastLength.Long).Show();

                var notification = new Notification.Builder(Application.Context)
                    .SetContentTitle("YNABImport")
                    .SetContentText(DisplayText)
                    .SetSmallIcon(Resource.Drawable.ic_stat_beach_access).Build();

                var notificationManager = Application.Context.GetSystemService(NotificationService) as NotificationManager;
                notificationManager.Notify(0, notification);
            });
        }
    }
}