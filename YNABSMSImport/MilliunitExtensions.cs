using System.Text.RegularExpressions;

namespace YNABSMSImport
{
    internal static class MilliunitExtensions
    {
        public static float FloatParseAdvanced(this string strToParse, char decimalSymbol = ',')
        {
            var regExMatch = Regex.Match(strToParse, @"([-]?[0-9]+)([\s])?([0-9]+)?[." + decimalSymbol + "]?([0-9 ]+)?([0-9]+)?").Value;

            if (regExMatch.Length <= 0 || !strToParse.Contains(regExMatch)) return 0;

            var currDecSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            regExMatch = regExMatch.Replace(".", currDecSeparator).Replace(decimalSymbol.ToString(), currDecSeparator);

            return float.Parse(regExMatch);

        }

        public static int ToMilliunits(this float amount)
        {
            return (int)(amount * 1000);
        }
    }
}