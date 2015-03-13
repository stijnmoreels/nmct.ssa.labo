namespace nmct.ssa.labo.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basketitems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "Available", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasketItems", "Available");
        }
    }
}
