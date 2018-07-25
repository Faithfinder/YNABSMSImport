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
            var Pattern = @"Платёж (?<amount>.*?) р. в (?<payee>.*?). Карта \*(?<card>.*?). Доступно (?<total>.*?) р.";
            var RegEx = new Regex(Pattern);
            var match = RegEx.Match(Text);
            if (match.Success)
            {
                result.Add("Amount", match.Groups["amount"].Value);
                result.Add("Payee", match.Groups["payee"].Value);
            }
            return result;
        }
    }
}