using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Entities;
using DSALib2.Classes.Charakter;
using DSALib2.Utils;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributController : BaseDataBaseController
    {
        public AttributController(ApplicationContext context, ILogger<AttributController> logger) : base(context, logger) { }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody] IDAttributRequest request)
        {
            var charakter = new DSASQLCharakter(Context, request.CharakterID, "");
            charakter.Attribute.SetAKT((CharakterAttribut)request.AttributID, request.Value);
            return CreateResponse();
        }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]CharakterIDRequest charakterID)
        {
            var charakter           = new DSASQLCharakter(Context, charakterID.CharakterID, "");
            var attributViewList    = charakter.Attribute.GetViewList();
            return CreateResponse(attributViewList);
        }
    }

}
