using NUnit.Framework;
using YNABConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
            var budget = ynabClient.GetBudgets();
            Debug.WriteLine(budget);
            Assert.That(budget is string);
        }

        private readonly string AccessToken;
    }
}