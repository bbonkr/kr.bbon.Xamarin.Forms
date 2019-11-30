using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using System.Diagnostics;
using kr.bbon.Xamarin.Forms.Abstractions;

namespace kr.bbon.Xamarin.Forms.Net.Http
{
    public class DefaultHttpMessageHandler : HttpClientHandler
    {
        private const string LOG_TAG = nameof(DefaultHttpMessageHandler);

        public DefaultHttpMessageHandler()
        {
            appCenterDiagnosticsService = AppContainer.Instance.Container.Resolve<IAppCenterDiagnosticsService>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {

            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                appCenterDiagnosticsService?.Error(LOG_TAG, ex.Message, ex, this);

                throw;
            }
#if DEBUG
            if (Debugger.IsAttached)
            {
                // 디버깅이 필요할 때만 사용합니다.
                //if (responseMessage.IsSuccessStatusCode)
                //{
                //    if (responseMessage.Content != null)
                //    {
                //        var contentType = responseMessage.Content.Headers?.ContentType?.MediaType ?? "unknown";
                //        if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
                //        {
                //            appCenterDiagnosticsService?.Debug(LOG_TAG, await responseMessage.Content.ReadAsStringAsync());
                //        }
                //        else if (contentType.EndsWith("/json", StringComparison.OrdinalIgnoreCase))
                //        {
                //            appCenterDiagnosticsService?.Debug(LOG_TAG, await responseMessage.Content.ReadAsStringAsync());
                //        }
                //        else
                //        {
                //            var fileName = responseMessage.Content.Headers?.ContentDisposition?.FileName;
                //            var fileSize = responseMessage.Content.Headers?.ContentDisposition?.Size;
                //            if (!fileSize.HasValue)
                //            {
                //                fileSize = responseMessage.Content.Headers?.ContentLength;
                //            }
                //            if (!fileSize.HasValue)
                //            {
                //                fileSize = -1;
                //            }

                //            appCenterDiagnosticsService?.Debug(LOG_TAG, $"content-type: {contentType } / filename: {fileName} / length: {fileSize.Value}");
                //        }
                //    }
                //}
            }
#endif            

            return responseMessage;
        }

        private readonly IAppCenterDiagnosticsService appCenterDiagnosticsService;
    }
}
