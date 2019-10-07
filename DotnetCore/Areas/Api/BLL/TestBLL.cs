using DotnetCore.Models.DB;
using DotnetCore.System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DotnetCore.Areas.Api.BLL
{

    /// <summary>
    /// 測試BLL
    /// </summary>
    public class TestBLL
    {
        /// <summary>
        /// sqlConStr
        /// </summary>
        private readonly string sqlConStr;
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="settings">Configuration設定檔</param>
        public TestBLL(IOptions<SystemSettingModel> settings)
        {
            if (settings != null)
            {
                sqlConStr = settings.Value.DB.ConnectionStrings.DefaultConnection;
            }

        }
        /// <summary>
        /// 取得姓名
        /// </summary>
        /// <returns></returns>
        public TestDBModel GetName()
        {
            TestDBModel testDBModel = new TestDBModel();
            using (SqlConnection cn = new SqlConnection(sqlConStr))
            {
                string sql = "select Top(1) name from Test";
                testDBModel = cn.Query<TestDBModel>(sql).Single();
            }
            return testDBModel;
        }
    }
}