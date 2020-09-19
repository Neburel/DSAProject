using DbaWebAPI.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace DSAProjectWeb.Server.Controllers
{
    public abstract class BaseController : Controller
    {
        internal ILogger Logger { get; private set; }
        private bool responseCreated = false;
      
        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }

        #region ResponseCreator
        public string CreateResponse()
        {
            return CreateResponse<object>(new object());
        }

        public string CreateResponse<K>(K data)
        {
            return CreateResponse<K>(RestServiceResultCode.OK, "", data);
        }
        
        private string CreateResponse<K>(RestServiceResultCode code, string message, K data)
        {
            var standardResponse = new StandartResponse<K>()
            {
                Message = "",
                ResultCode = RestServiceResultCode.OK,
                Data = data
            };
            this.responseCreated = true;
            return JsonSerializer.Serialize<StandartResponse<K>>(standardResponse);
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (!responseCreated) throw new Exception("Falsche verwendung des BaseController");
            base.OnActionExecuted(filterContext);
        }
    }

    public class StandartResponse<K>
    {
        public string Message { get; set; }
        public RestServiceResultCode ResultCode { get; set; }
        public K Data { get; set; }
    }
}
