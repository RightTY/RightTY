using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.System
{
    /// <summary>
    /// Configuration設定檔
    /// </summary>
    public class SystemSettingModel
    {
        /// <summary>
        /// DB設定檔Model
        /// </summary>
        public DB DB { get; set; }
        /// <summary>
        /// Line設定檔
        /// </summary>Model
        public Line Line { get; set; }
        /// <summary>
        /// Facebook設定檔
        /// </summary>Model
        public Facebook Facebook { get; set; }
    }

    #region --DB--
    /// <summary>
    /// DB設定檔Model
    /// </summary>
    public class DB
    {
        /// <summary>
        /// 連接字串Model
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }
    }
    /// <summary>
    /// 連接字串
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 預設ConnectionString
        /// </summary>
        public string DefaultConnection { get; set; }
    }
    #endregion

    #region --Line--
    /// <summary>
    /// Line設定檔
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Line登入
        /// </summary>
        public LineLoginProject LineLogin { get; set; }
    }
    /// <summary>
    /// Line Login專案
    /// </summary>
    public class LineLoginProject
    {
        /// <summary>
        /// Right_TY 專案名
        /// </summary>
        public LineLoginProjectSetting RightTY { get; set; }
    }
    /// <summary>
    /// Line Login專案 設定檔
    /// </summary>
    public class LineLoginProjectSetting
    {

        /// <summary>
        /// 頻道ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Authorize
        /// </summary>
        public Authorize Authorize { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Token
        /// </summary>
        public Token Token { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Profile
        /// </summary>
        public Profile Profile { get; set; }
        /// <summary>
        /// 頻道金鑰
        /// </summary>
        public string ClientSecret { get; set; }

    }
    /// <summary>
    /// LineLoginProjectSetting-Authorize
    /// </summary>
    public class Authorize
    {
        /// <summary>
        /// authorize URL
        /// </summary>
        public Uri AuthorizeUri { get; set; }
        /// <summary>
        /// 請求項目
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 回調URL
        /// </summary>
        public Uri RedirectUri { get; set; }
        /// <summary>
        /// 這告訴LINE平台返回授權碼
        /// </summary>
        public string ResponseType { get; set; }
        /// <summary>
        /// 用於防止跨站點請求偽造的唯一字母數字字符串。
        /// <para></para>
        /// 該值應由您的應用程序隨機生成。不能是URL編碼的字符串。
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 用於防止重放攻擊的字符串。此值以ID令牌返回。(非必要)
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// 即使用戶已授予所有請求的權限，也用於強制顯示同意屏幕。(非必要)
        /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// 自上次驗證用戶以來的允許經過時間（以秒為單位）。(非必要)
        /// <para></para>
        /// 對應於OpenID Connect Core 1.0max_age的“身份驗證請求”部分中定義的參數。
        /// </summary>
        public int Max_age { get; set; }
        /// <summary>
        /// LINE登錄屏幕的顯示語言。按首選項順序指定為一個或多個RFC 5646（BCP 47）語言標籤，
        /// <para></para>
        /// 以空格分隔。對應於OpenID Connect Core 1.0ui_locales的“身份驗證請求”部分中定義的參數。(非必要)
        /// </summary>
        public string UiLocales { get; set; }
        /// <summary>
        /// 顯示在登錄期間將LINE官方帳戶添加為朋友的選項。將值設置為normal或aggressive。(非必要)
        /// <para></para>
        /// 有關更多信息，請參閱將LINE官方帳戶與LINE登錄頻道相關聯。
        /// </summary>
        public string BotPrompt { get; set; }
    }
    /// <summary>
    /// LineLoginProjectSetting-Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 回調URL
        /// </summary>
        public Uri RedirectUri { get; set; }
        /// <summary>
        /// authorize URL
        /// </summary>
        public Uri TokenUri { get; set; }
        /// <summary>
        /// 指定授予類型。
        /// </summary>
        public string GrantType { get; set; }
    }
    /// <summary>
    ///  LineLoginProjectSetting-Profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Profile URL
        /// </summary>
        public Uri ProfileUri { get; set; }
    }
    #endregion

    /// <summary>
    /// Facebook
    /// </summary>
    public class Facebook
    {
        /// <summary>
        /// FacebookLogin
        /// </summary>
        public FacebookLogin FacebookLogin { get; set; }
    }

    /// <summary>
    /// FacebookLogin
    /// </summary>
    public class FacebookLogin
    {
        /// <summary>
        /// FacebookLoginProjectSetting
        /// </summary>
        public FacebookLoginProjectSetting RightTY { get; set; }
    }
    /// <summary>
    /// FacebookLoginProject
    /// </summary>
    public class FacebookLoginProjectSetting
    {
        /// <summary>
        /// 應用程式編號
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 應用程式密鑰
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Oauth
        /// </summary>
        public Oauth Oauth { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public FacebookToken Token { get; set; }
    }
    /// <summary>
    /// Oauth
    /// </summary>
    public class Oauth
    {
        /// <summary>
        /// OauthUri
        /// </summary>
        public Uri OauthUri { get; set; }
        /// <summary>
        /// 回調網址
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
    }
    /// <summary>
    /// FacebookToken
    /// </summary>
    public class FacebookToken
    {
        /// <summary>
        /// TokenUri
        /// </summary>
        public Uri TokenUri { get; set; }
        /// <summary>
        /// 回調網址
        /// </summary>
        public Uri RedirectUri { get; set; }
    }
}
