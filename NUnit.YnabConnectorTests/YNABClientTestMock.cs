using System.Collections.Generic;
using System;

using NUnit.Framework;

using YNABConnector;
using YNABConnector.Exceptions;
using YNABConnector.YNABObjectModel;

namespace NUnit.YnabConnectorTests
{
    [TestFixture(TestOf = typeof(YNABClient))]
    public class YNABClientTestMock
    {
        [OneTimeSetUp]
        public void Setup()
        {
            _handler = new StubHandler();
            _ynabClient = YNABClient.GetInstance(_handler);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            YNABClient.ResetInstance();
        }

        private StubHandler _handler;
        private YNABClient _ynabClient;

        [Test]
        [Category("Mocked")]
        public void ErrorUnauthorized()
        {
            _handler.QueueResponse(MockResponseMessages.Unauthorized);
            Assert.ThrowsAsync<AuthorizationException>(async () => await _ynabClient.GetBudgetsAsync());
        }

        [Test]
        [Category("Mocked")]
        public void GetAccountsParsedCorrectly()
        {
            _handler.QueueResponse(MockResponseMessages.AccountsResponse);
            var budgets = _ynabClient.GetAccountsAsync(new BudgetSummary {Id = Guid.Empty}).Result;
            Assert.That(budgets != null);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].Id == Guid.Empty, "Incorect Id");
            Assert.That(budgets[0].Name == "TestAccount", "Incorrect account name");
        }

        [Test]
        [Category("Mocked")]
        public void GetBudgetsParsedCorrectly()
        {
            _handler.QueueResponse(MockResponseMessages.BudgetsResponse);
            var budgets = _ynabClient.GetBudgetsAsync().Result;
            Assert.That(budgets != null);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
            Assert.That(budgets[0].Name == "Test budget");
        }

        [Test]
        [Category("Mocked")]
        public void PostTransaction()
        {
            _handler.QueueResponse(MockResponseMessages.PostTransactionResponse);
            var saveTransaction = new SaveTransaction
            {
                Account_id = Guid.Empty,
                Amount = 100000,
                Payee_name = "Test Payee"
            };
            var transaction = _ynabClient.PostTransactionAsync(new BudgetSummary {Id = Guid.Empty}, saveTransaction)
                .Result;
            Assert.That(transaction != null);
        }

        [Test]
        [Category("Mocked")]
        public void PostTransactionDoubleImportID()
        {
            _handler.QueueResponse(MockResponseMessages.ImportIDExists);
            var saveTransaction = new SaveTransaction
            {
                Account_id = Guid.Empty,
                Amount = 100000,
                Payee_name = "Test Payee",
                Import_id = "double"
            };
            Assert.ThrowsAsync<DuplicateImportIdException>(async () =>
                await _ynabClient.PostTransactionAsync(new BudgetSummary {Id = Guid.Empty}, saveTransaction));
        }
    }
}