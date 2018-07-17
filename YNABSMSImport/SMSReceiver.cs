using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;

namespace YNABSMSImport
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Telephony.Sms.Intents.SmsReceivedAction, "YNABSMSImport.SMSImitation" })]
    public class SMSReceiver : BroadcastReceiver
    {
        public const string INTENT_ACTION = Telephony.Sms.Intents.SmsReceivedAction;
        private static readonly int NotificationId = 1934;

        public override void OnReceive(Context context, Intent intent)
        {
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(context)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetSmallIcon(Resource.Drawable.ic_stat_beach_access);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:            
            notificationManager.Notify(NotificationId, notification);
        }
    }
}