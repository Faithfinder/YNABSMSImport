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

        public NumberFormatInfo ToNumberFormatInfo()
        {
            var NumberFormatInfo = new NumberFormatInfo
            {
                CurrencyDecimalDigits = decimal_digits,
                CurrencyDecimalSeparator = decimal_separator,
                CurrencyGroupSeparator = group_separator,
                CurrencySymbol = display_symbol ? currency_symbol : ""
            };
            if (symbol_first)
            {
                NumberFormatInfo.CurrencyNegativePattern = (int)CurrencyNegativePatterns.SignSymbolAmount;
                NumberFormatInfo.CurrencyPositivePattern = (int)CurrencyPositivePatterns.SymbolAmount;
            }
            else
            {
                NumberFormatInfo.CurrencyNegativePattern = (int)CurrencyNegativePatterns.SignAmountSymbol;
                NumberFormatInfo.CurrencyPositivePattern = (int)CurrencyPositivePatterns.AmountSymbol;
            }

            return NumberFormatInfo;
        }
    }
}