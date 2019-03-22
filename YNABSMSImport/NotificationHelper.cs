
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace YNABSMSImport
{
    internal static class NotificationHelper
    {
        private static readonly Handler handler;
        static NotificationHelper()
        {
            handler = new Handler(Looper.MainLooper);
        }

        internal static void ShowToast(Context context, string message, ToastLength length = ToastLength.Short)
        {
            handler.Post(() =>
            {
                Toast.MakeText(context, message, length).Show();
            });
        }

        internal static void ShowNotification(string message)
        {
            handler.Post(() =>
            {
                var notification = new Notification.Builder(Application.Context, "Don't need now")
                    .SetContentTitle("YNAB SMS Import")
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Drawable.ic_stat_beach_access)
                    .Build();

                var notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager?.Notify(0, notification);
            });
        }
    }
}