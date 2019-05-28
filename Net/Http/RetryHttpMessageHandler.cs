using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using System.Diagnostics;
using kr.bbon.Xamarin.Forms.Abstractions;

namespace kr.bbon.Xamarin.Forms.Net.Http
{
    public class RetryHttpMessageHandler : HttpClientHandler
    {
        public const int DEFAULT_MAX_RETRIES = 3;
        private const string LOG_TAG = nameof(RetryHttpMessageHandler);

        public RetryHttpMessageHandler(int maxTries) : base()
        {
            this.maxRetries = maxTries;

            appCenterDiagnosticsService = AppContainer.Instance.Resolve<IAppCenterDiagnosticsService>();
        }

        public RetryHttpMessageHandler() : this(DEFAULT_MAX_RETRIES) { }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage responseMessage = null;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    responseMessage = await base.SendAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    appCenterDiagnosticsService.Error(LOG_TAG, ex.Message, ex, this);

                    throw;
                }

                if (responseMessage.IsSuccessStatusCode)
                {
#if DEBUG
                    if (Debugger.IsAttached)
                    {
                        // 디버깅이 필요할 때만 사용합니다.
                        //if (responseMessage.Content != null)
                        //{
                        //    var contentType = responseMessage.Content.Headers?.ContentType?.MediaType ?? "unknown";
                        //    if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
                        //    {
                        //        appCenterDiagnosticsService.Debug(LOG_TAG, await responseMessage.Content.ReadAsStringAsync());
                        //    }
                        //    else if (contentType.EndsWith("/json", StringComparison.OrdinalIgnoreCase))
                        //    {
                        //        appCenterDiagnosticsService.Debug(LOG_TAG, await responseMessage.Content.ReadAsStringAsync());
                        //    }
                        //    else
                        //    {
                        //        var fileName = responseMessage.Content.Headers?.ContentDisposition?.FileName;
                        //        var fileSize = responseMessage.Content.Headers?.ContentDisposition?.Size;
                        //        if (!fileSize.HasValue)
                        //        {
                        //            fileSize = responseMessage.Content.Headers?.ContentLength;
                        //        }
                        //        if (!fileSize.HasValue)
                        //        {
                        //            fileSize = -1;
                        //        }

                        //        appCenterDiagnosticsService.Debug(LOG_TAG, $"content-type: {contentType } / filename: {fileName} / length: {fileSize.Value}");
                        //    }
                        //}
                    }
#endif
                    return responseMessage;
                }
            }

            return responseMessage;
        }

        private readonly int maxRetries;
        private readonly IAppCenterDiagnosticsService appCenterDiagnosticsService;
    }
}
