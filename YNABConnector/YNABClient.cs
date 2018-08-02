using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using YNABConnector.Exceptions;
using YNABConnector.YNABObjectModel;

namespace YNABConnector
{
    public class YNABClient
    {
        public bool Initialized => !(_instance is null);

        /// <summary>
        /// Get the singleton instance of YNABClient. If it's not initialized, supply constructor with handler.
        /// </summary>
        /// <param name="handler">Ignored if the instance is already initialized</param>
        /// <returns></returns>
        public static YNABClient GetInstance(HttpMessageHandler handler)
        {
            return _instance ?? (_instance = new YNABClient(handler));
        }

        public static YNABClient GetInstance() => GetInstance(new HttpClientHandler());

        public static void ResetInstance()
        {
            _instance = null;
        }

        public async Task<List<Account>> GetAccountsAsync(BudgetSummary budgetSummary)
        {
            var json = await GetJSON(YNABPaths.Accounts(budgetSummary.Id));

            return ExtractAccounts(json);
        }

        public async Task<List<BudgetSummary>> GetBudgetsAsync()
        {
            var json = await GetJSON(YNABPaths.BUDGETS);

            return ExtractBudgets(json);
        }

        public async Task<TransactionDetail> PostTransactionAsync(BudgetSummary budgetSummary, SaveTransaction saveTransaction)
        {
            return await PostTransactionAsync(budgetSummary.Id, saveTransaction);
        }

        public async Task<TransactionDetail> PostTransactionAsync(Guid budgetId, SaveTransaction saveTransaction)
        {
            var postJson = saveTransaction.Wrap().Serialize();
            var responseJson = await PostJSON(YNABPaths.Transactions(budgetId), ConstructHttpContent(postJson));
            return ExtractTransaction(responseJson);
        }

        public void RefreshAccessToken(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private static MediaTypeWithQualityHeaderValue JsonTypeHeader => new MediaTypeWithQualityHeaderValue(JSON_CONTENT_TYPE);
        private const string JSON_CONTENT_TYPE = "application/json";

        private static YNABClient _instance;

        private readonly HttpClient _client;

        private YNABClient(HttpMessageHandler handler)
        {
            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(YNABPaths.BASE)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(JsonTypeHeader);
        }

        private static StringContent ConstructHttpContent(string json)
        {
            var content = new StringContent(json);
            content.Headers.ContentType = JsonTypeHeader;
            return content;
        }

        private static Exception DeserializeToException(string json)
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(json);
            return ExceptionFactory.GenerateExceptionFromErrorResponse(errorResponse);
        }

        private static List<Account> ExtractAccounts(string json)
        {
            var accountsResponse = JsonConvert.DeserializeObject<SuccessResponse<AccountsWrapper>>(json);
            return accountsResponse.data.accounts;
        }

        private static List<BudgetSummary> ExtractBudgets(string json)
        {
            var budgetSummaryResponse = JsonConvert.DeserializeObject<SuccessResponse<BudgetSummaryWrapper>>(json);
            return budgetSummaryResponse.data.budgets;
        }

        private static TransactionDetail ExtractTransaction(string json)
        {
            var transactionResponse = JsonConvert.DeserializeObject<SuccessResponse<TransactionWrapper>>(json);
            return transactionResponse.data.Transaction;
        }

        private async Task<string> GetJSON(string path)
        {
            var response = await _client.GetAsync(path);
            return await ParseResponse(response);
        }

        private static async Task<string> ParseResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw DeserializeToException(json);

            return json;
        }

        private async Task<string> PostJSON(string path, HttpContent content)
        {
            var response = await _client.PostAsync(path, content);
            return await ParseResponse(response);
        }
    }
}