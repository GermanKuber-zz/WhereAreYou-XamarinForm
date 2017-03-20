using System.Net.Http;

namespace WhereAreYouMobile.Abstractions.Config
{
    public interface IHttpClientHandlerFactory
    {
        HttpClientHandler GetHttpClientHandler();
    }
}