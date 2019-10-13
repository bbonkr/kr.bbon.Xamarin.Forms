using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms
{
    public class XamarinSupport
    {
        /// <summary>
        /// 메인(UI) 쓰레드에서 실행합니다.
        /// </summary>
        /// <param name="action">실행할 메서드</param>
        public static void RunOnMainThread(Action action)
        {
            Device.BeginInvokeOnMainThread(action);
        }

        /// <summary>
        /// 지정된 간격 후 한번만 실행됩니다.
        /// </summary>
        /// <param name="interval">실행 간격</param>
        /// <param name="action">실행할 메서드</param>
        public static void RunOnceAfter(TimeSpan interval, Action action)
        {
            Device.StartTimer(interval, () =>
            {
                action?.Invoke();

                return false;
            });
        }

        /// <summary>
        /// 지정된 시간마다 실행됩니다.
        /// <para>실행을 중지하려면 <see cref="Func{TResult}"/> 에서 false를 반환해야 합니다.</para>
        /// </summary>
        /// <param name="interval">실행 간격</param>
        /// <param name="func">실행할 메서드</param>
        public static void RunAfter(TimeSpan interval, Func<bool> func)
        {
            Device.StartTimer(interval, () =>
            {
                return func.Invoke();
            });
        }
    }
}
