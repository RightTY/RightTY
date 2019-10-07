using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCore.Areas.Api.BLL;
using DotnetCore.Models;
using DotnetCore.Models.DB;
using DotnetCore.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetCore.Areas.Api.Controllers
{

    /// <summary>
    /// 測試用Controller
    /// </summary>
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class TestController : Controller
    {
        /// <summary>
        /// 接收注入
        /// </summary>
        public ResultModel resultModel;
        public TestBLL testBLL;
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="resultModel">回傳格式</param>
        /// <param name="settings">Configuration設定檔</param>
        public TestController(ResultModel resultModel, TestBLL testBLL)
        {
            this.resultModel = resultModel;
            this.testBLL = testBLL;
        }

        /// <summary>
        /// GET: api/{controller}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/{controller}/5
        /// </summary>
        /// <param name="id">int</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/{controller}
        /// </summary>
        /// <remarks>
        ///  Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "testString": "string"
        ///     }
        /// </remarks>
        /// <param name="Respones">測試Model 物件</param>
        /// <returns>回傳物件</returns>
        /// <response code="200">成功回傳 測試訊息</response>
        /// <response code="400">發生錯誤</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public ResultModel Post([FromBody]TestModel Respones)
        {
            resultModel.IsSuccess = true;
            resultModel.Message = Respones.TestString;
            resultModel.Data = testBLL.GetName();
            return resultModel;
        }

        /// <summary>
        /// PUT api/{controller}/5
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="value">string</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// DELETE api/{controller}/5
        /// </summary>
        /// <param name="id">int</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
