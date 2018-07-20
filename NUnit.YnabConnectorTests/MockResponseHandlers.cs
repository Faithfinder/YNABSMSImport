using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.YnabConnectorTests
{
    internal static class MockResponseHandlers
    {
        public static HttpResponseMessage BudgetsResponse
        {
            get
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"data\":{\"budgets\":[{\"id\":\"7ffba7b3-81b2-4ce1-a546-1b176368cc1b\",\"name\":\"LoL\",\"last_modified_on\":\"2018-07-10T13:01:46+00:00\",\"first_month\":\"2016-07-01\",\"last_month\":\"2018-07-01\",\"date_format\":{\"format\":\"DD.MM.YYYY\"},\"currency_format\":{\"iso_code\":\"RUB\",\"example_format\":\"123 456,78\",\"decimal_digits\":2,\"decimal_separator\":\",\",\"symbol_first\":false,\"group_separator\":\" \",\"currency_symbol\":\"р.\",\"display_symbol\":true}}]}}")
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