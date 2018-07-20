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
    public class YNABClientTestMock
    {
        [Test]
        [Category("Mocked")]
        public void ErrorUnauthorized()
        {
            handler.QueueResponse(MockResponseHandlers.Unauthorized);
            Assert.ThrowsAsync<AuthorizationException>(async () => await ynabClient.GetBudgetsAsync());
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