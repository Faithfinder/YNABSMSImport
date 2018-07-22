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
    public class YNABClientTestMock
    {
        [Test]
        [Category("Mocked")]
        public void ErrorUnauthorized()
        {
            handler.QueueResponse(MockResponseHandlers.Unauthorized);
            Assert.ThrowsAsync<AuthorizationException>(async () => await ynabClient.GetBudgetsAsync());
        }

        [Test]
        [Category("Mocked")]
        public void GetAccountsParsedCorrectly()
        {
            handler.QueueResponse(MockResponseHandlers.AccountsResponse);
            var budgets = ynabClient.GetAccountsAsync(new BudgetSummary { Id = Guid.Empty }).Result;
            Assert.That(budgets is List<Account>);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].Id == Guid.Empty, "Incorect Id");
            Assert.That(budgets[0].Name == "TestAccount", "Incorrect account name");
        }

        [Test]
        [Category("Mocked")]
        public void GetBudgetsParsedCorrectly()
        {
            handler.QueueResponse(MockResponseHandlers.BudgetsResponse);
            var budgets = ynabClient.GetBudgetsAsync().Result;
            Assert.That(budgets is List<BudgetSummary>);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
            Assert.That(budgets[0].Name == "Test budget");
        }

        [OneTimeSetUp]
        public void Setup()
        {
            handler = new StubHandler();
            ynabClient = YNABClient.GetInstance(handler);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            YNABClient.ResetInstance();
        }

        private StubHandler handler;
        private YNABClient ynabClient;
    }
}