using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using YNABConnector;
using YNABConnector.YNABObjectModel;

namespace YNABSMSImport.ImportSettings
{
    internal class AddTransactionBehaviour : ITemplateBehaviour
    {
        private static (string Amount, string Payee) ExtractData(string message, SMSTemplate processingTemplate)
        {
            var regEx = new Regex(processingTemplate.UserToRegEx());
            var match = regEx.Match(message);
            return match.Success ? (match.Groups["amount"].Value, match.Groups["payee"].Value) : ("0", "");
        }

        private static SaveTransaction SaveTransaction(string amount, string payee, Guid accountID)
        {
            return new SaveTransaction
            {
                Account_id = accountID,
                Amount = -1 * amount.FloatParseAdvanced().ToMilliunits(),
                Payee_name = payee
            };
        }

        public async Task ProcessMessage(string message, SMSTemplate template)
        {
            if (template is AddTransaction processingTemplate)
            {
                var (amount, payee) = ExtractData(message, processingTemplate);
                var transaction = SaveTransaction(amount, payee, processingTemplate.AccountID);
                var client = YNABClient.GetInstance();
                client.RefreshAccessToken(ApiKeys.AccessToken);
                await client.PostTransactionAsync(processingTemplate.BudgetID, transaction);
            }
            else
            {
                throw new ArgumentException("Wrong behaviour for template!");
            }
        }
    }
}