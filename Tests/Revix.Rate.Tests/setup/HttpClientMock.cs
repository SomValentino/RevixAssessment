namespace Revix.Rate.Tests.Setup;
using Moq;
public class HttpClientMock {
    public static Mock<IHttpClientFactory> CreateHttpClientFactory () 
    {
        var client = new HttpClient()
    }
}