
using Android.App;
using Android.Content;
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
            if (intent.HasExtra("pdus"))
            {
                var messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

                foreach (var message in messages)
                {
                    StartProcessing(context, message);
                }
            }
        }

        private void StartProcessing(Context context, Android.Telephony.SmsMessage message)
        {
            Intent smsProcessor = new Intent(context, typeof(SMSProcessor));
            smsProcessor.PutExtra("Address", message.DisplayOriginatingAddress);
            smsProcessor.PutExtra("Message", message.DisplayMessageBody);

            context.StartService(smsProcessor);
        }
    }
}