namespace Revix.Rate.Tests.Setup;
using Moq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

public class HttpClientMock {
    public static IHttpClientFactory CreateHttpClientFactory () 
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "f25cb104-1984-4fe6-8c40-73b5517e9bbd");

        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        IHttpClientFactory factory = mockFactory.Object;

        return factory;
    }
}