using kr.bbon.Xamarin.Forms.Abstractions;
using kr.bbon.Xamarin.Forms.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Factories
{
    /// <summary>
    /// HttpClient 인스턴스를 반환합니다. 
    /// </summary>
    public class HttpClientFactory : IHttpClientFactory
    {
        /// <summary>
        /// 재시도 HTTP Client 이름
        /// </summary>
        public const string HTTP_CLIENT_RETRY = "retry-http-client";

        /// <summary>
        /// 기본 HTTP Client 이름
        /// </summary>
        public const string HTTP_CLIENT_DEFAULT = "default-http-client";

        /// <summary>
        /// 만료시간 기본값 (30초)
        /// </summary>
        public const int HTTP_CLIENT_TIMEOUT_SECONDS_DEFAULT = 30;


        public HttpClient Create(HttpMessageHandler handler, bool disposeHandler, int timeoutSeconds)
        {
            var client = new HttpClient(handler ?? new HttpClientHandler(), disposeHandler);
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            return client;
        }

        public HttpClient Create(HttpMessageHandler handler)
        {
            return Create(handler, false, HTTP_CLIENT_TIMEOUT_SECONDS_DEFAULT);
        }

        public HttpClient Create()
        {
            return Create(HTTP_CLIENT_DEFAULT, HTTP_CLIENT_TIMEOUT_SECONDS_DEFAULT);
        }

        public HttpClient Create(int timeoutSeconds)
        {
            return Create(HTTP_CLIENT_DEFAULT, timeoutSeconds);
        }

        public HttpClient Create(string name, int timeoutSeconds)
        {
            HttpClientHandler handler = null;
            switch (name)
            {
                case HTTP_CLIENT_RETRY:
                    handler = new RetryHttpMessageHandler();
                    break;
                case HTTP_CLIENT_DEFAULT:
                default:
                    handler = new DefaultHttpMessageHandler();
                    break;
            }

            return Create(handler);
        }
    }
}
