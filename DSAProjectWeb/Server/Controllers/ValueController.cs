using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DSAProject2Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : BaseDataBaseController
    {
        public ValueController(ApplicationContext context, ILogger<ValueController> logger) : base(context, logger) { }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]CharakterIDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.CharakterID, "");
            var attributViewList = charakter.Values.GetViewList();
            return CreateResponse(attributViewList);
        }
    }
}
