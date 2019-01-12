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

        protected override async void OnHandleIntent(Intent intent)
        {
            var (address, message) = ExtractSMSDataFromIntent(intent);

            var displayText = $"From: {address}, Text: {message}";
            NotifyUser(displayText);
            var setting = await new SettingsManager().FindSettingBySenderAsync(address);

            setting?.ProcessMessage(message);
        }

        private static (string address, string message) ExtractSMSDataFromIntent(Intent intent)
        {
            return (intent.GetStringExtra("Address"), intent.GetStringExtra("Message"));
        }

        private static void NotifyUser(string displayText)
        {
            var handler = new Handler(Looper.MainLooper);
            handler.Post(() =>
            {
                Toast.MakeText(Application.Context, displayText, ToastLength.Long).Show();

                var notification = new Notification.Builder(Application.Context, "Don't need now")
                    .SetContentTitle("YNABImport")
                    .SetContentText(displayText)
                    .SetSmallIcon(Resource.Drawable.ic_stat_beach_access).Build();

                var notificationManager = Application.Context.GetSystemService(NotificationService) as NotificationManager;
                notificationManager?.Notify(0, notification);
            });
        }
    }
}