
using System.Diagnostics;
namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        ///   无
        /// </summary>
        None,

        /// <summary>
        ///   系統消息
        /// </summary>
        System,

        /// <summary>
        ///   登录消息
        /// </summary>
        Login,

        /// <summary>
        ///   网络请求
        /// </summary>
        Request,

        /// <summary>
        ///   WCF消息
        /// </summary>
        WcfMessage,

        /// <summary>
        ///   数据库
        /// </summary>
        DataBase,

        /// <summary>
        ///   信息
        /// </summary>
        Message,

        /// <summary>
        ///   调试信息
        /// </summary>
        Trace,

        /// <summary>
        ///   警告
        /// </summary>
        Warning,

        /// <summary>
        ///   错误
        /// </summary>
        Error,

        /// <summary>
        ///   异常
        /// </summary>
        Exception,

        /// <summary>
        ///   计划
        /// </summary>
        Plan,

        /// <summary>
        ///   监视
        /// </summary>
        Monitor
    }
}
