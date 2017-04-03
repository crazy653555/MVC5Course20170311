using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        
        
        // GET: Products
        public ActionResult Index(int? ProductId,string type,bool? isActive,string keyword)
        {
            var data = repo.All(true).Take(5);
            //var repoOL = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);

            if (isActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue && p.Active.Value == isActive);
            }

            var item = new List<SelectListItem>();
            item.Add(new SelectListItem { Value = "false", Text = "無效" });
            item.Add(new SelectListItem { Value = "true", Text = "有效" });
            ViewBag.isActive = new SelectList(item,"Value","Text");
            

            ViewBag.type = type;
            if (ProductId.HasValue)
            {
                ViewBag.SelectedProductId = ProductId;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult Index(IList<Products批次更新VIewModel> data)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in data)
                {
                    var product = repo.Find(item.ProductId);
                    product.Price = item.Price;
                    product.Stock = item.Stock;
                }

                repo.UnitOfWork.Commit();
                TempData["UpdateSuccess"] = "資料更新完成";
                return RedirectToAction("Index");
            }

            return View(repo.All().Take(5));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderLines = product.OrderLine.ToList();

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();             
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection form)
        {
            Product product = repo.Find(id);
            //if (TryUpdateModel<Product>(product, new string[] { "ProductId","ProductName","Price","Active","Stock"}))           
            if (TryUpdateModel<IProduct>(product))
            {
                repo.UnitOfWork.Commit();
                TempData["ProductEditDoneMsg"] = "商品編輯成功";
                return RedirectToAction("Index");
            }
                            
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Find(id);
            product.IsDeleted = true;
            repo.UnitOfWork.Commit();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (FabricsEntities)repo.UnitOfWork.Context; //取得連線資訊
                db.Dispose(); //關閉連線
            }
            base.Dispose(disposing);
        }
    }
}
