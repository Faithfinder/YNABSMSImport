using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
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

        public dynamic GetBudgetsAsync()
        {
            var response = client.GetAsync("v1/budgets").Result;

            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<BudgetSummaryResponse>(json);

            return result;
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
    }
}