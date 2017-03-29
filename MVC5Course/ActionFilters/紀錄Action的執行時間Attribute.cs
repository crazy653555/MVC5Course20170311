using System;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
    public class 紀錄Action的執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your aplication description page.";
            base.OnActionExecuting(filterContext);
        }
    }
}