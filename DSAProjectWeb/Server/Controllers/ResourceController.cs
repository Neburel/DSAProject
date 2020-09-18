using DSALib2.SQLDataBase;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSAProjectWeb.Server.Entities;
using DSALib2.Classes.Charakter;

namespace DSAProjectWeb.Server.Controllers
{
    public class ResourceController : BaseDataBaseController
    {
        public ResourceController(ApplicationContext context, ILogger<ResourceController> logger) : base(context, logger) { }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]IDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.ID);
            var attributViewList = charakter.Resources.GetViewList();
            return CreateResponse(attributViewList);
        }
    }
}
