using System;
using System.Collections.Generic;
using System.Text;

namespace YNABConnector.YNABObjectModel
{
    public class TransactionDetail
    {
        public Guid? Account_id { get; set; }
        public string Account_name { get; set; }
        public int Amount { get; set; }
        public bool Approved { get; set; }
        public Guid? Category_id { get; set; }
        public string Category_name { get; set; }
        public TransactionClearedStatus Cleared { get; set; }
        public DateTime Date { get; set; }
        public bool Deleted { get; set; }
        public FlagColor? Flag_color { get; set; }
        public Guid? Id { get; set; }
        public string Import_id { get; set; }
        public string Memo { get; set; }
        public Guid? Payee_id { get; set; }
        public string Payee_name { get; set; }
        public List<SubTransaction> Subtransactions { get; set; }
        public Guid? Transfer_account_id { get; set; }
    }
}