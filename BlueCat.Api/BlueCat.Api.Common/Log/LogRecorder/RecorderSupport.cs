using System.Diagnostics;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    /// 日志支持
    /// </summary>
    public class RecorderSupport 
    {
        /// <summary>
        ///   日志类型到文本
        /// </summary>
        /// <param name="type"> </param>
        /// <param name="def"> </param>
        /// <returns> </returns>
        public static string TypeToString(LogType type, string def = null)
        {
            switch (type)
            {
                default:
                    return def ?? "None";
                case LogType.Plan:
                    return "Plan";
                case LogType.Trace:
                    return "Debug";
                case LogType.Message:
                    return "Message";
                case LogType.Warning:
                    return "Warning";
                case LogType.Error:
                    return "Error";
                case LogType.Exception:
                    return "Exception";
                case LogType.System:
                    return "System";
                case LogType.Login:
                    return "Login";
                case LogType.Request:
                    return "Request";
                case LogType.DataBase:
                    return "DataBase";
                case LogType.WcfMessage:
                    return "WcfMessage";
                case LogType.Monitor:
                    return "Monitor";
            }
        }

        static string FormatMessage(string message, object[] formatArgs)
        {
            string msg = null;
            if (message != null)
            {
                if (formatArgs == null || formatArgs.Length == 0)
                {
                    msg = message;
                }
                else
                {
                    msg = string.Format(message, formatArgs);
                }
            }
            return msg;
        }

        /// <summary>
        ///   堆栈信息
        /// </summary>
        /// <param name="title"> 标题 </param>
        public static string StackTraceInfomation(string title = null)
        {
            return string.Format(@"{0}:{1}", title, new StackTrace());
        }
    }
}
