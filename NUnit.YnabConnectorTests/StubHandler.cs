using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class StubHandler : HttpMessageHandler
{
    public IEnumerable<HttpRequestMessage> GetRequests() =>
            _requests;

    public void QueueResponse(HttpResponseMessage response) =>
            _responses.Enqueue(response);

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

    // Requests that were sent via the handler
    private readonly List<HttpRequestMessage> _requests =
        new List<HttpRequestMessage>();

    // Responses to return
    private readonly Queue<HttpResponseMessage> _responses =
        new Queue<HttpResponseMessage>();
}