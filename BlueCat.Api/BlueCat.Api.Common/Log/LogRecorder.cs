using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   文本记录器
    /// </summary>
    public static partial class LogRecorder
    {
        /// <summary>
        ///   记录数据库访问日志
        /// </summary>
        public static bool RecorderDataBaseRequest { get; set; }

        /// <summary>
        ///   记录WCF消息日志
        /// </summary>
        public static bool RecorderWcfRequest { get; set; }

        /// <summary>
        ///   消息跟踪器
        /// </summary>
        public static ILogListener Listener { get; set; }

        static LogRecorder()
        { 
            
        }
    }


}
