﻿using System;
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
        FabricsEntities db = new FabricsEntities();
        public ActionResult Index()
        {

            var product = new Product
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true
            };

            //db.Product.Add(product);
            
            try
            {
                //db.SaveChanges();
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

            var pkey = product.ProductId;
            
            var data = db.Product.OrderByDescending(item => item.ProductId).Take(5);

            foreach (var item in data)
            {
                item.Price = item.Price + 1;
            }

            db.SaveChanges();
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}