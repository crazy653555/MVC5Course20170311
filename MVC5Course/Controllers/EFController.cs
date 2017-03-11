using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();
            db.Product.Add(new Product
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    string entityName = item.Entry.Entity.GetType().Name;

                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(entityName + "類型驗證失敗:" + err.ErrorMessage);
                    }
                }
                throw;
            }

            var data = db.Product.OrderByDescending(item => item.ProductId).ToList();

            return View(data);
        }
    }
}