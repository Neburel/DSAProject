using DSALib2.Classes.Charakter.Repository.SQL;
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
    public class DescriptionController : CharakterDataBaseController
    {
        public DescriptionController(ApplicationContext context, IConfiguration configuration, ILogger<DescriptionController> logger) : base(context, configuration, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] CharakterDataRequest<DescriptionView> request)
        {
            var charakter = this.GetDSASQLCharakter(request);
            charakter.Description.SetByView(request.Data);
            
            return CreateResponse();
        }

        [Route("Get")]
        [HttpPost]
        public string GetList([FromBody] CharakterIDRequest request)
        {
            var charakter = this.GetDSASQLCharakter(request);
            var view = charakter.Description.GetView();
            return CreateResponse(view);
        }
    }
}
