namespace APBD_Tests;

public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _responseMessage;

    public MockHttpMessageHandler(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseMessage);
    }
}