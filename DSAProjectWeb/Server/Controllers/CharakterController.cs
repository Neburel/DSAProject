using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharakterController : BaseDataBaseController
    {

        public CharakterController(ApplicationContext context, ILogger<CharakterController> logger) : base(context, logger) { }

        [Route("Create")]
        [HttpPost]
        public string HttpRequest()
        {
            var charakter   = DSASQLCharakter.CreateDSACharakter(Context, "New Charakter");
            return CreateResponse(charakter);
        }
    }
}
