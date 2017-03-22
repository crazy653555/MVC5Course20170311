using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        // GET: EF
        FabricsEntities db = new FabricsEntities();
        public ActionResult Index(bool? IsActive, string keyword)
        {

            //var product = new Product
            //{
            //    ProductName = "BMW",
            //    Price = 2,
            //    Stock = 1,
            //    Active = true
            //};

            //db.Product.Add(product);
            //var pkey = product.ProductId;

            //var data = db.Product.OrderByDescending(item => item.ProductId);

            //foreach (var item in data)
            //{
            //    item.Price = item.Price + 1;
            //}

            //SaveChanges();

            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();

            if (IsActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

                        
            return View(data);
        }

        private void SaveChanges()
        {
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
        }

        public ActionResult Details(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            db.OrderLine.RemoveRange(product.OrderLine);
            db.Product.Remove(product);
            SaveChanges();
            //db.Product.Remove(product);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult QueryPlan(int num = 10)
        {
            var data = db.Product.Include(t => t.OrderLine).OrderBy(p => p.ProductId).AsQueryable();
            return View(data);
        }
    }
}