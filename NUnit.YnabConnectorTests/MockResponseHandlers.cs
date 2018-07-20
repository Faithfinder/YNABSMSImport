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