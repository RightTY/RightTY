using DotnetCore.Models;
using DotnetCore.Models.Line;
using DotnetCore.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace DotnetCore.BLL.Login
{
    /// <summary>
    /// Line登入BLL
    /// </summary>
    public class LineLoginBLL
    {
        /// <summary>
        /// sqlConStr
        /// </summary>
        private readonly Line _line;
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
        public LineLoginBLL(IOptions<SystemSettingModel> settings, ResultModel resultModel, IHttpClientFactory httpClientFactory)
        {
            _line = settings.Value.Line;
            _resultModel = resultModel;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Line登入
        /// </summary>
        /// <returns></returns>
        public ResultModel LineLogin()
        {
            LineLoginProjectSetting lineSetting;
            //如果以後有多個Line登入 所需判斷
            if (true)
            {
                lineSetting = _line.LineLogin.RightTY;
                lineSetting.Authorize.State = "123";
            }
            _resultModel.Data =
                lineSetting.Authorize.AuthorizeUri + "?" +
                "response_type=" + lineSetting.Authorize.ResponseType + "&" +
                "client_id=" + lineSetting.ClientId + "&" +
                "redirect_uri=" + lineSetting.Authorize.RedirectUri + "&" +
                "scope=" + lineSetting.Authorize.Scope + "&" +
                "state=" + lineSetting.Authorize.State;
            _resultModel.IsSuccess = true;
            _resultModel.Message = "Success";
            return _resultModel;
        }

        /// <summary>
        /// 取得Line會員資料
        /// </summary>
        /// <param name="lineAuthorize"></param>
        /// <returns></returns>
        public LineProfile GetLineAccount(LineAuthorizeModel lineAuthorize)
        {
            LineLoginProjectSetting lineSetting;
            if (true)
            {
                lineSetting = _line.LineLogin.RightTY;
            }
            LineTokenModel lineTokenModel = GetLineAccountToken(lineAuthorize, lineSetting);
            HttpClientHelper<LineProfile> httpClientHelper = new HttpClientHelper<LineProfile>(_httpClientFactory);
            httpClientHelper.SetHeaders("Authorization", "Bearer " + lineTokenModel.Access_Token);
            return httpClientHelper.SendAsync(HttpMethod.Get, lineSetting.Profile.ProfileUri.ToString());
        }

        /// <summary>
        /// 取得Line會員Access_Token
        /// </summary>
        /// <param name="lineAuthorize">line Authorize GetCode Model</param>
        /// <returns></returns>
        public LineTokenModel GetLineAccountToken(LineAuthorizeModel lineAuthorize, LineLoginProjectSetting lineSetting)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"grant_type",lineSetting.Token.GrantType },
                {"code",lineAuthorize.Code },
                {"redirect_uri",lineSetting.Token.RedirectUri.ToString()},
                {"client_id",lineSetting.ClientId},
                {"client_secret",lineSetting.ClientSecret}
            };
            HttpClientHelper<LineTokenModel> httpClientHelper = new HttpClientHelper<LineTokenModel>(_httpClientFactory);
            LineTokenModel lineTokenModel = httpClientHelper.SendAsync(HttpMethod.Post, lineSetting.Token.TokenUri.ToString(),dic);
            return lineTokenModel;
        }
    }
}
