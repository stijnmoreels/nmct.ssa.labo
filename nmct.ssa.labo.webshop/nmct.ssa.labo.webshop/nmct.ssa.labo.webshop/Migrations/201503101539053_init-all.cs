namespace nmct.ssa.labo.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasketItems", "TotalPrice");
        }
    }
}
