using NUnit.Framework;
using System.Collections.Generic;
using YNABConnector;
using YNABConnector.YNABObjectModel;

namespace NUnit.YnabConnectorTests
{
    [TestFixture(TestOf = typeof(YNABClient))]
    public class YNABClientTestReal
    {
        [Test]
        [Category("Real")]
        public void GetAccountsTest()
        {
            var budgets = _ynabClient.GetBudgetsAsync().Result;
            var accounts = _ynabClient.GetAccountsAsync(budgets[0]).Result;

            Assert.That(accounts != null);
        }

        [Test]
        [Category("Real")]
        public void GetBudgetsTest()
        {
            var budgets = _ynabClient.GetBudgetsAsync().Result;

            Assert.That(budgets != null);
        }

        [Test]
        [Category("Real")]
        [Explicit]
        public void PostTransactionTest()
        {
            var budgets = _ynabClient.GetBudgetsAsync().Result;
            var accounts = _ynabClient.GetAccountsAsync(budgets[0]).Result;
            var saveTransaction = new SaveTransaction
            {
                Account_id = accounts[0].Id,
                Amount = 100000,
                Payee_name = "Test Payee"
            };
            var transaction = _ynabClient.PostTransactionAsync(budgets[0], saveTransaction).Result;
            Assert.That(transaction != null);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _ynabClient = YNABClient.GetInstance();
            _ynabClient.RefreshAccessToken(ApiKeys.AccessToken);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            YNABClient.ResetInstance();
        }

        private YNABClient _ynabClient;
    }
}