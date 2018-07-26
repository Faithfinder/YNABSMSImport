using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace YNABSMSImport
{
    internal static class SMSDataExtractor
    {
        public static (string Amount, string Payee) ExtractData(string Text)
        {
            var result = (Amount: "", Payee: "");
            var Pattern = @"Платёж (?<amount>.*?) р. в (?<payee>.*?). Карта \*(?<card>.*?). Доступно (?<total>.*?) р.";
            var RegEx = new Regex(Pattern);
            var match = RegEx.Match(Text);
            if (match.Success)
            {
                result = (match.Groups["amount"].Value, match.Groups["payee"].Value);
            }
            return result;
        }
    }
}