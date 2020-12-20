using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APController : BaseDataBaseController
    {
        public APController(ApplicationContext context, ILogger<APController> logger) : base(context, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] CharakterValueRequest request)
        {
            var charakter = new DSASQLCharakter(Context, request.CharakterID, "");
            charakter.AP.SetAPEarned(request.Value);
            return CreateResponse();
        }
        [Route("SetInvested")]
        [HttpPost]
        public string SetInvested([FromBody] CharakterValueRequest request)
        {
            var charakter = new DSASQLCharakter(Context, request.CharakterID, "");
            charakter.AP.SetAPInvested(request.Value);
            return CreateResponse();
        }

        [Route("Get")]
        [HttpPost]
        public string GetList([FromBody]CharakterIDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.CharakterID, "");
            var view = charakter.AP.GetView();
            return CreateResponse(view);
        }
    }
}
