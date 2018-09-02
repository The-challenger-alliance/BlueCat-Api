
namespace BlueCat.Api.Common
{
    /// <summary>
    ///   远程返回的异常的信息
    /// </summary>
    public class ExceptionInfo
    {
        /// <summary>
        ///   主要信息
        /// </summary>
        public string RootMessage { get; set; }

        /// <summary>
        ///   节点
        /// </summary>
        public RemoteExceptionInfoItem Item { get; set; }

        /// <summary>
        ///   简要信息,已做简单的HTML格式化
        /// </summary>
        public string BriefMessage
        {
            get
            {
                RemoteExceptionInfoItem it = this.Item;
                while (it != null)
                {
                    if (!string.IsNullOrWhiteSpace(it.Reason))
                    {
                        return string.Format("{0}<BR/>代码:{2}<BR/>信息:{1}", this.RootMessage, it.Reason, this.ErrorCode);
                    }
                    it = it.Item;
                }
                return this.RootMessage;
            }
        }

        /// <summary>
        ///   平台提供的错误代码--可能为空
        /// </summary>
        public string ErrorCode
        {
            get
            {
                RemoteExceptionInfoItem it = this.Item;
                while (it != null)
                {
                    if (!string.IsNullOrWhiteSpace(it.ErrorCode))
                    {
                        return it.ErrorCode;
                    }
                    it = it.Item;
                }
                return "-1";
            }
        }
    }

    /// <summary>
    ///   远程返回的异常的信息节点
    /// </summary>
    public class RemoteExceptionInfoItem
    {
        /// <summary>
        ///   子节点
        /// </summary>
        public RemoteExceptionInfoItem Item { get; set; }

        /// <summary>
        ///   异常类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///   异常的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///   异常的消息源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///   异常的栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        ///   平台异常的信息
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///   平台异常的错误代码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        ///   平台异常的其它错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
