using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using YNABConnector;
using YNABConnector.YNABObjectModel;
using YNABConnector.Exceptions;
using System;

namespace NUnit.YnabConnectorTests
{
    [TestFixture(TestOf = typeof(YNABClient))]
    public class YNABClientTestReal
    {
        [Test]
        [Category("Real")]
        public void GetAccountsTest()
        {
            var budgets = ynabClient.GetBudgetsAsync().Result;
            var accounts = ynabClient.GetAccountsAsync(budgets[0]).Result;

            Assert.That(accounts is List<Account>);
        }

        [Test]
        [Category("Real")]
        public void GetBudgetsTest()
        {
            var budgets = ynabClient.GetBudgetsAsync().Result;

            Assert.That(budgets is List<BudgetSummary>);
        }

        [Test]
        [Category("Real")]
        public void PostTransactionTest()
        {
            var budgets = ynabClient.GetBudgetsAsync().Result;
            var accounts = ynabClient.GetAccountsAsync(budgets[0]).Result;
            var saveTransaction = new SaveTransaction
            {
                Account_id = accounts[0].Id,
                Amount = 100000,
                Payee_name = "Test Payee"
            };
            var transaction = ynabClient.PostTransactionAsync(budgets[0], saveTransaction).Result;
            Assert.That(transaction is TransactionDetail);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            ynabClient = YNABClient.GetInstance();
            ynabClient.RefreshAccessToken(ApiKeys.AccessToken);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            YNABClient.ResetInstance();
        }

        private YNABClient ynabClient;
    }
}