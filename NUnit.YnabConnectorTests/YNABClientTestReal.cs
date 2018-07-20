using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using YNABConnector;
using YNABConnector.YNABObjectModel;
using YNABConnector.Exceptions;

namespace NUnit.YnabConnectorTests
{
    [TestFixture(TestOf = typeof(YNABClient))]
    public class YNABClientTestReal
    {
        [Test]
        [Category("Real")]
        public void GetBudgetsTest()
        {
            var ynabClient = YNABClient.GetInstance();
            var budget = ynabClient.GetBudgetsAsync().Result;

            Assert.That(budget is List<BudgetSummary>);
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