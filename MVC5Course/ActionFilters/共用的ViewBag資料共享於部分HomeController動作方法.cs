using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
    public class 共用的ViewBag資料共享於部分HomeController動作方法:ActionFilterAttribute
    {
        public DateTime date_1;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //紀錄開始時間
            date_1 = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //紀錄結束時間
            var date_2 = DateTime.Now;         
            //計算執行時間
            TimeSpan executeTime = date_2 - date_1;

            filterContext.Controller.ViewBag.執行時間 = executeTime;

            base.OnActionExecuted(filterContext);
        }
    }
}