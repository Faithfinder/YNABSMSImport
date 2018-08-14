using System.Collections.Generic;

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
            var gluedMessages = GlueMessagesTogether(messages);

            foreach (var message in gluedMessages) StartProcessing(context, message);
        }

        private static void StartProcessing(Context context, KeyValuePair<string,string> message)
        {
            var smsProcessor = new Intent(context, typeof(SMSProcessor));
            smsProcessor.PutExtra("Address", message.Key);
            smsProcessor.PutExtra("Message", message.Value);

            context.StartService(smsProcessor);
        }

        private static Dictionary<string, string> GlueMessagesTogether(IEnumerable<SmsMessage> messages)
        {
            var result = new Dictionary<string,string>();

            foreach (var smsMessage in messages)
            {
                if (!result.ContainsKey(smsMessage.DisplayOriginatingAddress))
                {
                    result.Add(smsMessage.DisplayOriginatingAddress, smsMessage.DisplayMessageBody);
                }
                else
                {
                    result[smsMessage.DisplayOriginatingAddress] += smsMessage.DisplayMessageBody;
                }
            }

            return result;
        }
    }
}