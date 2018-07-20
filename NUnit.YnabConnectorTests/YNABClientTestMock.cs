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
        public void GetBudgetsParsedCorrectly()
        {
            handler.QueueResponse(MockResponseHandlers.BudgetsResponse);
            var budgets = ynabClient.GetBudgetsAsync().Result;
            Assert.That(budgets is List<BudgetSummary>);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].id == Guid.Parse("7ffba7b3-81b2-4ce1-a546-1b176368cc1b"));
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