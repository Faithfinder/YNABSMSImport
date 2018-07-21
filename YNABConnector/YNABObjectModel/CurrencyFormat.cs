using System.Globalization;

namespace YNABConnector.YNABObjectModel
{
    public class CurrencyFormat
    {
        public string currency_symbol;
        public int decimal_digits;
        public string decimal_separator;
        public bool display_symbol;
        public string example_format;
        public string group_separator;
        public string iso_code;
        public bool symbol_first;

        public CultureInfo ToCultureInfo()
        {
            var result = new CultureInfo(iso_code);
            result.NumberFormat.CurrencyDecimalDigits = decimal_digits;
            result.NumberFormat.CurrencyDecimalSeparator = decimal_separator;
            result.NumberFormat.CurrencyGroupSeparator = group_separator;
            result.NumberFormat.CurrencySymbol = display_symbol ? currency_symbol : "";
            if (symbol_first)
            {
                result.NumberFormat.CurrencyNegativePattern = (int)CurrencyNegativePatterns.SignSymbolAmount;
                result.NumberFormat.CurrencyPositivePattern = (int)CurrencyPositivePatterns.SymbolAmount;
            }
            else
            {
                result.NumberFormat.CurrencyNegativePattern = (int)CurrencyNegativePatterns.SignAmountSymbol;
                result.NumberFormat.CurrencyPositivePattern = (int)CurrencyPositivePatterns.AmountSymbol;
            }

            return result;
        }
    }
}