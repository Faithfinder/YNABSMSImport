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
        private HttpMessageHandler handler;

        private YNABClient(HttpMessageHandler handler)
        {
            accessToken = ApiKeys.AccessToken;
            ConstructHttpClient(handler);

            void ConstructHttpClient(HttpMessageHandler _handler)
            {
                handler = _handler;
                client = new HttpClient(_handler)
                {
                    BaseAddress = new Uri(YNABPaths.Base)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
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