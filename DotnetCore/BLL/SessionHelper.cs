using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotnetCore.BLL
{
    /// <summary>
    /// SessionHelper
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// Session
        /// </summary>
        private  ISession _session => _httpContextAccessor.HttpContext.Session;


        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 設置Session
        /// </summary>
        /// <param name="key">Session Key</param>
        /// <param name="T">Session 內容</param>
        public void SetSession(string key, object T)
        {
            _session.SetString(key, JsonConvert.SerializeObject(T));
        }

        /// <summary>
        /// 取得Session
        /// </summary>
        /// <param name="key">Session Key</param>
        /// <returns></returns>
        public object GetSession(string key)
        {
            return JsonConvert.DeserializeObject(_session.GetString(key));
        }

        #region --靜態--
        /// <summary>
        /// 設置Session
        /// </summary>
        /// <param name="key">Session Key</param>
        /// <param name="T">Session 內容</param>
        /// <param name="context">HttpContext</param>
        public static void SetSession(string key, object T, HttpContext context)
        {
            context.Session.SetString(key, JsonConvert.SerializeObject(T));
        }

        /// <summary>
        /// 取得Session
        /// </summary>
        /// <param name="key">Session Key</param>
        /// <param name="context">HttpContext</param>
        /// <returns></returns>
        public static object GetSession(string key, HttpContext context)
        {
            return JsonConvert.DeserializeObject(context.Session.GetString(key));
        }
        #endregion
    }
}
