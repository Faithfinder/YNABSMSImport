using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using YNABConnector;
using YNABConnector.YNABObjectModel;

namespace YNABSMSImport.ImportSettings
{
    internal class AddTransactionBehaviour : ITemplateBehaviour
    {
        private readonly AddTransaction _parentTemplate;

        public AddTransactionBehaviour(AddTransaction parentTemplate)
        {
            _parentTemplate = parentTemplate;
        }

        private (string Amount, string Payee) ExtractData(string message)
        {
            var regEx = new Regex(_parentTemplate.UserToRegEx());
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

        public async Task ProcessMessage(string message)
        {

            var (amount, payee) = ExtractData(message);
            var transaction = SaveTransaction(amount, payee, _parentTemplate.AccountID);
            var client = YNABClient.GetInstance();
            client.RefreshAccessToken(ApiKeys.AccessToken);
            await client.PostTransactionAsync(_parentTemplate.BudgetID, transaction);
        }
    }
}