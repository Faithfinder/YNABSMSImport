using System.Net;
using System.Net.Http;

namespace NUnit.YnabConnectorTests
{
    internal static class MockResponseMessages
    {
        public static HttpResponseMessage AccountsResponse
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"data\":{\"accounts\":[{\"id\":\"00000000-0000-0000-0000-000000000000\",\"name\":\"TestAccount\",\"type\":\"checking\",\"on_budget\":true,\"closed\":false,\"note\":null,\"balance\":100000000,\"cleared_balance\":100000000,\"uncleared_balance\":0,\"deleted\":false}]}}")
                };

                return result;
            }
        }

        public static HttpResponseMessage BudgetsResponse
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"data\":{\"budgets\":[{\"id\":\"00000000-0000-0000-0000-000000000000\",\"name\":\"Test budget\",\"last_modified_on\":\"2018-07-10T13:01:46+00:00\",\"first_month\":\"2016-07-01\",\"last_month\":\"2018-07-01\",\"date_format\":{\"format\":\"DD.MM.YYYY\"},\"currency_format\":{\"iso_code\":\"RUB\",\"example_format\":\"123 456,78\",\"decimal_digits\":2,\"decimal_separator\":\",\",\"symbol_first\":false,\"group_separator\":\" \",\"currency_symbol\":\"р.\",\"display_symbol\":true}}]}}")
                };

                return result;
            }
        }

        public static HttpResponseMessage ImportIDExists
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"error\":{\"id\":\"400\",\"name\":\"bad_request\",\"detail\":\"{\\\"import_id\\\":[\\\"A transaction with the same import_id already exists on the account.\\\"]}\"}}")
                };

                return result;
            }
        }

        public static HttpResponseMessage PostTransactionResponse
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("{\"data\":{\"transaction\":{\"id\":\"00000000-0000-0000-0000-000000000000\",\"date\":\"2018-07-22\",\"amount\":100000,\"memo\":null,\"cleared\":\"uncleared\",\"approved\":false,\"flag_color\":null,\"account_id\":\"00000000-0000-0000-0000-000000000000\",\"account_name\":\"Дима(Сбер)\",\"payee_id\":\"00000000-0000-0000-0000-000000000000\",\"payee_name\":\"Test Payee\",\"category_id\":null,\"category_name\":null,\"transfer_account_id\":null,\"import_id\":null,\"deleted\":false,\"subtransactions\":[]}}}")
                };

                return result;
            }
        }

        public static HttpResponseMessage Unauthorized
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("{\"error\":{\"id\":\"401\",\"name\":\"unauthorized\",\"detail\":\"Unauthorized\"}}")
                };

                return result;
            }
        }
    }
}