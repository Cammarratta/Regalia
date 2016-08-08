namespace InventoryMngmt.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Serial = c.String(),
                        Type = c.Int(nullable: false),
                        Brand = c.Int(nullable: false),
                        AcquisitionDate = c.DateTime(),
                        Location = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        WarehouseFrom = c.Int(),
                        WarehouseTo = c.Int(),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        Serial = p.String(),
                        Type = p.Int(),
                        Brand = p.Int(),
                        AcquisitionDate = p.DateTime(),
                        Location = p.Int(),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Products]([Serial], [Type], [Brand], [AcquisitionDate], [Location], [Description])
                      VALUES (@Serial, @Type, @Brand, @AcquisitionDate, @Location, @Description)
                      
                      DECLARE @ProductID int
                      SELECT @ProductID = [ProductID]
                      FROM [dbo].[Products]
                      WHERE @@ROWCOUNT > 0 AND [ProductID] = scope_identity()
                      
                      SELECT t0.[ProductID]
                      FROM [dbo].[Products] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ProductID] = @ProductID"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        ProductID = p.Int(),
                        Serial = p.String(),
                        Type = p.Int(),
                        Brand = p.Int(),
                        AcquisitionDate = p.DateTime(),
                        Location = p.Int(),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [Serial] = @Serial, [Type] = @Type, [Brand] = @Brand, [AcquisitionDate] = @AcquisitionDate, [Location] = @Location, [Description] = @Description
                      WHERE ([ProductID] = @ProductID)"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        ProductID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Products]
                      WHERE ([ProductID] = @ProductID)"
            );
            
            CreateStoredProcedure(
                "dbo.Transactions_Insert",
                p => new
                    {
                        ProductID = p.Int(),
                        WarehouseFrom = p.Int(),
                        WarehouseTo = p.Int(),
                        Reason = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Transactions]([ProductID], [WarehouseFrom], [WarehouseTo], [Reason])
                      VALUES (@ProductID, @WarehouseFrom, @WarehouseTo, @Reason)
                      
                      DECLARE @TransactionID int
                      SELECT @TransactionID = [TransactionID]
                      FROM [dbo].[Transactions]
                      WHERE @@ROWCOUNT > 0 AND [TransactionID] = scope_identity()
                      
                      SELECT t0.[TransactionID]
                      FROM [dbo].[Transactions] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[TransactionID] = @TransactionID"
            );
            
            CreateStoredProcedure(
                "dbo.Transactions_Update",
                p => new
                    {
                        TransactionID = p.Int(),
                        ProductID = p.Int(),
                        WarehouseFrom = p.Int(),
                        WarehouseTo = p.Int(),
                        Reason = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Transactions]
                      SET [ProductID] = @ProductID, [WarehouseFrom] = @WarehouseFrom, [WarehouseTo] = @WarehouseTo, [Reason] = @Reason
                      WHERE ([TransactionID] = @TransactionID)"
            );
            
            CreateStoredProcedure(
                "dbo.Transactions_Delete",
                p => new
                    {
                        TransactionID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Transactions]
                      WHERE ([TransactionID] = @TransactionID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Transactions_Delete");
            DropStoredProcedure("dbo.Transactions_Update");
            DropStoredProcedure("dbo.Transactions_Insert");
            DropStoredProcedure("dbo.Product_Delete");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Insert");
            DropForeignKey("dbo.Transactions", "ProductID", "dbo.Products");
            DropIndex("dbo.Transactions", new[] { "ProductID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Products");
        }
    }
}
