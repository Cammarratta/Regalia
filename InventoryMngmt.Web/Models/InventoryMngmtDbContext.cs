using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryMngmt.Web.Models
{
    public class InventoryMngmtDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public InventoryMngmtDbContext() : base("name=InventoryMngmtDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryMngmt.Entities.Product>().MapToStoredProcedures();
            modelBuilder.Entity<InventoryMngmt.Entities.Transactions>().MapToStoredProcedures();

        }

        public System.Data.Entity.DbSet<InventoryMngmt.Entities.Product> Products { get; set; }

        public System.Data.Entity.DbSet<InventoryMngmt.Entities.Transactions> Transactions { get; set; }
    }
}
