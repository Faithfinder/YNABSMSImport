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
        public static YNABClient Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new YNABClient();
                }
                return instance;
            }
        }

        public async Task<List<BudgetSummary>> GetBudgetsAsync()
        {
            var response = await client.GetAsync(YNABPaths.Budgets);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw DeserializeToException(json);

            return ExtractBudgets(json);
        }

        private static YNABClient instance;

        private readonly string accessToken;

        private HttpClient client;

        private YNABClient()
        {
            accessToken = ApiKeys.AccessToken;
            ConstructHttpClient();
        }

        private void ConstructHttpClient()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.youneedabudget.com/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private Exception DeserializeToException(string json)
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(json);
            return ExceptionFactory.GenerateExceptionFromErrorResponse(errorResponse);
        }

        private List<BudgetSummary> ExtractBudgets(string json)
        {
            var budgetSummaryResponse = JsonConvert.DeserializeObject<SuccessResponse<BudgetSummaryWrapper>>(json);
            return budgetSummaryResponse.data.budgets;
        }
    }
}