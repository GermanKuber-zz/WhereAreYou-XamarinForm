using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.iOS.Services;
using Xamarin.Forms;
using System.Net.Http;
[assembly: Dependency(typeof(HttpClientHandlerFactory))]

namespace WhereAreYouMobile.iOS.Services
{
    /// <summary>
    /// Http client handler factory for iOS. Allows the simulator to use the host operating systems's (OS X) proxy settings in order to allow debugging of HTTP requests with tools such as Charles.
    /// Only used for debugging, not production.
    /// </summary>
    public class HttpClientHandlerFactory : IHttpClientHandlerFactory
    {
        public HttpClientHandler GetHttpClientHandler()
        {
            return new HttpClientHandler
            {
                Proxy = CoreFoundation.CFNetwork.GetDefaultProxy(),
                UseProxy = true
            };
        }
    }
}

