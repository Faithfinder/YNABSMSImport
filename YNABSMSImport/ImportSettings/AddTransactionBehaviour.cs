using System;
using System.Text.RegularExpressions;
using YNABConnector.YNABObjectModel;
using YNABConnector;

namespace YNABSMSImport.ImportSettings
{
    internal class AddTransactionBehaviour : ITemplateBehaviour
    {
        public void ProcessMessage(string message, SMSTemplate template)
        {
            if (template is AddTransaction processingTemplate)
            {
                var (Amount, Payee) = ExtractData(message, processingTemplate);
                var transaction = SaveTransaction(Amount, Payee, processingTemplate.AccountID);
                var client = YNABClient.GetInstance();
                client.RefreshAccessToken(ApiKeys.AccessToken);
                var postTask = client.PostTransactionAsync(processingTemplate.BudgetID, transaction);
            }
            else
            {
                throw new ArgumentException("Wrong behaviour for template!");
            }
        }

        private static (string Amount, string Payee) ExtractData(string message, AddTransaction processingTemplate)
        {
            var RegEx = new Regex(processingTemplate.UserToRegEx());
            var match = RegEx.Match(message);
            if (match.Success)
            {
                return (match.Groups["amount"].Value, match.Groups["payee"].Value);
            }
            else
            {
                return ("0", "");
            }
        }

        private SaveTransaction SaveTransaction(string amount, string payee, Guid accountID)
        {
            return new SaveTransaction
            {
                Account_id = accountID,
                Amount = (int)(float.Parse(amount) * -1000),
                Payee_name = payee
            };
        }
    }
}