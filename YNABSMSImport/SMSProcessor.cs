using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
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

            var setting = await new SettingsManager().FindActiveSettingBySenderAsync(address);
            setting?.ProcessMessage(message);
        }

        private static (string address, string message) ExtractSMSDataFromIntent(Intent intent)
        {
            return (intent.GetStringExtra("Address"), intent.GetStringExtra("Message"));
        }

        private static void NotifyUser(string displayText)
        {
            try
            {
                NotificationHelper.ShowToast(Application.Context, displayText, ToastLength.Short);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}