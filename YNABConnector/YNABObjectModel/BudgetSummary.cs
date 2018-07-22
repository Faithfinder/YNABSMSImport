using System;
using System.Globalization;

namespace YNABConnector.YNABObjectModel
{
    public class BudgetSummary
    {
        public CurrencyFormat Currency_format { get; set; }
        public DateFormat Date_format { get; set; }
        public Guid Id { get; set; }
        public DateTime Last_modified_on { get; set; }
        public string Name { get; set; }
    }
}