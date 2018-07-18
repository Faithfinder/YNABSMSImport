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
        public YNABConnectorTest()
        {
            AccessToken = Resources.AccessToken;
        }

        private readonly string AccessToken;

        [Test]
        public void GetBudgetTest()
        {
            var ynabClient = new YNABClient(AccessToken);
            var budget = ynabClient.GetBudgets();
            Debug.WriteLine(budget);
            Assert.That(budget is string);
        }
    }
}
