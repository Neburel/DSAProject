using DSALib2.SQLDataBase;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSAProject2Web.Server.Entities;
using DSALib2.Classes.Charakter;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : BaseDataBaseController
    {
        public ResourceController(ApplicationContext context, ILogger<ResourceController> logger) : base(context, logger) { }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]CharakterIDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.CharakterID);
            var viewList = charakter.Resources.GetViewList();
            return CreateResponse(viewList);
        }
    }
}
