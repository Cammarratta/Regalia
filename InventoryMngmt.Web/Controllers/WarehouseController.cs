using InventoryMngmt.Entities;
using InventoryMngmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryMngmt.Web.Controllers
{
    public class WarehouseController : Controller
    {
        private InventoryMngmtDbContext db = new InventoryMngmtDbContext();

        // GET: Warehouse
        public ActionResult Index(Warehouse? Location, InventoryType? Type)
        {
            ViewBag.Location = (from r in db.Products
                                   select r.Location).Distinct();

            ViewBag.Type = (from r in db.Products
                            select r.Type).Distinct();

            var model = db.Products.AsEnumerable().Select(t => t)
                .Where(t => t.Location == Location || Location.ToString() == String.Empty || Location == null)
                .Where(t => t.Type == Type || Type.ToString() == String.Empty || Type == null)
                .Distinct()
                .ToList();

            return View(model);
        }
        
    }
}
