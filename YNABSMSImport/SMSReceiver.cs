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

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.HasExtra("pdus")){
                var messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

                var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

                var i = 0;
                foreach (var message in messages)
                {
                    var DisplayText = $"From: {message.DisplayOriginatingAddress}, Text: {message.DisplayMessageBody}, Number{++i}";

                    var handler = new Handler();
                    handler.Post(() => {
                        Toast.MakeText(context, DisplayText, ToastLength.Short).Show();
                    });

                    

                    var notification = new Notification.Builder(context)
                        .SetContentTitle("SMS")
                        .SetContentText(DisplayText)
                        .SetSmallIcon(Resource.Drawable.ic_stat_beach_access).Build();


                    notificationManager.Notify(i, notification);
                }
            }
        }
    }
}