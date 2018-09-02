using BlueCat.Api.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlueCat.Api.Common.Log
{
    /// <summary>
    /// 日志异常
    /// </summary>
    public static class RecordException
    {
        /// <summary>
        ///   记录异常的外部信息
        /// </summary>
        /// <param name="id"> 日志查询标识 </param>
        /// <param name="ex"> 异常 </param>
        /// <returns> </returns>
        public static string ExceptionMessage(Guid id, Exception ex)
        {
            if (ex != null)
            {

                if (ex is SqlException)
                {
                    return BlueCatSystemException.SqlExceptionLevel(ex as SqlException) > 16
                                   ? String.Format("发生服务器错误,系统标识:{0}", id)
                                   : String.Format("发生服务器错误,{1},系统标识:{0}", id, ex.Message);
                }
                if (ex is SystemException)
                {
                    return String.Format("发生系统错误,系统标识:{0}", id);
                }
                if (ex is BlueCatSystemException)
                {
                    return String.Format("发生内部错误,系统标识:{0}", id);
                }
                if (ex is BugException)
                {
                    return String.Format("发生设计错误,系统标识:{0}", id);
                }
                if (ex is BlueCatBusinessException)
                {
                    return String.Format("发生业务逻辑错误,内容为:{1},系统标识:{0}", id, ex.Message);
                }

                return String.Format("发生未知错误,系统标识:{0}", id);
            }

            return "发生未处理异常";
        }

        /// <summary>
        ///   记录异常的详细信息
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="ex"> </param>
        /// <param name="message"> </param>
        /// <param name="xml"> </param>
        /// <returns> </returns>
        public static string ExceptionInfomation(Guid id, Exception ex, string message, out string xml)
        {
            string outmsg = "发生未处理异常";
            string tag = "";
            if (ex != null)
            {
                if (ex is BlueCatSystemException)
                {
                    tag = "系统致命错误";
                    outmsg = String.Format("发生内部错误,系统标识:{0}", id);
                }
                else if (ex is BugException)
                {
                    tag = "存在设计缺陷";
                    outmsg = String.Format("发生设计错误,系统标识:{0}", id);
                }
                else if (ex is BlueCatBusinessException)
                {
                    tag = "业务逻辑错误";
                    outmsg = String.Format("发生错误,内容为:{1},系统标识:{0}", id, ex.Message);
                }
                else if (ex is SqlException)
                {
                    if (BlueCatSystemException.SqlExceptionLevel(ex as SqlException) > 16)
                    {
                        tag = "数据库致命错误(级别大于16)";
                        outmsg = String.Format("发生服务器错误,系统标识:{0}", id);
                    }
                    else
                    {
                        tag = "数据库一般错误(级别小等于16)";
                        outmsg = String.Format("发生服务器错误,{1},系统标识:{0}", id, ex.Message);
                    }
                }
                else if (ex is SystemException)
                {
                    tag = "系统错误";
                    outmsg = String.Format("发生系统错误,系统标识:{0}", id);
                }
                else
                {
                    tag = "未知错误";
                    outmsg = String.Format("发生未知错误,系统标识:{0}", id);
                }
            }
            XElement element = new XElement("ExceptionInfomation",
                                       new XElement("ID", id),
                                       new XElement("Tag", tag),
                                       new XElement("RecordType", "Exception"),
                                       new XElement("OutMessage", outmsg));
            if (!string.IsNullOrWhiteSpace(message))
            {
                if (message[0] == '<')
                {
                    try
                    {
                        element.Add(new XElement("Tag", new XElement(message)));
                    }
                    catch
                    {
                        element.Add(new XElement("Message", message));
                    }
                }
                else
                {
                    element.Add(new XElement("Message", message));
                }
            }
            if (ex != null)
            {
                ReflectionHelper.SerializeException(ex, element);
            }
            xml = element.ToString(SaveOptions.None);
            return outmsg;
        }
    }
}
