
namespace BlueCat.Api.Common.Log
{

    /// <summary>
    ///   记录器
    /// </summary>
    public interface ILogRecorder
    {
        /// <summary>
        ///   初始化
        /// </summary>
        void Initialize();

        /// <summary>
        ///   停止
        /// </summary>
        void Shutdown();

        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"> 日志消息 </param>
        void RecordLog(RecordInfo info);
    }
}
