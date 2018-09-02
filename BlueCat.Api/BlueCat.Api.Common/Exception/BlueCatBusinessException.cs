using System;

namespace BlueCat.Api.Common
{
    /// <summary>
    /// 业务逻辑异常类
    /// </summary>
    [Serializable]
    public class BlueCatBusinessException : BlueCatException
    {
        /// <summary>
        ///   基本构造
        /// </summary>
        public BlueCatBusinessException()
        {
        }

        /// <summary>
        ///   消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        public BlueCatBusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   内联消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        /// <param name="innerException"> 内联消息 </param>
        public BlueCatBusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
