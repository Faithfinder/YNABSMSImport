using System;

namespace YNABConnector.YNABObjectModel
{
    public class SubTransaction
    {
        public int Amount { get; set; }
        public Guid Category_id { get; set; }
        public bool Deleted { get; set; }
        public Guid Id { get; set; }
        public string Memo { get; set; }
        public Guid Payee_id { get; set; }
        public Guid transaction_id { get; set; }
        public Guid Transfer_account_id { get; set; }
    }
}