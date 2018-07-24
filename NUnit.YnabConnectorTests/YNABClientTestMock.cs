using NUnit.Framework;
using System.Collections.Generic;
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
            handler.QueueResponse(MockResponseMessages.Unauthorized);
            Assert.ThrowsAsync<AuthorizationException>(async () => await ynabClient.GetBudgetsAsync());
        }

        [Test]
        [Category("Mocked")]
        public void GetAccountsParsedCorrectly()
        {
            handler.QueueResponse(MockResponseMessages.AccountsResponse);
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
            handler.QueueResponse(MockResponseMessages.BudgetsResponse);
            var budgets = ynabClient.GetBudgetsAsync().Result;
            Assert.That(budgets is List<BudgetSummary>);
            Assert.That(budgets.Count == 1);
            Assert.That(budgets[0].Id == Guid.Parse("00000000-0000-0000-0000-000000000000"));
            Assert.That(budgets[0].Name == "Test budget");
        }

        [Test]
        [Category("Mocked")]
        public void PostTransaction()
        {
            handler.QueueResponse(MockResponseMessages.PostTransactionResponse);
            var saveTransaction = new SaveTransaction
            {
                Account_id = Guid.Empty,
                Amount = 100000,
                Payee_name = "Test Payee"
            };
            var transaction = ynabClient.PostTransactionAsync(new BudgetSummary { Id = Guid.Empty }, saveTransaction).Result;
            Assert.That(transaction is TransactionDetail);
        }

        [Test]
        [Category("Mocked")]
        public void PostTransactionDoubleImportID()
        {
            handler.QueueResponse(MockResponseMessages.ImportIDExists);
            var saveTransaction = new SaveTransaction
            {
                Account_id = Guid.Empty,
                Amount = 100000,
                Payee_name = "Test Payee",
                Import_id = "double"
            };
            Assert.ThrowsAsync<DuplicateImportIdException>(async () => await ynabClient.PostTransactionAsync(new BudgetSummary { Id = Guid.Empty }, saveTransaction));
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