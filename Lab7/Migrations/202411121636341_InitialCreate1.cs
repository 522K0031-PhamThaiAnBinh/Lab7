namespace Lab7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "ItemID", "dbo.Items");
            AddColumn("dbo.OrderDetails", "Item_ItemID", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Item_ItemID");
            AddForeignKey("dbo.OrderDetails", "Item_ItemID", "dbo.Items", "ItemID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Item_ItemID", "dbo.Items");
            DropIndex("dbo.OrderDetails", new[] { "Item_ItemID" });
            DropColumn("dbo.OrderDetails", "Item_ItemID");
            AddForeignKey("dbo.OrderDetails", "ItemID", "dbo.Items", "ItemID", cascadeDelete: true);
        }
    }
}
