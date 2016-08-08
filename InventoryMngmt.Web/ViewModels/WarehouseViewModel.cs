using InventoryMngmt.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryMngmt.Web.ViewModels
{
    public class WarehouseViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Total { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}