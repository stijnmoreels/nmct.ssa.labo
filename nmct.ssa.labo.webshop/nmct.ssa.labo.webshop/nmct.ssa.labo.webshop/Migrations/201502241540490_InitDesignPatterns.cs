namespace nmct.ssa.labo.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDesignPatterns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Devices", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "Description", c => c.String());
            AlterColumn("dbo.Devices", "Name", c => c.String());
        }
    }
}
