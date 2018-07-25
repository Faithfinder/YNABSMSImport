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

namespace YNABSMSImport.ImportSettings
{
    internal class OtkritieBankSettings : IDataProviderSettings
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Sender { get; set; }

        public List<ISMSTemplate> Templates { get; set; }

        public OtkritieBankSettings()
        {
            Id = Guid.NewGuid();
            Name = "@string/bank_oktritie_name";
            Sender = "OTKRITIE";
            var Template = new AddTransactionSMS
            {
                RegExPattern = @"Платёж (?<amount>.*?) р. в (?<Payee>.*?). Карта \*(?<Card>.*?). Доступно (?<Total>.*?) р."
            };
            Templates.Add(Template);
        }

        public void ProcessSMS(string messageText)
        {
            throw new NotImplementedException();
        }
    }
}