using DSALib2.Classes.Charakter.View;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Entities;
using DSAProjectWeb.Server.ControllersBase;
using DSAProjectWeb.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyController : CharakterDataBaseController
    {
        public MoneyController(ApplicationContext context, IConfiguration configuration, ILogger<DescriptionController> logger) : base(context, configuration, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] CharakterDataRequest<MoneyView> request)
        {
            var charakter = this.GetDSASQLCharakter(request);
            charakter.Money.SetByView(request.Data);
            return CreateResponse();
        }

        [Route("Get")]
        [HttpPost]
        public string Get([FromBody] CharakterIDRequest request)
        {
            var charakter = this.GetDSASQLCharakter(request);
            var view = charakter.Money.GetView();
            return CreateResponse(view);
        }
    }
}
