using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSALib2.SQLDataBase;
using DSAProjectWeb.Server.Entities;
using DSALib2.Classes.Charakter;
using DSALib2.Utils;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributController : BaseDataBaseController
    {
        public AttributController(ApplicationContext context, ILogger<AttributController> logger) : base(context, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] IDValueRequest request)
        {
            var charakter = new DSASQLCharakter(Context, request.CharakterID);
            charakter.Attribute.SetAKT((CharakterAttribut)request.ID, request.Value);
            return CreateResponse();
        }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]IDRequest charakterID)
        {
            var charakter           = new DSASQLCharakter(Context, charakterID.ID);
            var attributViewList    = charakter.Attribute.GetViewList();
            return CreateResponse(attributViewList);
        }
    }

}
