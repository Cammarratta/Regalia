using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryMngmt.Entities;
namespace InventoryMngmt.Web.Models
{
    public class WarehouseModel
    {
        public enum Warehouse
        {
            Dallas,
            Austin,
            Chicago,
            Lansing,
            Denver
        }
    }
}