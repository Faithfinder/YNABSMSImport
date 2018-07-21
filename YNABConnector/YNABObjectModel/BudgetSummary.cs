using System;
using System.Globalization;

namespace YNABConnector.YNABObjectModel
{
    public class BudgetSummary
    {
        public CurrencyFormat currency_format;
        public DateFormat date_format;
        public Guid id;
        public DateTime last_modified_on;
        public string name;

        public CultureInfo ExtractCultureInfo()
        {
            var result = currency_format.ToCultureInfo();
            return result;
        }
    }
}