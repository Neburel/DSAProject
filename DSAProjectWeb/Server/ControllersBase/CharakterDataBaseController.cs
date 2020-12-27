using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using DSAProject2Web.Server.Controllers;
using DSAProject2Web.Server.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DSAProjectWeb.Server.ControllersBase
{
    public class CharakterDataBaseController : BaseDataBaseController

    {   
        public string JsonTalentFile { get; private set; }
        public CharakterDataBaseController(ApplicationContext context, IConfiguration configuration, ILogger<TalentController> logger) : base(context, logger)
        {
            JsonTalentFile = configuration.GetSection(DSAProject2Web.Startup.TALENTJSONFILE).Value;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    

        internal DSASQLCharakter GetDSASQLCharakter(CharakterIDRequest request)
        {
            if (request.CharakterID <= 0) throw new ArgumentNullException("Die Charakter ID ist ungültig");
            return new DSASQLCharakter(Context, request.CharakterID, this.JsonTalentFile);
        }
        
    }
}
