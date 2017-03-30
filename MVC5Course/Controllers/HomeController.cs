using MVC5Course.ActionFilters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [紀錄Action的執行時間]
    public class HomeController : BaseController
    {
        [共用的ViewBag資料共享於部分HomeController動作方法]
        public ActionResult Index()
        {
            return View();
        }

        [共用的ViewBag資料共享於部分HomeController動作方法]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Test()
        {
            return View();
        }

        [HandleError(ExceptionType = typeof(ArgumentException),View = "ErrorArgument")]
        [HandleError(ExceptionType = typeof(SqlException),View ="ErrorSql")]
        public ActionResult ErrorTest(string e)
        {
            if (e == "1")
            {
                throw new Exception("Error 1");
            }

            if (e == "2")
            {
                throw new ArgumentException("Error 2");
            }
            return Content("no Error");
        }
    }
}