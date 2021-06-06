using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using DSALib2.SQLDataBase;
using DSAProjectWeb.Server.ControllersBase;
using DSAProject2Web.Server.Entities;
using DSAProjectWeb.Server.Entities;
using DSALib2.Classes.Charakter.View;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraitController : CharakterDataBaseController
    {
        public TraitController(ApplicationContext context, IConfiguration configuration, ILogger<TraitController> logger) : base(context, configuration, logger) { }

        [Route("New")]
        [HttpPost]
        public string GetNew([FromBody] CharakterIDRequest request)
        {
            var charakter = GetDSASQLCharakter(request);
            return CreateResponse(charakter.Traits.GetEmptyView());
        }

        [Route("GetList")]
        [HttpPost]
        public string GetTraitList([FromBody] CharakterIDRequest request) 
        {
            var charakter = GetDSASQLCharakter(request);
            return CreateResponse(charakter.Traits.GetViewList());
        }

        [Route("Set")]
        [HttpPost]
        public string SetTrait([FromBody] CharakterDataRequest<TraitView> request)
        {
            var charakter = GetDSASQLCharakter(request);
            charakter.Traits.SetByView(request.Data);
            return CreateResponse(charakter.Traits.GetViewList());
        }

        [Route("Delete")]
        [HttpPost]
        public string DeleteTrait([FromBody] CharakterDataRequest<TraitView> request)
        {
            var charakter = GetDSASQLCharakter(request);
            charakter.Traits.DeleteByView(request.Data);
            return CreateResponse();
        }
    }
}
