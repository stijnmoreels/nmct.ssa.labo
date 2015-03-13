namespace nmct.ssa.labo.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Amount = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Device_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.Device_Id)
                .Index(t => t.Device_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "Device_Id", "dbo.Devices");
            DropIndex("dbo.BasketItems", new[] { "Device_Id" });
            DropTable("dbo.BasketItems");
        }
    }
}
