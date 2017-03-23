using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView("Index");
        }

        public ActionResult ContentTest()
        {
            return Content("<script>alert('Redriecting ...');</script>", "application/javascript", Encoding.UTF8);
        }

        public ActionResult FileTest()
        {
            return File(Server.MapPath("~/Content/alphago-logo.png"),"image/png");
        }

        public ActionResult JsonTest()
        {
            var db = new FabricsEntities();
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Product.Take(3);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}