
namespace BlueCat.Api.Common
{
    /// <summary>
    ///   表示用户登录方面的异常
    /// </summary>
    public class AccountException : BlueCatException
    {
        /// <summary>
        ///   构造
        /// </summary>
        /// <param name="msg"> </param>
        public AccountException(string msg)
            : base(msg)
        {
        }
    }
}
