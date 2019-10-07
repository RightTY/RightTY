using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Models
{
    /// <summary>
    /// API回傳Model
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 回傳資料
        /// </summary>
        public object Data { get; set; }

    }
}
