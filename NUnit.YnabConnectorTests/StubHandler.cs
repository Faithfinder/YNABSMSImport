using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NUnit.YnabConnectorTests
{
    internal class StubHandler : HttpMessageHandler
    {
        // Requests that were sent via the handler
        private readonly List<HttpRequestMessage> _requests =
            new List<HttpRequestMessage>();

        // Responses to return
        private readonly Queue<HttpResponseMessage> _responses =
            new Queue<HttpResponseMessage>();

        public IEnumerable<HttpRequestMessage> GetRequests()
        {
            return _requests;
        }

        public void QueueResponse(HttpResponseMessage response)
        {
            _responses.Enqueue(response);
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_responses.Count == 0)
                throw new InvalidOperationException("No response configured");

            _requests.Add(request);
            var response = _responses.Dequeue();
            return Task.FromResult(response);
        }
    }
}