using BlueCat.Api.Common.Extend.ScopeBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   文本记录器
    /// </summary>
    public static partial class LogRecorder
    {
        #region 对象
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

        /// <summary>
        ///   静态构造
        /// </summary>
        static LogRecorder()
        {
            Recorder = new TxtRecorder();
            Recorder.Initialize();
            var logThread = new Thread(WriteRecordLoop) { IsBackground = true, Priority = ThreadPriority.Lowest };
            logThread.Start();
        }

        /// <summary>
        ///   记录器
        /// </summary>
        public static ILogRecorder Recorder { get; private set; }

        private static bool IsTextRecorder;
        /// <summary>
        ///   初始化
        /// </summary>
        /// <param name="record"> </param>
        public static void Initialize(ILogRecorder record)
        {
            Recorder = record ?? new TxtRecorder();
            IsTextRecorder = record == null || record is TxtRecorder;
            Recorder.Initialize();
        }

        /// <summary>
        ///   中止
        /// </summary>
        public static void Shutdown()
        {
            Recorder.Shutdown();
        }

        /// <summary>
        /// 取请求ID的方法
        /// </summary>
        public static Func<Guid> GetRequestIdFunc;

        /// <summary>
        /// 取请求ID的方法
        /// </summary>
        static Guid GetRequestId()
        {
            return GetRequestIdFunc.Invoke() == null ? Guid.NewGuid() : GetRequestIdFunc.Invoke();
            //return Guid.NewGuid();
        }
        #endregion

        #region 记录
        ///<summary>
        ///  记录一般日志
        ///</summary>
        ///<param name="type"> 日志类型(SG) </param>
        ///<param name="name"> </param>
        ///<param name="message"> 消息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        [Conditional("TRACE")]
        public static void Trace(LogType type, string name, string message, params object[] formatArgs)
        {
            Record(GetRequestId(), name, FormatMessage(message, formatArgs), type);
        }
        ///<summary>
        ///  记录一般日志
        ///</summary>
        ///<param name="type"> 日志类型(SG) </param>
        ///<param name="name"> </param>
        ///<param name="message"> 消息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void Record(LogType type, string name, string message, params object[] formatArgs)
        {
            Record(GetRequestId(), name, FormatMessage(message, formatArgs), type);
        }

        /// <summary>
        ///   记录一般日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        /// <param name="type"> 日志类型(SG) </param>
        public static void Record(string msg, LogType type = LogType.Message, bool isCompleted = false)
        {
            RecordLogs(GetRequestId(), type.ToString(), msg, type, isCompleted);
        }

        ///<summary>
        ///  记录一般日志
        ///</summary>
        ///<param name="type"> 日志类型(SG) </param>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void Record(string type, string message, params object[] formatArgs)
        {
            Record(GetRequestId(), type, FormatMessage(message, formatArgs), LogType.None, type);
        }

        ///<summary>
        ///  记录数据日志
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        [Conditional("TRACE")]
        public static void RecordDataLog(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "数据日志", FormatMessage(message, formatArgs), LogType.DataBase);
        }

        ///<summary>
        ///  记录登录日志
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void RecordLoginLog(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "登录日志", FormatMessage(message, formatArgs), LogType.Login);
        }

        ///<summary>
        ///  记录网络请求日志
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        [Conditional("TRACE")]
        public static void RecordRequestLog(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "网络请求", FormatMessage(message, formatArgs), LogType.Request);
        }

        ///<summary>
        ///  记录WCF消息日志
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        [Conditional("TRACE")]
        public static void RecordWcfLog(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "WCF消息", FormatMessage(message, formatArgs), LogType.WcfMessage);
        }

        ///<summary>
        ///  记录消息
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void Message(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "消息", FormatMessage(message, formatArgs), LogType.Message);
        }
        /// <summary>
        ///   记录堆栈跟踪
        /// </summary>
        /// <param name="title"> 标题 </param>
        [Conditional("TRACE")]
        public static void RecordStackTrace(string title)
        {
            Record(GetRequestId(), "堆栈跟踪:" + title, StackTraceInfomation(title), LogType.Trace);
        }

        ///<summary>
        ///  写入调试日志
        ///</summary>
        ///<param name="recordStackTrace"> 记录堆栈信息吗 </param>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        [Conditional("TRACE")]
        public static void Trace(bool recordStackTrace, string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "调试", FormatMessage(message, formatArgs), LogType.Trace);
        }

        /// <summary>
        ///   写入调试日志
        /// </summary>
        /// <param name="message"> 日志详细信息 </param>
        /// <param name="formatArgs">格式化参数</param>
        [Conditional("TRACE")]
        public static void Trace(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "调试", FormatMessage(message, formatArgs), LogType.Trace);
        }
        /// <summary>
        ///   写入调试日志
        /// </summary>
        /// <param name="obj"> 记录对象 </param>
        [Conditional("TRACE")]
        public static void Trace(object obj)
        {
            Record(GetRequestId(), "调试", obj == null ? "NULL" : obj.ToString(), LogType.Trace);
        }

        /// <summary>
        ///   记录系统日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        public static void SystemLog(string msg)
        {
            Record(GetRequestId(), "系统", msg, LogType.System);
        }

        /// <summary>
        ///   记录系统日志
        /// </summary>
        /// <param name="message"> 日志详细信息 </param>
        /// <param name="formatArgs">格式化参数</param>
        public static void SystemLog(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "系统", FormatMessage(message, formatArgs), LogType.Trace);
        }

        /// <summary>
        ///   记录系统日志
        /// </summary>
        /// <param name="msg"> 消息 </param>
        public static void PlanLog(string msg)
        {
            Record(GetRequestId(), "计划", msg, LogType.System);
        }

        ///<summary>
        ///  写入一般日志
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void RecordMessage(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "消息", FormatMessage(message, formatArgs), LogType.Message);
        }

        ///<summary>
        ///  记录警告消息
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void Warning(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "警告", FormatMessage(message, formatArgs), LogType.Warning);
        }

        ///<summary>
        ///  记录错误消息
        ///</summary>
        ///<param name="message"> 日志详细信息 </param>
        ///<param name="formatArgs"> 格式化的参数 </param>
        public static void Error(string message, params object[] formatArgs)
        {
            Record(GetRequestId(), "错误", StackTraceInfomation(FormatMessage(message, formatArgs)), LogType.Error);
        }

        /// <summary>
        ///   记录异常日志
        /// </summary>
        /// <param name="exception"> 异常 </param>
        /// <param name="message"> 日志详细信息 </param>
        public static Guid RecordException(Exception exception, out string message)
        {

            Guid id = GetRequestId();
            message = ExceptionMessage(id, exception);
            string xml;
            ExceptionInfomation(id, exception, null, out xml);
            string title = "异常";
            if (exception != null)
            {
                title = exception.Message;
            }
            Record(id, title, xml, LogType.Exception);
            return id;
        }

        /// <summary>
        ///   记录异常日志
        /// </summary>
        /// <param name="ex"> 异常 </param>
        /// <param name="message"> 日志详细信息 </param>
        public static string Exception(Exception ex, string message = null)
        {
            Guid id = GetRequestId();
            string xml;
            string re = ExceptionInfomation(id, ex, message, out xml);
            Record(id, "异常", xml, LogType.Exception);
            return re;
        }

        /// <summary>
        ///   记录异常日志
        /// </summary>
        /// <param name="e"> 异常 </param>
        /// <param name="message"> 日志详细信息 </param>
        /// <param name="formatArgs">格式化参数</param>
        public static string Exception(Exception e, string message, params object[] formatArgs)
        {
            Guid id = GetRequestId();
            string xml;
            string re = ExceptionInfomation(id, e, FormatMessage(message, formatArgs), out xml);
            if (e != null)
            {
                message = e.Message;
            }
            Record(id, message, xml, LogType.Exception);
            return re;
        }
        #endregion

        #region 写入
        /// <summary>
        /// 日志序号
        /// </summary>
        static int _id = 1;
        /// <summary>
        /// 用于对象锁定
        /// </summary>
        static readonly object lockTooken = new RecordInfo();

        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="id"> 标识 </param>
        /// <param name="name"> 原始的消息 </param>
        /// <param name="msg"> 处理后的消息 </param>
        /// <param name="type"> 日志类型 </param>
        /// <param name="typeName"> 类型名称 </param>
        private static void Record(Guid id, string name, string msg, LogType type, string typeName = null)
        {
            if (type == LogType.None)
            {
                type = LogType.Message;
            }
            int idx;
            using (ThreadLockScope.Scope(lockTooken))
            {
                idx = _id++;
            }
            Push(new RecordInfo
            {
                LogId = id,
                Index = idx,
                Name = name,
                Type = type,
                Message = msg,
                ThreadID = Thread.CurrentThread.ManagedThreadId,
                TypeName = typeName ?? TypeToString(type)
            });
        }


        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="id"> 标识 </param>
        /// <param name="name"> 原始的消息 </param>
        /// <param name="msg"> 处理后的消息 </param>
        /// <param name="type"> 日志类型 </param>
        /// <param name="typeName"> 类型名称 </param>
        private static void RecordLogs(Guid id, string name, string msg, LogType type, bool isCompleted, string typeName = null)
        {
            if (type == LogType.None)
            {
                type = LogType.Message;
            }
            int idx;
            using (ThreadLockScope.Scope(lockTooken))
            {
                idx = _id++;
            }
            Push(new RecordInfo
            {
                LogId = id,
                Index = idx,
                Name = name,
                Type = type,
                Message = msg,
                IsCompleted = isCompleted,
                ThreadID = Thread.CurrentThread.ManagedThreadId,
                TypeName = typeName ?? TypeToString(type)
            });
        }


        /// <summary>
        /// 待写入的日志信息集合
        /// </summary>
        static readonly List<RecordInfo> recordInfos = new List<RecordInfo>();

        /// <summary>
        ///   入队列
        /// </summary>
        /// <param name="info"> </param>
        private static void Push(RecordInfo info)
        {
            if (Thread.CurrentPrincipal != null)
            {
                info.User = Thread.CurrentPrincipal.Identity.Name;
            }
            using (ThreadLockScope.Scope(recordInfos))
            {
                recordInfos.Add(info);
            }
        }

        /// <summary>
        ///  日志记录独立线程
        /// </summary>
        /// <param name="arg"> </param>
        private static void WriteRecordLoop(object arg)
        {
            while (true)
            {
                Thread.Sleep(3);
                List<RecordInfo> infos;
                List<Guid> distinctLogIds;
                int distinctLogIdsCount = 0;
                using (ThreadLockScope.Scope(recordInfos))
                {
                    //infos = recordInfos.ToArray();
                    //recordInfos.Clear();
                    var logIds = recordInfos.Where(x => x.IsCompleted).Select(x => x.LogId).ToList();
                    distinctLogIdsCount = logIds.Distinct().ToList().Count;
                    distinctLogIds = logIds.Distinct().ToList();
                    infos = recordInfos.Where(x => logIds.Contains(x.LogId)).ToList();
                    recordInfos.RemoveAll(x => logIds.Contains(x.LogId));
                }
                //foreach (var info in infos)
                //{
                //    Thread.Sleep(3);//释放一次时间片,以保证主要线程的流畅性
                //    WriteToLog(info);
                //}
                foreach (var logId in distinctLogIds)
                {
                    Thread.Sleep(3);//释放一次时间片,以保证主要线程的流畅性

                    WriteLogsToLog(infos.Where(x => x.LogId == logId).ToList());
                }
            }
        }

        private static void WriteLogsToLog(List<RecordInfo> infos)
        {
            try
            {
                foreach (var info in infos)
                {
                    Recorder.RecordLog(info);
                }
            }
            catch (Exception ex)
            {
                SystemTrace("日志写入发生错误", ex);
            }
        }


        private static void WriteToLog(RecordInfo info)
        {
            try
            {
                if (Listener != null)
                {
                    Listener.Trace(info);
                    Thread.Sleep(0);
                }
                else
                {
                    SystemTrace(info.Message);
                }
            }
            catch (Exception ex)
            {
                SystemTrace("日志侦听器发生错误", ex);
            }
            try
            {
                if (info.Type == LogType.Trace)
                {
                    TxtRecorder.RecordTrace(info.Message);
                    return;
                }
                if (info.Type == LogType.Monitor)
                {
                    TxtRecorder.RecordTrace(info.Message, ".monitor");
                    return;
                }

                Recorder.RecordLog(info);
            }
            catch (Exception ex)
            {
                SystemTrace("日志写入发生错误", ex);
            }
        }

        /// <summary>
        /// 写入系统跟踪
        /// </summary>
        /// <param name="arg"></param>
        [Conditional("TRACE")]
        public static void SystemTrace(object arg)
        {
            System.Diagnostics.Trace.WriteLine(arg);
        }

        /// <summary>
        /// 写入系统跟踪
        /// </summary>
        /// <param name="title"></param>
        /// <param name="arg"></param>
        [Conditional("TRACE")]
        public static void SystemTrace(string title, object arg)
        {
            System.Diagnostics.Trace.WriteLine(arg, title);
        }

        #endregion
    }
}
