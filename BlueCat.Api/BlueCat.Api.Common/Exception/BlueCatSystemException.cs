using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace BlueCat.Api.Common
{
    [Serializable]
    public class BlueCatSystemException : BlueCatException
    {
        /// <summary>
        ///   内容的详细信息
        /// </summary>
        public string InnerMessage { get; set; }

        /// <summary>
        ///   扩展信息
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        ///   基本构造
        /// </summary>
        public BlueCatSystemException()
        {
        }

        /// <summary>
        ///   消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        public BlueCatSystemException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        ///   基本构造
        /// </summary>
        public BlueCatSystemException(Exception serr) : this(serr.Message , serr)
        {
        }

        /// <summary>
        ///   错误类型
        /// </summary>
        public SystemErrorType ErrorType { get ; set ; }

        /// <summary>
        ///   基本构造
        /// </summary>
        public BlueCatSystemException(System.Data.Common.DbException serr) : this(serr.Message , serr)
        {
        }

        /// <summary>
        ///   内联消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        /// <param name="innerException"> 内联消息 </param>
        public BlueCatSystemException(string message , Exception innerException) : base(message , innerException)
        {
            this.ErrorType = SystemErrorType.UnknowError ;
        }

        /// <summary>
        ///   内联消息构造
        /// </summary>
        /// <param name="message"> 消息 </param>
        /// <param name="innerException"> 内联消息 </param>
        /// <param name="errtype"> 异常类型 </param>
        /// <param name="innermessage"> 内部扩展消息 </param>
        public BlueCatSystemException(string message , SystemErrorType errtype , string innermessage , Exception innerException) : base(message , innerException)
        {
            this.ErrorType = errtype ;
            this.InnerMessage = innermessage ;
        }

        /// <summary>
        ///   序列化构造
        /// </summary>
        /// <param name="info"> 序列化对象 </param>
        /// <param name="context"> 数据流上下文 </param>
        protected BlueCatSystemException(SerializationInfo info , StreamingContext context) : base(info , context)
        {
        }

        /// <summary>
        ///   基本构造
        /// </summary>
        public static BlueCatException OnAgebullDatabaseException(SqlException serr)
        {
            if(SqlExceptionLevel(serr) > 16)
            {
                return new BlueCatSystemException("系统内部错误" , SystemErrorType.DataBaseError , serr.Message , serr) ;
            }
            return new BugException(serr.Message , serr) ;
        }

        /// <summary>
        ///   基本构造
        /// </summary>
        public static int SqlExceptionLevel(SqlException serr)
        {
            int errclass = 0 ;
            foreach(SqlError se in serr.Errors)
            {
                if(se.Class > errclass)
                {
                    errclass = se.Class ;
                }
            }
            return errclass ;
        }

        /// <summary>
        /// </summary>
        /// <param name="serr"> </param>
        /// <returns> </returns>
        public static Exception OnBlueCatDatabaseException(DbException serr)
        {
            return new BlueCatSystemException("系统内部错误" , SystemErrorType.DataBaseError , serr.Message , serr) ;
        }
    }
}
