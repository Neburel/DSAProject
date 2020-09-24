using DSALib2.Classes.Charakter;
using DSALib2.Classes.Charakter.Repository.SQL;
using DSALib2.SQLDataBase;
using DSAProjectWeb.Server.Entities;
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
            var repo        = new SQLCharakterRepository(Context);
            var charakter   = repo.CreateDSACharakter(Context, "New Charakter");
            return CreateResponse(charakter);
        }

        [Route("GetList")]
        [HttpPost]
        public string GetList()
        {
            var repo = new SQLCharakterRepository(Context);
            return CreateResponse(repo.GetList());
        }

        [Route("Set")]
        [HttpPost]
        public string Set([FromBody]CharakterIDRequest request)
        {
            var repo = new SQLCharakterRepository(Context);
            repo.SetCharakter(request.CharakterID);
            return CreateResponse(repo.GetList());
        }
    }
}
