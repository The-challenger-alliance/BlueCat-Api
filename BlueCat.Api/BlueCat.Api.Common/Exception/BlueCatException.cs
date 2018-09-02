using System;
using System.Runtime.Serialization;

namespace BlueCat.Api.Common
{
    public class BlueCatException : Exception
    {
        /// <summary>
        ///   格式化异常
        /// </summary>
        /// <param name="err"> 异常 </param>
        /// <returns> 文本 </returns>
        public static string FormatException(Exception err)
        {
            if (err == null)
            {
                return string.Empty;
            }

            return string.Format("发生错误\"{0}\":\r\n堆栈:{1}\r\n{2}", err.Message, err.StackTrace, FormatException(err.InnerException));
        }

        /// <summary>
        ///   基本构造
        /// </summary>
        public BlueCatException()
        {
        }

        /// <summary>
        ///   消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        public BlueCatException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   内联消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        /// <param name="innerException"> 内联消息 </param>
        public BlueCatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///   序列化构造
        /// </summary>
        /// <param name="info"> 序列化对象 </param>
        /// <param name="context"> 数据流上下文 </param>
        protected BlueCatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
