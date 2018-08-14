using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Api.Common
{
    public class ErrorCode
    {
        /// <summary>
        /// 正确
        /// </summary>
        public const string Success = "0";

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string UnknowError = "-1";

        /// <summary>
        /// 参数错误
        /// </summary>
        public const string ArgumentError = "-2";

        /// <summary>
        /// 网络错误
        /// </summary>
        public const string NetworkError = "-3";

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const string InnerError = "-4";

        /// <summary>
        /// 拒绝访问
        /// </summary>
        public const string DenyAccess = "-5";

        /// <summary>
        /// 未知的RefreshToken
        /// </summary>
        public const string Auth_RefreshToken_Unknow = "40083";

        /// <summary>
        /// 未知的ServiceKey
        /// </summary>
        public const string Auth_ServiceKey_Unknow = "40082";

        /// <summary>
        /// 未知的AccessToken
        /// </summary>
        public const string Auth_AccessToken_Unknow = "40081";

        /// <summary>
        /// 未知的用户
        /// </summary>
        public const string Auth_User_Unknow = "40421";

        /// <summary>
        /// 未知的设备识别码
        /// </summary>
        public const string Auth_Device_Unknow = "40022";

        /// <summary>
        /// 令牌过期
        /// </summary>
        public const string Auth_AccessToken_TimeOut = "40036";
    }
}
