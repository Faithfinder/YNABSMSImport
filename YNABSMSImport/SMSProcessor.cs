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
using YNABConnector;
using YNABConnector.YNABObjectModel;

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
                var data = SMSDataExtractor.ExtractData(message);
                var ynabClient = YNABClient.GetInstance();
                ynabClient.RefreshAccessToken(ApiKeys.AccessToken);
                try
                {
                    ynabClient.PostTransactionAsync(GetBudgetSummary(),
                        SaveTransaction(data.GetValueOrDefault("Amount", "0"), data.GetValueOrDefault("Payee", "Test")));
                }
                catch
                {
                    NotifyUser("Some error");
                }
            }
        private static (string address, string message) ExtractSMSDataFromIntent(Intent intent)
        {
            return (intent.GetStringExtra("Address"), intent.GetStringExtra("Message"));
        }

        private BudgetSummary GetBudgetSummary()
        {
            return new BudgetSummary { Id = Guid.Parse("7ffba7b3-81b2-4ce1-a546-1b176368cc1b") };
        }

        private void NotifyUser(string DisplayText)
        {
            var handler = new Handler();
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

        private SaveTransaction SaveTransaction(string Amount, string Payee)
        {
            return new SaveTransaction
            {
                Account_id = Guid.Parse("126cda04-98fb-f0bf-1760-b96a7fa27d42"),
                Amount = (int)(float.Parse(Amount) * -1000),
                Payee_name = Payee
            };
        }
    }
}