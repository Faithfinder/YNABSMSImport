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

namespace YNABSMSImport
{
    [Service]
    class SMSProcessor : IntentService
    {
        public SMSProcessor() : base("SMSProcessor")
        {
        }

        protected override void OnHandleIntent(Intent intent)
        {
            var DisplayText = $"From: {intent.GetStringExtra("Address")}, Text: {intent.GetStringExtra("Message")}";

            var handler = new Handler();
            handler.Post(() => {
                Toast.MakeText(this, DisplayText, ToastLength.Short).Show();

                var notification = new Notification.Builder(this)
                    .SetContentTitle("SMS")
                    .SetContentText(DisplayText)
                    .SetSmallIcon(Resource.Drawable.ic_stat_beach_access).Build();

                var notificationManager = GetSystemService(NotificationService) as NotificationManager;
                notificationManager.Notify(0, notification);
            });

            
        }
    }
}