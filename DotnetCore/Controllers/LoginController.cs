using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotnetCore.BLL;
using DotnetCore.BLL.Login;
using DotnetCore.Models;
using DotnetCore.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotnetCore.Controllers
{
    /// <summary>
    /// 登入Controller
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Configuration設定檔
        /// </summary>
        private readonly IOptions<SystemSettingModel> settings;
        /// <summary>
        /// 回傳訊息
        /// </summary>
        private readonly ResultModel resultModel;
        public readonly IHttpClientFactory _httpClientFactory;
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="settings">Configuration設定檔</param>
        /// /// <param name="resultModel">回傳訊息</param>
        public LoginController(IOptions<SystemSettingModel> settings, ResultModel resultModel, IHttpClientFactory httpClientFactory)
        {
            if (settings != null)
            {
                this.settings = settings;
                this.resultModel = resultModel;
                _httpClientFactory = httpClientFactory;
            }
        }


        /// <summary>
        /// 登入首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Line登入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel LineLogin()
        {
            LineLoginBLL lineLoginBLL = new LineLoginBLL(settings,resultModel,_httpClientFactory);
            return lineLoginBLL.LineLogin();
        }

        /// <summary>
        /// Facebook
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel FacebookLogin()
        {
            FacebookLoginBLL facebookLoginBLL = new FacebookLoginBLL(settings, resultModel, _httpClientFactory);
            return facebookLoginBLL.FacebookLogin();
        }
    }
}