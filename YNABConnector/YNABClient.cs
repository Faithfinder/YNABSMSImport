using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace YNABConnector
{
    public class YNABClient
    {

        public string AccessToken { get; }
        private HttpClient client;

        public YNABClient(string accessToken)
        {
            AccessToken = accessToken;
            client = new HttpClient();
        }

        public string GetBudgets()
        {
            client.BaseAddress = new Uri("https://api.youneedabudget.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            HttpResponseMessage response = client.GetAsync("v1/budgets").Result;
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            return result;
        }
    }
}
