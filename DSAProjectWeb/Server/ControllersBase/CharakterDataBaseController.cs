using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Controllers;
using DSAProject2Web.Server.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace DSAProjectWeb.Server.ControllersBase
{
    public class CharakterDataBaseController : BaseDataBaseController

    {   
        public string JsonTalentFile { get; private set; }
        public CharakterDataBaseController(ApplicationContext context, IConfiguration configuration, ILogger<CharakterDataBaseController> logger) : base(context, logger)
        {
            JsonTalentFile = configuration.GetSection(DSAProject2Web.Startup.TALENTJSONFILE).Value;
        }

        internal DSASQLCharakter GetDSASQLCharakter(CharakterIDRequest request)
        {
            return GetDSASQLCharakter(request.CharakterID);
        }
        internal DSASQLCharakter GetDSASQLCharakter(int charakterID)
        {
            if (charakterID <= 0) throw new ArgumentNullException("Die Charakter ID ist ungültig");
            return new DSASQLCharakter(Context, charakterID, this.JsonTalentFile);
        }

    }
}
