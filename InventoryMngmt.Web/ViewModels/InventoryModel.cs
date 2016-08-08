using InventoryMngmt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryMngmt.Web.ViewModels
{
    public class InventoryModel
    {
        public Product InventoryProducts { get; set; }
        public Transactions InventoryTransactions { get; set; }
    }
}