using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Models.Facebook
{
    /// <summary>
    /// FacebookOauthModel
    /// </summary>
    public class FacebookOauthModel
    {
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Code { get; set; }
    }
    /// <summary>
    /// FacebookTokenModel
    /// </summary>
    public class FacebookTokenModel
    {
        /// <summary>
        /// Access_Token
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// Token_Type
        /// </summary>
        public string Token_Type { get; set; }
        /// <summary>
        /// Expires_In
        /// </summary>
        public string Expires_In { get; set; }
        /// <summary>
        /// ERROR
        /// </summary>
        public Error Error { get; set; }
    }
    /// <summary>
    /// ERROR
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 錯誤類型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// Fbtrace_Id
        /// </summary>
        public string Fbtrace_Id { get; set; }
    }
}
