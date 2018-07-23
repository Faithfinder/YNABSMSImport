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
        public bool Initialized => !(instance is null);

        /// <summary>
        /// Get the singleton instance of YNABClient. If it's not initialized, supply constructor with handler.
        /// </summary>
        /// <param name="_handler">Ignored if the instance is already initialized</param>
        /// <returns></returns>
        public static YNABClient GetInstance(HttpMessageHandler _handler)
        {
            if (instance is null)
            {
                instance = new YNABClient(_handler);
            }

            return instance;
        }

        public static YNABClient GetInstance()
        {
            return GetInstance(new HttpClientHandler());
        }

        public static void ResetInstance()
        {
            instance = null;
        }

        public async Task<List<Account>> GetAccountsAsync(BudgetSummary budgetSummary)
        {
            var json = await GetJSON(YNABPaths.Accounts(budgetSummary.Id));

            return ExtractAccounts(json);
        }

        public async Task<List<BudgetSummary>> GetBudgetsAsync()
        {
            var json = await GetJSON(YNABPaths.Budgets);

            return ExtractBudgets(json);
        }

        public async Task<TransactionDetail> PostTransactionAsync(BudgetSummary budgetSummary, SaveTransaction saveTransaction)
        {
            var postJson = saveTransaction.Wrap().Serialize();
            var responseJson = await PostJSON(YNABPaths.Transactions(budgetSummary.Id), ConstructHttpContent(postJson));
            return ExtractTransaction(responseJson);
        }

        public void RefreshAccessToken(string accessToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private const string JSON_CONTENT_TYPE = "application/json";

        private static YNABClient instance;

        private HttpClient client;

        private HttpMessageHandler handler;

        private YNABClient(HttpMessageHandler _handler)
        {
            handler = _handler;
            client = new HttpClient(_handler)
            {
                BaseAddress = new Uri(YNABPaths.Base)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(JsonTypeHeader());
        }

        private static StringContent ConstructHttpContent(string Json)
        {
            var content = new StringContent(Json);
            content.Headers.ContentType = JsonTypeHeader();
            return content;
        }

        private static MediaTypeWithQualityHeaderValue JsonTypeHeader()
        {
            return new MediaTypeWithQualityHeaderValue(JSON_CONTENT_TYPE);
        }

        private Exception DeserializeToException(string json)
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(json);
            return ExceptionFactory.GenerateExceptionFromErrorResponse(errorResponse);
        }

        private List<Account> ExtractAccounts(string json)
        {
            var accountsResponse = JsonConvert.DeserializeObject<SuccessResponse<AccountsWrapper>>(json);
            return accountsResponse.data.accounts;
        }

        private List<BudgetSummary> ExtractBudgets(string json)
        {
            var budgetSummaryResponse = JsonConvert.DeserializeObject<SuccessResponse<BudgetSummaryWrapper>>(json);
            return budgetSummaryResponse.data.budgets;
        }

        private TransactionDetail ExtractTransaction(string json)
        {
            var transactionResponse = JsonConvert.DeserializeObject<SuccessResponse<TransactionWrapper>>(json);
            return transactionResponse.data.Transaction;
        }

        private async Task<string> GetJSON(string path)
        {
            var response = await client.GetAsync(path);
            return await ParseResponse(response);
        }

        private async Task<string> ParseResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw DeserializeToException(json);

            return json;
        }

        private async Task<string> PostJSON(string path, HttpContent content)
        {
            var response = await client.PostAsync(path, content);
            return await ParseResponse(response);
        }
    }
}