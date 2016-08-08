namespace InventoryMngmt.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "WarehouseFrom", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "WarehouseTo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "WarehouseTo", c => c.Int());
            AlterColumn("dbo.Transactions", "WarehouseFrom", c => c.Int());
        }
    }
}
