using NUnit.Framework;
using YNABConnector;

namespace NUnit.YnabConnectorTests
{
    [TestFixture]
    public class YNABConnectorTest
    {
        public YNABConnectorTest() => AccessToken = ApiKeys.AccessToken;

        [Test]
        public void GetBudgetTest()
        {
            var ynabClient = YNABClient.Instance;
            var budget = ynabClient.GetBudgetsAsync();

            Assert.That(budget is string);
        }

        private readonly string AccessToken;
    }
}