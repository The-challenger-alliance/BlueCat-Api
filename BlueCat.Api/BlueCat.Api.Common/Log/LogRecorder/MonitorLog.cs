﻿using BlueCat.Api.Common.Extend.ScopeBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    ///   文本记录器
    /// </summary>
    partial class LogRecorder
    {
        /// <summary>
        /// 侦测吗
        /// </summary>
        private static bool InMonitor
        {
            get { return MonitorItem.InMonitor; }
            set { MonitorItem.InMonitor = value; }
        }

        /// <summary>
        /// 开始检测资源
        /// </summary>
        [Conditional("Monitor")]
        public static void BeginMonitor(string title)
        {
            if (!InMonitor || MonitorItem.MonitorStack == null)
                BeginMonitorInner(title);
            else
                BeginStepMonitorInner(title);
        }
        /// <summary>
        /// 锁定
        /// </summary>
        private static readonly object LockKey = new object();

        static void BeginMonitorInner(string title)
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                InMonitor = true;
                if (!AppDomain.MonitoringIsEnabled)
                    AppDomain.MonitoringIsEnabled = true;
                MonitorItem.MonitorTexter = new StringBuilder();
                StringBuilder sb = new StringBuilder();
                sb.Append(' ', 24);
                sb.Append("标题");
                sb.Append(' ', 24);
                sb.Append("|状态|   时间   |   用 时(ms)   |  CPU(ms) |内存分配Kb| 总分配Mb |内存驻留Kb| 总驻留Mb |");//   计数A  |   计数B  |   计数C  |
                ShowMinitor(sb.ToString());

                MonitorItem.MonitorStack = new FixStack<MonitorItem>();
                MonitorItem.MonitorStack.SetFix(new MonitorItem(title));
                ShowMinitor(title, 0);
            }
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void BeginStepMonitor(string title)
        {
            if (!InMonitor || MonitorItem.MonitorStack == null || MonitorItem.MonitorStack.Current == null)
            {
                BeginMonitorInner(title);
            }
            else
            {
                BeginStepMonitorInner(title);
            }
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        public static void BeginStepMonitorInner(string title)
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                MonitorItem.MonitorStack.Push(new MonitorItem(title));
                ShowMinitor(title, 1);
            }
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void EndStepMonitor()
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                if (MonitorItem.MonitorStack == null || MonitorItem.MonitorStack.Current == null)
                    return;
                MonitorItem.MonitorStack.Current.Coll();
                if (MonitorItem.MonitorStack.StackCount == 0)
                {
                    EndMonitorInner();
                    return;
                }
                MonitorItem.MonitorStack.Current.EndMessage();
                ShowMinitor(MonitorItem.MonitorStack.Current.Title, 2);
                MonitorItem pre = MonitorItem.MonitorStack.Current;
                MonitorItem.MonitorStack.Pop();
                MonitorItem.MonitorStack.Current.Coll(pre);
                MonitorItem.MonitorStack.Current.Flush();
            }
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void EndAllStepMonitor()
        {
            EndAllStepMonitorInner();
        }
        static void EndAllStepMonitorInner()
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                if (MonitorItem.MonitorStack.FixValue == MonitorItem.MonitorStack.Current)
                {
                    MonitorItem.MonitorStack.FixValue.Coll();
                    return;
                }
                MonitorItem.MonitorStack.Current.Coll();
                MonitorItem pre = MonitorItem.MonitorStack.Current;
                MonitorItem.MonitorStack.Current.EndMessage();
                ShowMinitor(MonitorItem.MonitorStack.Current.Title, 2);
                MonitorItem.MonitorStack.Pop();
                while (MonitorItem.MonitorStack.StackCount > 0)
                {
                    MonitorItem.MonitorStack.Current.Coll(pre);
                    pre = MonitorItem.MonitorStack.Current;
                    MonitorItem.MonitorStack.Current.EndMessage();
                    ShowMinitor(MonitorItem.MonitorStack.Current.Title, 2);
                    MonitorItem.MonitorStack.Pop();
                }
                if (pre != null)
                {
                    MonitorItem.MonitorStack.FixValue.Coll(pre);
                }
            }
        }
        /// <summary>
        /// 显示监视跟踪
        /// </summary>
        [Conditional("Monitor")]
        public static void MonitorTrace(string message)
        {
            ShowMinitor(message, 4, false);
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void FlushMonitor(string title, bool number = false)
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                if (MonitorItem.MonitorStack == null || MonitorItem.MonitorStack.Current == null)
                    return;
                MonitorItem.MonitorStack.Current.Coll();
                MonitorItem.MonitorStack.Current.FlushMessage();
                ShowMinitor(title, 4);
                MonitorItem.MonitorStack.Current.Flush();
            }
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void FlushMonitor(string fmt, params object[] args)
        {
            FlushMonitor(string.Format(fmt, args));
        }
        /// <summary>
        /// 刷新资源检测
        /// </summary>
        [Conditional("Monitor")]
        public static void EndMonitor()
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                if (MonitorItem.MonitorStack == null || MonitorItem.MonitorStack.Current == null)
                    return;
                EndAllStepMonitorInner();
                EndMonitorInner();
            }
        }
        static void EndMonitorInner()
        {
            using (ThreadLockScope.Scope(LockKey))
            {
                try
                {
                    MonitorItem.MonitorStack.FixValue.EndMessage();
                    string title = MonitorItem.MonitorStack.FixValue.Title ?? "Monitor";
                    ShowMinitor(title, 3);
                    string log = MonitorItem.MonitorTexter.ToString();

                    Record(GetRequestId(), title, log, LogType.Monitor);

                    //if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                    //    Clipboard.SetText(MonitorItem.MonitorTexter.ToString());
                }
                catch
                { }
                finally
                {
                    InMonitor = false;

                }
            }
        }
        static void ShowMinitor(string title, int type, bool showMonitorValue = true)
        {
            if (MonitorItem.MonitorStack == null || MonitorItem.MonitorStack.Current == null)
                return;
            StringBuilder sb = new StringBuilder();
            int cnt = MonitorItem.MonitorStack.StackCount;
            switch (type)
            {
                case 0:
                case 1:
                    if (cnt > 0)
                        sb.Append('│', cnt);
                    sb.Append('┌');
                    break;
                //if (cnt > 0)
                //    sb.Append('│', cnt);
                //sb.Append('┬');
                //break;
                case 2:
                    if (cnt > 1)
                        sb.Append('│', cnt - 1);
                    sb.Append("├┴");
                    break;
                case 3:
                    if (cnt > 0)
                        sb.Append('│', cnt);
                    sb.Append('└');
                    break;
                default:
                    if (cnt > 0)
                        sb.Append('│', cnt);
                    sb.Append("├─");
                    cnt++;
                    break;
            }
            if (string.IsNullOrWhiteSpace(title))
                title = "*";
            sb.Append(title);
            if (showMonitorValue)
            {
                int l = cnt * 2 + title.GetLen();
                if (l < 50)
                    sb.Append(' ', 50 - l);
                sb.Append(MonitorItem.MonitorStack.Current.message);
            }
            ShowMinitor(sb.ToString());
        }
        static void ShowMinitor(string msg)
        {
            SystemTrace(msg);
            using (ThreadLockScope.Scope(LockKey))
            {
                MonitorItem.MonitorTexter.AppendLine(msg);
            }
        }
    }
}
