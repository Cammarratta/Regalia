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
using System.Dynamic;
using InventoryMngmt.Web.ViewModels;

namespace InventoryMngmt.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private InventoryMngmtDbContext db = new InventoryMngmtDbContext();

        public TransactionsController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        // GET: Transactions
        public ActionResult Index(Warehouse? Location, InventoryType? Type)
        {
            ViewBag.Location = (from r in db.Transactions
                                 select r.WarehouseTo).Distinct();

            ViewBag.Type = (from r in db.Transactions
                                     select r.Product.Type).Distinct();

            var model = db.Transactions.AsEnumerable().Select(t => t)
                .Where(t => t.WarehouseTo == Location || Location.ToString() == String.Empty || Location == null)                
                .Distinct()
                .ToList();
            
            return View(model);
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }

            InventoryModel invModel = new InventoryModel();
            Transactions transactions = db.Transactions.Find(id);
            Product product = db.Products.Find(transactions.ProductID);
            invModel.InventoryProducts = product;
            invModel.InventoryTransactions = transactions;

            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(invModel);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {

                InventoryModel invModel = new InventoryModel();
                var prod = db.Products.Find(id);
                invModel.InventoryProducts = prod;          

                return View(invModel);
            }
            else {
                return RedirectToAction("Index");
            }
        }

        
        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryModel invModel)
        {
            if (ModelState.IsValid)
            {
                Transactions transaction = invModel.InventoryTransactions;
                Product product = db.Products.Find(invModel.InventoryProducts.ProductID);
                product.Location = transaction.WarehouseTo;
                transaction.ProductID = product.ProductID;
                transaction.WarehouseFrom = invModel.InventoryProducts.Location;

                db.Transactions.Add(transaction);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(invModel);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,WarehouseFrom,WarehouseTo,Reason")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
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
