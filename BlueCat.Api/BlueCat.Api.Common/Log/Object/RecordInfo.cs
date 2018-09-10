using System;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   记录信息
    /// </summary>
    public class RecordInfo
    {
        public RecordInfo()
        {
            this.IsCompleted = false;
        }

        /// <summary>
        ///   日志记录序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///   线程ID
        /// </summary>
        public int ThreadID { get; set; }

        /// <summary>
        ///   日志ID
        /// </summary>
        public Guid LogId { get; set; }

        /// <summary>
        ///   名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   格式化消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///   日志类型
        /// </summary>
        public LogType Type { get; set; }

        /// <summary>
        ///   日志扩展名称,类型为None
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        ///   当前用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 是否完结
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
