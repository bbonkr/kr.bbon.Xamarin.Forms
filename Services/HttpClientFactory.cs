using kr.bbon.Xamarin.Forms.Abstractions;
using kr.bbon.Xamarin.Forms.Net.Http;
using System;
using System.Net.Http;

namespace kr.bbon.Xamarin.Forms.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClientFactory(HttpClientOptions options)
        {
            this.options = options ?? new HttpClientOptions();
        }

        public HttpClient Create(HttpMessageHandler handler, bool disposeHandler)
        {
            var client = new HttpClient(handler ?? new HttpClientHandler(), disposeHandler);
            client.Timeout = TimeSpan.FromSeconds(options.Timeout);
            return client;
        }

        public HttpClient Create(HttpMessageHandler handler)
        {
            return Create(handler, false);
        }

        public HttpClient Create()
        {
            return Create(HttpClientNames.HTTP_CLIENT_DEFAULT);
        }

        public HttpClient Create(string name)
        {
            HttpClientHandler handler = null;
            switch (name)
            {
                case HttpClientNames.HTTP_CLIENT_RETRY:
                    handler = new RetryHttpMessageHandler();
                    break;
                case HttpClientNames.HTTP_CLIENT_DEFAULT:
                default:
                    handler = new DefaultHttpMessageHandler();
                    break;
            }

            return Create(handler);
        }

        private readonly HttpClientOptions options;
    }
}
