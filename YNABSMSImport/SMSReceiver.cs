using Android.App;
using Android.Content;
using Android.Provider;
using Android.Telephony;

namespace YNABSMSImport
{
    [BroadcastReceiver]
    [IntentFilter(new[] {Telephony.Sms.Intents.SmsReceivedAction, "YNABSMSImport.SMSImitation"})]
    public class SMSReceiver : BroadcastReceiver
    {
        public const string INTENT_ACTION = Telephony.Sms.Intents.SmsReceivedAction;

        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.HasExtra("pdus")) return;

            var messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

            foreach (var message in messages) StartProcessing(context, message);
        }

        private static void StartProcessing(Context context, SmsMessage message)
        {
            var smsProcessor = new Intent(context, typeof(SMSProcessor));
            smsProcessor.PutExtra("Address", message.DisplayOriginatingAddress);
            smsProcessor.PutExtra("Message", message.DisplayMessageBody);

            context.StartService(smsProcessor);
        }
    }
}