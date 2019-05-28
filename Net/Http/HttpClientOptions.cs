using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Net.Http
{
    public class HttpClientOptions
    {
        public double Timeout { get; set; } = 30;
    }

    public class HttpClientNames
    {
        /// <summary>
        /// 재시도 HTTP Client 이름
        /// </summary>
        public const string HTTP_CLIENT_RETRY = "retry-http-client";

        /// <summary>
        /// 기본 HTTP Client 이름
        /// </summary>
        public const string HTTP_CLIENT_DEFAULT = "default-http-client";
    }
}
