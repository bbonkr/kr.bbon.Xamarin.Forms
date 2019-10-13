using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Abstractions
{
    public interface IHttpClientFactory
    {
        HttpClient Create(HttpMessageHandler handler, bool disposeHandler, int timeoutSeconds);

        HttpClient Create(HttpMessageHandler handler);

        HttpClient Create(string name, int timeoutSeconds);

        HttpClient Create(int timeoutSeconds);

        HttpClient Create();
    }
}
