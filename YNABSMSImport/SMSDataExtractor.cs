using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace YNABSMSImport
{
    internal static class SMSDataExtractor
    {
        public static Dictionary<string, string> ExtractData(string Text)
        {
            var result = new Dictionary<string, string>();
            var AmountPattern = "(?<=Платёж )(.*?)(?= р.)";
            var AmountRegEx = new Regex(AmountPattern);
            var matches = AmountRegEx.Matches(Text);
            if (matches.Count > 0)
            {
                result.Add("Amount", matches[0].Value);
            }
            var PayeePattern = "(?<= р. в )(.*?)(?=. Карта)";
            var PayeeRegEx = new Regex(PayeePattern);
            matches = PayeeRegEx.Matches(Text);
            if (matches.Count > 0)
            {
                result.Add("Payee", matches[0].Value);
            }
            return result;
        }
    }
}