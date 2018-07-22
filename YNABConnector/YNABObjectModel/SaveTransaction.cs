using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YNABConnector.YNABObjectModel
{
    public class SaveTransaction
    {
        public Guid? Account_id { get; set; }

        //Milliunits to float?
        //public float Amount { get => amount / 1000; set => amount = (int)(value * 1000); }
        public int Amount { get; set; }

        public bool Approved { get; set; }

        public Guid? Category_id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionClearedStatus Cleared { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FlagColor? Flag_color { get; set; }

        public Guid? Import_id { get; set; }

        public string Memo { get; set; }

        public Guid? Payee_id { get; set; }

        public string Payee_name { get; set; }

        public SaveTransaction()
        {
            Date = DateTime.Now.Date;
            Approved = false;
            Cleared = TransactionClearedStatus.uncleared;
            //Flag_color = null;
        }

        public SaveTransactionWrapper Wrap()
        {
            return new SaveTransactionWrapper()
            {
                Transaction = this
            };
        }
    }
}