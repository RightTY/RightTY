using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Models
{
    //public class ResponeModels
    //{

    //}

    /// <summary>
    ///測試Model
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// 測試字串
        /// </summary>
        [Required]
        public string TestString { get; set; }
    }
}
