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

        }

        [OneTimeSetUp]
        public void Setup()
        {
            handler = new HttpClientHandler();
            ynabClient = YNABClient.GetInstance(handler);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            YNABClient.ResetInstance();
        }

        private HttpClientHandler handler;
        private YNABClient ynabClient;
    }
}