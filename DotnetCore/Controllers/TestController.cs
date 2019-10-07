using DotnetCore.BLL;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Controllers
{
    //使用區域Router
    // [MiddlewareFilter(typeof(RegionMiddlewareTemplate))]
    /// <summary>
    /// 測試用
    /// </summary>
    public class TestController : Controller
    {
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;
        private readonly OperationService _operationService;

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="transientOperation"></param>
        /// <param name="scopedOperation"></param>
        /// <param name="singletonOperation"></param>
        /// <param name="singletonInstanceOperation"></param>
        /// <param name="operationService"></param>
        public TestController(IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation, IOperationSingletonInstance singletonInstanceOperation, OperationService operationService)
        {
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;
            _operationService = operationService;
        }

        // GET: /<controller>/
        /// <summary>
        /// TEST
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Transient = _transientOperation;
            ViewBag.Scoped = _scopedOperation;
            ViewBag.Singleton = _singletonOperation;
            ViewBag.SingletonInstance = _singletonInstanceOperation;
            ViewBag.Service = _operationService;

            return View();
        }
    }
}