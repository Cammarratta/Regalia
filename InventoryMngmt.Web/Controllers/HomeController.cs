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
    public class HomeController : Controller
    {
        private InventoryMngmtDbContext db = new InventoryMngmtDbContext();


        public ActionResult Index(Warehouse? location, InventoryType? type)
        {
            ViewBag.WarehouseTo = db.Products.Select(p => p.Location).Distinct();

            ViewBag.Type = db.Products.Select(p => p.Type).Distinct();

            var ProductsList = db.Products.AsEnumerable().Select(t => t)
                .Where(t => t.Location == location || location.ToString() == String.Empty || location == null)
                .Where(t => t.Type == type || type.ToString() == String.Empty || type == null)
                .Distinct()
                .ToList();

            //var ProductsList = db.Products.ToList();
            var model = ProductsList.GroupBy(wvm => new {
                wvm.Location,
                wvm.Type
            })
            .Select(wvm => new WarehouseViewModel
            {
                Products = wvm,
                Name = wvm.Key.Location.GetDisplayName(),
                Type = wvm.Key.Type.GetDisplayName(),
                Total = wvm.Count()
            }
            ).OrderBy(wvm => wvm.Name);

            return View(model);
        }

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
    }
}