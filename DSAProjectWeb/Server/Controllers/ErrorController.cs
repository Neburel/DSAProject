using DbaWebAPI.Exceptions;
using DbaWebAPI.JSON;
using DbaWebAPI.Util;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("Error")]
        public string Error()
        { 
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            var code = 500; // Internal Server Error by default

            JSONBaseResponse<JSONEmpty> returnObject;
            if (typeof(AbstractMyException).IsAssignableFrom(exception.GetType()))
            {
                var innerException = (AbstractMyException)exception;
                returnObject = createReturnObject(innerException.ErrorToSend, innerException.ResultCode);
            }
            else
            {
                returnObject = createReturnObject(exception.Message, RestServiceResultCode.Undefined);
            }

            string json = returnObject.ToJSON(out string error);
            return json;
        }

        private JSONBaseResponse<JSONEmpty> createReturnObject(string errorToSend, RestServiceResultCode resultCode)
        {
            return new JSONBaseResponse<JSONEmpty> { ResultCode = resultCode, Message = errorToSend };
        }
    }
}
