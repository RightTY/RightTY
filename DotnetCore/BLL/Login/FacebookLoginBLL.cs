using DotnetCore.Models;
using DotnetCore.Models.Facebook;
using DotnetCore.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotnetCore.BLL.Login
{
    /// <summary>
    /// FacebookLoginBLL
    /// </summary>
    public class FacebookLoginBLL
    {
        /// <summary>
        /// sqlConStr
        /// </summary>
        private readonly Facebook _facebook;
        /// <summary>
        /// 回傳訊息
        /// </summary>
        private readonly ResultModel _resultModel;
        /// <summary>
        /// 
        /// </summary>
        public readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="settings">Configuration設定檔</param>
        /// <param name="resultModel">回傳訊息</param>
        /// <param name="httpClientFactory"></param>
        public FacebookLoginBLL(IOptions<SystemSettingModel> settings, ResultModel resultModel, IHttpClientFactory httpClientFactory)
        {
            _facebook = settings.Value.Facebook;
            _resultModel = resultModel;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Facebook登入
        /// </summary>
        /// <returns></returns>
        public ResultModel FacebookLogin()
        {
            FacebookLoginProjectSetting FacebookSetting;
            //如果以後有多個Line登入 所需判斷
            if (true)
            {
                FacebookSetting = _facebook.FacebookLogin.RightTY;
                FacebookSetting.Oauth.State = "123";
            }
            _resultModel.Data =
                FacebookSetting.Oauth.OauthUri + "?" +
                "client_id=" + FacebookSetting.ClientId + "&" +
                "redirect_uri=" + FacebookSetting.Oauth.RedirectUri + "&" +
                "state=" + FacebookSetting.Oauth.State;
            _resultModel.IsSuccess = true;
            _resultModel.Message = "Success";
            return _resultModel;
        }

        ///// <summary>
        ///// 取得Line會員資料
        ///// </summary>
        ///// <param name="lineAuthorize"></param>
        ///// <returns></returns>
        //public LineProfile GetLineAccount(LineAuthorizeModel lineAuthorize)
        //{
        //    LineLoginProjectSetting lineSetting;
        //    if (true)
        //    {
        //        lineSetting = _line.LineLogin.RightTY;
        //    }
        //    LineTokenModel lineTokenModel = GetLineAccountToken(lineAuthorize, lineSetting);
        //    HttpClientHelper<LineProfile> httpClientHelper = new HttpClientHelper<LineProfile>(_httpClientFactory);
        //    httpClientHelper.SetHeaders("Authorization", "Bearer " + lineTokenModel.Access_Token);
        //    return httpClientHelper.SendAsync(HttpMethod.Get, lineSetting.Profile.ProfileUri.ToString());
        //}

        /// <summary>
        /// 取得Line會員Access_Token
        /// </summary>
        /// <param name="facebookOauth">FacebookOauthModel</param>
        /// /// <param name="facebookSetting">FacebookLoginProject</param>
        /// <returns></returns>
        public FacebookTokenModel GetFacebookAccountToken(FacebookOauthModel facebookOauth)
        {
            FacebookLoginProjectSetting facebookSetting = _facebook.FacebookLogin.RightTY;
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"client_id",facebookSetting.ClientId},
                {"redirect_uri",facebookSetting.Token.RedirectUri.ToString()},
                {"client_secret",facebookSetting.ClientSecret},
                {"code",facebookOauth.Code }
            };
            HttpClientHelper<FacebookTokenModel> httpClientHelper = new HttpClientHelper<FacebookTokenModel>(_httpClientFactory);
            FacebookTokenModel facebookTokenModel = httpClientHelper.SendAsync(HttpMethod.Get, facebookSetting.Token.TokenUri.ToString(), dic);
            return facebookTokenModel;
        }
    }
}
