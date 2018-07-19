using NUnit.Framework;
using System.Collections.Generic;
using YNABConnector;
using YNABConnector.YNABObjectModel;

namespace NUnit.YnabConnectorTests
{
    [TestFixture(TestOf = typeof(YNABClient))]
    public class YNABClientTest
    {
        [Test]
        public void GetBudgetsTest()
        {
            var ynabClient = YNABClient.Instance;
            var budget = ynabClient.GetBudgetsAsync().Result;

            Assert.That(budget is List<BudgetSummary>);
        }
    }
}