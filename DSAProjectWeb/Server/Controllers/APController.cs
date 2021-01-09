using DSALib2.Classes.Charakter;
using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Entities;
using DSAProjectWeb.Server.ControllersBase;
using DSAProjectWeb.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APController : CharakterDataBaseController
    {
        public APController(ApplicationContext context, IConfiguration configuration, ILogger<APController> logger) : base(context, configuration, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] CharakterDataRequest<APView> request)
        {
            var charakter = this.GetDSASQLCharakter(request);
            charakter.AP.SetbyView(request.Data);
            return CreateResponse();
        }
      
        [Route("Get")]
        [HttpPost]
        public string Get([FromBody]CharakterIDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.CharakterID, "");
            var view = charakter.AP.GetView();
            return CreateResponse(view);
        }
    }
}
