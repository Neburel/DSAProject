using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace DSAProjectWeb.Server.ControllersBase
{
    public class CharakterDataBaseController : BaseDataBaseController

    {
        public string JsonTalentFile { get; private set; }
        public CharakterDataBaseController(ApplicationContext context, IConfiguration configuration, ILogger<TalentController> logger) : base(context, logger)
        {
            JsonTalentFile = configuration.GetSection(DSAProject2Web.Startup.TALENTJSONFILE).Value;
        }
    }
}
