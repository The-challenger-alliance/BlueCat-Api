﻿using BlueCat.Api.Common.Extend.ScopeBase;
using NLog.Internal;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace BlueCat.Api.Common.Log
{
    public sealed class TxtRecorder : ILogRecorder
    {
        /// <summary>
        ///   初始化
        /// </summary>
        public static TxtRecorder Recorder = new TxtRecorder();

        /// <summary>
        ///   初始化
        /// </summary>
        public void Initialize()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LogPath))
                {
                    var cfgpath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
                    if (string.IsNullOrWhiteSpace(cfgpath))
                    {
                        string exeth = Path.GetDirectoryName(GetType().Assembly.Location);
                        if (exeth != null)
                        {
                            cfgpath = Path.Combine(exeth, "log");
                        }
                    }
                    LogPath = cfgpath;
                }
                if (LogPath != null && !Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
            }
            catch (Exception ex)
            {
                LogRecorder.SystemTrace("日志记录:TextRecorder.Initialize", ex);
            }
        }

        /// <summary>
        ///   停止
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        ///   文本日志的路径,如果不配置,就为:[应用程序的路径]\log\
        /// </summary>
        public static string LogPath { get; set; }

        // <summary>
        ///   记录日志
        /// </summary>
        /// <param name="id"> 标识 </param>
        /// <param name="msg"> 消息 </param>
        /// <param name="type"> 日志类型 </param>
        /// <param name="user"> 当前操作者 </param>
        /// <param name="name"> 标识的文件后缀(如.error,则文件名可能为 20160602.error.log) </param>
        private void RecordLog(Guid id, string msg, string type, string user = null, string name = null)
        {
            string xml = type == "DataBase" ? string.Format("{0}\r\n", msg) : string.Format(@"Date:{0}
Type:{1}
User:{2}
{3}", DateTime.Now.ToString(CultureInfo.InvariantCulture), type, user, msg) + "\r\n"; 
      
            try
            {
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
                string path = Path.Combine(LogPath, string.Format("{0}{1}.log", DateTime.Now.ToString("yyyyMMdd"), name));

                using (ThreadLockScope.Scope(this))
                {
                    File.AppendAllText(path, xml, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
               LogRecorder.SystemTrace("日志记录:TextRecorder.RecordLog4", ex);
            }
        }

        /// <summary>
        ///   记录日志
        /// </summary>
        private void RecordTraceInner(string message, string type = ".trace")
        { 
             if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }

            string path = Path.Combine(LogPath,string.Format("{0}{1}.log", DateTime.Now.ToString("yyyyMMdd"), type));

            using (ThreadLockScope.Scope(this))
            {
                File.AppendAllText(path, message + "\n", Encoding.UTF8);
            }
        }

        /// <summary>
        ///   记录跟踪信息
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="type"></param>
        public static void RecordTrace(string message, string type = ".trace")
        {
            try
            {
                Recorder.RecordTraceInner(message, type);
            }
            catch (Exception ex)
            {
                LogRecorder.SystemTrace("日志记录:TextRecorder.RecordLog1", ex);
            }
        }


        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"> 日志消息 </param>
        public void RecordLog(RecordInfo info)
        {
            switch (info.Type)
            {
                case LogType.System:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".system");
                    break;
                case LogType.Login:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".user");
                    break;
                case LogType.Request:
                case LogType.WcfMessage:
                case LogType.Trace:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".trace");
                    break;
                case LogType.DataBase:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".sql");
                    break;
                case LogType.Warning:
                case LogType.Error:
                case LogType.Exception:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".error");
                    break;
                case LogType.Plan:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".plan");
                    break;
                case LogType.Monitor:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User, ".monitor");
                    break;
                default:
                    this.RecordLog(info.LogId, info.Message, info.TypeName, info.User);
                    break;
            }
        }

        /// <summary>
        ///   写消息--Trace
        /// </summary>
        /// <param name="message"> </param>
        public void WriteLine(string message)
        {
            this.RecordLog(Guid.NewGuid(), message, TypeToString(LogType.Trace));
        }

        /// <summary>
        ///   日志类型到文本
        /// </summary>
        /// <param name="type"> </param>
        /// <param name="def"> </param>
        /// <returns> </returns>
        public string TypeToString(LogType type, string def = null)
        {
            switch (type)
            {
                default:
                    return def ?? "None";
                case LogType.Plan:
                    return "Plan";
                case LogType.Trace:
                    return "Debug";
                case LogType.Message:
                    return "Message";
                case LogType.Warning:
                    return "Warning";
                case LogType.Error:
                    return "Error";
                case LogType.Exception:
                    return "Exception";
                case LogType.System:
                    return "System";
                case LogType.Login:
                    return "Login";
                case LogType.Request:
                    return "Request";
                case LogType.DataBase:
                    return "DataBase";
                case LogType.WcfMessage:
                    return "WcfMessage";
                case LogType.Monitor:
                    return "Monitor";
            }
        }
    }
}
