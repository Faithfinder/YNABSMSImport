using System;

namespace YNABSMSImport.ImportSettings
{
    internal class AddTransaction : SMSTemplate
    {
        public Guid AccountID { get; set; }

        public Guid BudgetID { get; set; }

        public AddTransaction() => Behaviour = new AddTransactionBehaviour(this);
    }
}