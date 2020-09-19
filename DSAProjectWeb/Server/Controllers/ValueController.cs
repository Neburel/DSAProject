using DSALib2.Classes.Charakter;
using DSALib2.SQLDataBase;
using DSAProjectWeb.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSAProjectWeb.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : BaseDataBaseController
    {
        public ValueController(ApplicationContext context, ILogger<ValueController> logger) : base(context, logger) { }

        [Route("GetList")]
        [HttpPost]
        public string GetList([FromBody]IDRequest charakterID)
        {
            var charakter = new DSASQLCharakter(Context, charakterID.ID);
            var attributViewList = charakter.Values.GetViewList();
            return CreateResponse(attributViewList);
        }
    }
}
