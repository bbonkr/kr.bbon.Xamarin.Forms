using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kr.bbon.Xamarin.Forms.Abstractions
{
    /// <summary>
    /// App Center Diagnostics API를 사용합니다.
    /// </summary>
    public interface IAppCenterDiagnosticsService
    {
        /// <summary>
        /// 이벤트 정보 전송여부를 설정합니다.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task SetAnalyticsEnabledAsync(bool enabled);

        /// <summary>
        /// 예외 정보 전송여부를 설정합니다.
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task SetCrashesEnabledAsync(bool enabled);

        /// <summary>
        /// 이벤트 정보 전송여부를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAnalyticsEnabledAsync();

        /// <summary>
        /// 예외 정보 전송 여부를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCrashesEnabledAsync();

        /// <summary>
        /// 이벤트 데이터를 전송합니다.
        /// </summary>
        /// <param name="name">이벤트 이름</param>
        /// <param name="properties">추가 정보</param>
        void TrackEvent(string name, AppCenterProperties properties = null);

        /// <summary>
        /// 이벤트 정보를 전송합니다.
        /// </summary>
        /// <param name="name">이벤트 이름</param>
        /// <param name="properties">추가 정보</param>
        /// <returns></returns>
        Task TrackEventAsync(string name, AppCenterProperties properties = null);

        /// <summary>
        /// 예외 정보를 전송합니다.
        /// </summary>
        /// <param name="exception">예외</param>
        /// <param name="properties">추가 정보</param>
        void TrackError(Exception exception, AppCenterProperties properties = null);

        /// <summary>
        /// 예외 정보를 전송합니다.
        /// </summary>
        /// <param name="exception">예외</param>
        /// <param name="properties">추가정보</param>
        /// <returns></returns>
        Task TrackErrorAsync(Exception exception, AppCenterProperties properties = null);

        /// <summary>
        /// 로깅 Verbose
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Verbose(string tag, string message, Exception ex = null);

        void Verbose(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// 로깅 Debug
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Debug(string tag, string message, Exception ex = null);

        void Debug(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// 로깅 Info
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Info(string tag, string message, Exception ex = null);

        void Info(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// 로깅 Warn
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Warn(string tag, string message, Exception ex = null);

        void Warn(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// 로깅 Error
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string tag, string message, Exception ex = null);

        void Error(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// 로깅 Assert
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Assert(string tag, string message, Exception ex = null);

        void Assert(string tag, string message, Exception ex, object currentObj, [CallerMemberName]string memberName = "", [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);
    }
}
