using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Abstractions
{
    public interface IHttpClientFactory
    {
        HttpClient Create(HttpMessageHandler handler, bool disposeHandler);

        HttpClient Create(HttpMessageHandler handler);

        HttpClient Create();

        HttpClient Create(string name);
    }
}
