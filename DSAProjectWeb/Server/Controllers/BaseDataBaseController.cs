using DSALib2.SQLDataBase;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DSAProjectWeb.Server.Controllers
{
    public abstract class BaseDataBaseController : BaseController
    {
        public ApplicationContext Context { get; set; }
        public BaseDataBaseController(ApplicationContext context, ILogger<BaseDataBaseController> logger) : base(logger) {
            this.Context = context;
            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Context.SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}
