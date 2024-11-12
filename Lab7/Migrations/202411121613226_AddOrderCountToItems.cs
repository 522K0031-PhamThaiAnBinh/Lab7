namespace Lab7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderCountToItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "OrderCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "OrderCount");
        }
    }
}
