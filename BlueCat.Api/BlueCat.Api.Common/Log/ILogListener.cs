
namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   表示日志的监听器
    /// </summary>
    public interface ILogListener
    {
        /// <summary>
        ///   显示日志消息
        /// </summary>
        /// <param name="info"> 日志消息 </param>
        void Trace(RecordInfo info);
    }
}
