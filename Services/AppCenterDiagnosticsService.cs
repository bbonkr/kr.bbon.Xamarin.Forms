using kr.bbon.Xamarin.Forms.Abstractions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kr.bbon.Xamarin.Forms.Services
{
    /// <summary>
    /// App Center Diagnostics API를 사용합니다.
    /// </summary>
    public class AppCenterDiagnosticsService : IAppCenterDiagnosticsService
    {
        private const string TAG = "AppCenterDiagnosticsService";

        public async Task SetAnalyticsEnabledAsync(bool enabled)
        {
            await Analytics.SetEnabledAsync(enabled);

            await IsAnalyticsEnabledAsync().ContinueWith(t =>
            {
                Info(TAG, $"Analytics.IsEnabled={t.Result}", null, null, null);
            }, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.NotOnFaulted);
        }

        public async Task SetCrashesEnabledAsync(bool enabled)
        {
            await Crashes.SetEnabledAsync(enabled);

            await IsCrashesEnabledAsync().ContinueWith(t =>
            {
                Info(TAG, $"Crashes.IsEnabled={t.Result}", null, null, null);
            }, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.NotOnFaulted);
        }

        public async Task<bool> IsAnalyticsEnabledAsync()
        {
            return await Analytics.IsEnabledAsync();
        }

        public async Task<bool> IsCrashesEnabledAsync()
        {
            return await Crashes.IsEnabledAsync();
        }

        /// <summary>
        /// 이벤트 데이터를 전송합니다.
        /// </summary>
        /// <param name="name">이벤트 이름</param>
        /// <param name="properties">추가 정보</param>
        public void TrackEvent(string name, AppCenterProperties properties = null)
        {
            Info(AppCenterLog.LogTag, $"{name}{Environment.NewLine}{properties?.ToString()}", null, null, null);

            Analytics.TrackEvent(name, properties?.ToDictionary());
        }

        /// <summary>
        /// 이벤트 데이터를 전송합니다.
        /// </summary>
        /// <param name="name">이벤트 이름</param>
        /// <param name="properties">추가 정보</param>
        /// <returns></returns>
        public Task TrackEventAsync(string name, AppCenterProperties properties = null)
        {
            TrackEvent(name, properties);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 예외 정보를 전송합니다.
        /// </summary>
        /// <param name="exception">예외</param>
        /// <param name="properties">추가 정보</param>
        public void TrackError(Exception exception, AppCenterProperties properties = null)
        {
            Error(AppCenterLog.LogTag, $"{exception.Message}{Environment.NewLine}{properties?.ToString()}", exception);

            Crashes.TrackError(exception, properties?.ToDictionary());
        }

        /// <summary>
        /// 예외 정보를 전송합니다.
        /// </summary>
        /// <param name="exception">예외</param>
        /// <param name="properties">추가정보</param>
        /// <returns></returns>
        public Task TrackErrorAsync(Exception exception, AppCenterProperties properties = null)
        {
            TrackError(exception, properties);

            return Task.CompletedTask;
        }

        public void Verbose(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);

            AppCenterLog.Verbose(tag, collectedMessage, ex);
        }

        public void Verbose(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);

            Verbose(tag, $"{message}{callerInfo}", ex);
        }

        public void Debug(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);
            AppCenterLog.Debug(tag, collectedMessage, ex);
        }

        public void Debug(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);
            Debug(tag, $"{message}{callerInfo}", ex);
        }

        public void Info(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);
            AppCenterLog.Info(tag, collectedMessage, ex);
        }

        public void Info(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);
            Info(tag, $"{message}{callerInfo}", ex);
        }

        public void Warn(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);
            AppCenterLog.Warn(tag, collectedMessage, ex);
        }

        public void Warn(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);
            Warn(tag, $"{message}{callerInfo}", ex);
        }

        public void Error(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);
            AppCenterLog.Error(tag, collectedMessage, ex);
        }

        public void Error(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);
            Error(tag, $"{message}{callerInfo}", ex);
        }

        public void Assert(string tag, string message, Exception ex = null)
        {
            var collectedMessage = CollectExceptionDetails(message, ex);
            AppCenterLog.Assert(tag, collectedMessage, ex);
        }

        public void Assert(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0)
        {
            string callerInfo = GetCallerInfo(currentObj, memberName, sourceFilePath, sourceLineNumber);
            Assert(tag, $"{message}{callerInfo}", ex);
        }

        private string CollectExceptionDetails(string message, Exception ex)
        {
            var builder = new StringBuilder();
            builder.Append($"Message: {message}");

            if (ex != null)
            {
                CollectExceptionDetails(builder, ex);
            }

            return builder.ToString();
        }

        private void CollectExceptionDetails(StringBuilder builder, Exception ex)
        {
            if (builder == null) { builder = new StringBuilder(); }

            builder.AppendLine($"{ex.GetType().GetTypeInfo().FullName} => {ex.Message}");

            if (ex.InnerException != null)
            {
                builder.AppendLine("InnerException: ");
                CollectExceptionDetails(builder, ex.InnerException);
            }

            if (ex is AggregateException)
            {

                var aggregateException = ex as AggregateException;
                builder.AppendLine("InnerExceptions: ");
                foreach (var e in aggregateException.InnerExceptions)
                {
                    CollectExceptionDetails(builder, e);
                }
            }
        }

        private string GetCallerInfo(object obj, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            StringBuilder builder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(sourceFilePath) && sourceLineNumber > 0)
            {
                builder.Append($"{sourceFilePath}({sourceLineNumber}) : ");
            }

            if (obj != null && !String.IsNullOrWhiteSpace(memberName))
            {
                builder.Append($"Call ==> {obj.GetType().GetTypeInfo().FullName}.{memberName}");
            }

            if (builder.Length > 0)
            {
                builder.AppendLine();
            }

            return builder.ToString();
        }

    }
}
