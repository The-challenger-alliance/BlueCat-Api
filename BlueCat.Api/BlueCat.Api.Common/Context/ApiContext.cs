using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common.Context
{
    /// <summary>
    /// API调用上下文（流程中使用）
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ApiContext
    {

        /// <summary>
        /// 当前调用上下文
        /// </summary>
        private InternalCallContext requestContext;

        /// <summary>
        /// 当前调用上下文
        /// </summary>
        private UserProfile user;

        /// <summary>
        /// 设置当前上下文
        /// </summary>
        /// <param name="context"></param>
        public static void SetContext(ApiContext context)
        {
            CallContext.LogicalSetData("ApiContext", context);
        }

        /// <summary>
        /// 设置当前用户
        /// </summary>
        /// <param name="customer"></param>
        public static void SetCustomer(UserProfile customer)
        {
            Current.user = customer;
        }

        /// <summary>
        /// 设置当前请求上下文
        /// </summary>
        /// <param name="context"></param>
        public static void SetRequestContext(InternalCallContext context)
        {
            Current.requestContext = context;
        }

        /// <summary>
        /// 当前实例对象
        /// </summary>
        public static ApiContext Current
        {
            get
            {
                var current = CallContext.LogicalGetData("ApiContext") as ApiContext;
                if (current != null)
                    return current;
                CallContext.LogicalSetData("ApiContext", current = new ApiContext());
                return current;
            }
        }

        /// <summary>
        /// 当前调用上下文
        /// </summary>
        public static InternalCallContext RequestContext
        {
            get { return Current.requestContext; }
        }

        /// <summary>
        /// 当前调用的客户信息
        /// </summary>
        public static UserProfile Customer
        {
            get { return Current.user; }
        }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class InternalCallContext
    {
        ///// <summary>
        ///// 请求服务身份
        ///// </summary>
        //[JsonProperty("s", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        //public Guid ServiceKey { get; set; }

        /// <summary>
        /// 全局请求标识（源头为用户请求）
        /// </summary>
        [JsonProperty("r", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid RequestId { get; set; }

        /// <summary>
        /// 当前请求的用户标识
        /// </summary>
        [JsonProperty("u", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public long UserId { get; set; }

        /// <summary>
        /// 线程标识
        /// </summary>
        [JsonIgnore]
        public long ThreadId { get; set; }
        /*
        /// <summary>
        /// 当前请求原始参数（来自URL或FORM）
        /// </summary>
        [JsonProperty("a", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string RequestArgument { get; set; }*/
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserProfile 
    {
        /// <summary>
        /// 用户数字标识
        /// </summary>
       public long UserId { get; set; }

        /// <summary>
        /// 用户所在设备标识
        /// </summary>
       public string DeviceId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
       public string NickName { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
       public string PhoneNumber { get; set; }
        /// <summary>
        /// 加入系统时间
        /// </summary>
       public long JoinTime { get; set; }
        /// <summary>
        /// 当前服务器时间
        /// </summary>
       public long ApiServerTime { get; set; }
    }
}
