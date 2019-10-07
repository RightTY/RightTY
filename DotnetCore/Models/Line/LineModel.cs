using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Models.Line
{
    /// <summary>
    /// line Authorize Response
    /// </summary>
    public class LineAuthorizeModel
    {
        /// <summary>
        /// line Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// line 驗證碼
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Human-readable ASCII encoded text description of the error.
        /// <para></para>
        /// 可讀的ASCII編碼文本描述錯誤。
        /// </summary>
        public string Error_description { get; set; }

    }

    /// <summary>
    /// Line Token  Response
    /// </summary>
    public class LineTokenModel
    {
        /// <summary>
        /// 訪問令牌。有效期為30天。
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// 直到訪問令牌過期的時間（以秒為單位）。
        /// </summary>
        public int Expires_In { get; set; }
        /// <summary>
        /// 包含有關用戶信息的JSON Web令牌（JWT）。僅openid在範圍中指定時，才返回此字段。有關更多信息，請參閱ID令牌。
        /// </summary>
        public string Id_Token { get; set; }
        /// <summary>
        /// 用於獲取新訪問令牌的令牌。有效期至訪問令牌過期後的10天。
        /// </summary>
        public string Refresh_Token { get; set; }
        /// <summary>
        /// 用戶授予的權限。但是，即使授予了權限，email作用域也不會作為scope屬性的值返回。
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// Bearer
        /// </summary>
        public string Token_Type { get; set; }
    }
    /// <summary>
    /// Line 個人檔案
    /// </summary>
    public class LineProfile
    {
        /// <summary>
        /// userId
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// Line的顯示名稱
        /// </summary>
        public string displayName { get; set; }
        /// <summary>
        /// 大頭照網址
        /// </summary>
        public Uri pictureUrl { get; set; }
        /// <summary>
        ///statusMessage
        /// </summary>
        public string statusMessage { get; set; }
    }
}
