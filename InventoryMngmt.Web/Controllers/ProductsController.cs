using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryMngmt.Entities;
using InventoryMngmt.Web.Models;
using System.Diagnostics;
using InventoryMngmt.Web.ViewModels;

namespace InventoryMngmt.Web.Controllers
{
    public class ProductsController : Controller
    {
        private InventoryMngmtDbContext db = new InventoryMngmtDbContext();

        public ProductsController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("/Home/Index");
            }

            InventoryModel invModel = new InventoryModel();
            Product product = db.Products.Find(id);

            invModel.InventoryProducts = product;
            invModel.InventoryProducts.Transactions = db.Transactions.Where(t => t.ProductID == id).ToList();

            invModel.InventoryProducts = product;

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(invModel);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Serial,Type,Brand,AcquisitionDate,Location,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                string serial = product.Serial;
                

                if(!db.Products.Any(f => serial.Equals(f.Serial)))
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            }
            ViewBag.Message = "Product with same serial already exists, please choose a different serial number.";
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Serial,Type,Brand,AcquisitionDate,Location,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                /*//var productOld = db.Products.Find(product.ProductID);
                using (var productOld = (IDisposable)db.Products.Find(product.ProductID)) {
                    if (!productOld.Location.Equals(product.Location))
                    {
                        Transactions transaction = new Transactions();
                        transaction.ProductID = product.ProductID;
                        transaction.WarehouseFrom = productOld.Location;
                        transaction.WarehouseTo = product.Location;
                        transaction.Reason = "A Change was made to the Product on the Edit Screen";
                    }
                }*/

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
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
            Product product = db.Products.Find(id);
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
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
