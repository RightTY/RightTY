using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetCore.Models;
using Microsoft.AspNetCore.Http;
using DotnetCore.Models.Line;
using Newtonsoft.Json;
using DotnetCore.BLL;
using DotnetCore.BLL.Login;
using Microsoft.Extensions.Options;
using DotnetCore.System;
using System.Net.Http;
using DotnetCore.Models.Facebook;

namespace DotnetCore.Controllers
{
#pragma warning disable 1591
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ResultModel _resultModel;
        private readonly SessionHelper _sessionHelper;
        private readonly IOptions<SystemSettingModel> _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(
            ILogger<HomeController> logger,
            ResultModel resultModel,
            SessionHelper sessionHelper,
            IOptions<SystemSettingModel> settings,
            IHttpClientFactory httpClientFactory
            )
        {
            _logger = logger;
            _resultModel = resultModel;
            _sessionHelper = sessionHelper;
            _settings = settings;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
#pragma warning restore 1591
        /// <summary>
        /// 歡迎頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult Wellcome()
        {
            ViewBag.Nickname = _sessionHelper.GetSession("Nickname");
            return View();
        }

        /// <summary>
        /// Line Login Authorize回調
        /// </summary>
        /// <param name="lineAuthorize">line Authorize Response</param>
        [HttpGet]
        public void LineLogin([FromQuery]LineAuthorizeModel lineAuthorize)
        {
            string Uri = string.Empty;
            try
            {
                #region --登入發生錯誤--
                if (lineAuthorize.Error != null)
                {
                    //TODO:錯誤處理
                    Uri = "/Home/Error";
                }
                #endregion
                else
                {
                    LineLoginBLL lineLoginBLL = new LineLoginBLL(_settings,_resultModel,_httpClientFactory);
                    LineProfile Data = lineLoginBLL.GetLineAccount(lineAuthorize);
                    _sessionHelper.SetSession("Nickname", Data.displayName);
                    Uri = "/Home/Wellcome";
                }
            }
            catch (Exception ex)
            {

                Uri = "/Home/Error";
            }
            finally
            {
                Response.Redirect(Uri);
            }
        }

        /// <summary>
        /// Facebook Login Oauth
        /// </summary>
        /// <param name="lineAuthorize">line Authorize Response</param>
        [HttpGet]
        public void FacebookLogin([FromQuery]FacebookOauthModel facebookOauth)
        {
            string Uri = string.Empty;
            try
            {
                #region --登入發生錯誤--
                if (false)
                {
                    //TODO:錯誤處理
                    Uri = "/Home/Error";
                }
                #endregion
                else
                {
                    FacebookLoginBLL facebookLoginBLL = new FacebookLoginBLL(_settings, _resultModel, _httpClientFactory);
                    facebookLoginBLL.GetFacebookAccountToken(facebookOauth);
                    //LineProfile Data = lineLoginBLL.GetLineAccount(lineAuthorize);
                    //_sessionHelper.SetSession("Nickname", Data.displayName);
                    Uri = "/Home/Wellcome";
                }
            }
            catch (Exception ex)
            {

                Uri = "/Home/Error";
            }
            finally
            {
                Response.Redirect(Uri);
            }
        }
    }
}