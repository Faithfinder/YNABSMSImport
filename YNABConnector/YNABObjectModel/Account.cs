using System;

namespace YNABConnector.YNABObjectModel
{
    public class Account
    {
        public int Balance { get; set; }
        public int Cleared_balance { get; set; }
        public bool Closed { get; set; }
        public bool Deleted { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool On_budget { get; set; }
        public AccountTypes Type { get; set; }
        public int Uncleared_balance { get; set; }
    }
}