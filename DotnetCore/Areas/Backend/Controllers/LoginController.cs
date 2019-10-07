using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Areas.Backend.Controllers
{
    /// <summary>
    /// 後台Controller
    /// </summary>
    [Area("Backend")]
    public class LoginController : Controller
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}