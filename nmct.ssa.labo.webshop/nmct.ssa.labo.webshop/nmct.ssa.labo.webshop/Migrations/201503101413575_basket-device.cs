namespace nmct.ssa.labo.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basketdevice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "Device_Id", "dbo.Devices");
            DropIndex("dbo.BasketItems", new[] { "Device_Id" });
            RenameColumn(table: "dbo.BasketItems", name: "Device_Id", newName: "DeviceId");
            AlterColumn("dbo.BasketItems", "DeviceId", c => c.Int(nullable: false));
            CreateIndex("dbo.BasketItems", "DeviceId");
            AddForeignKey("dbo.BasketItems", "DeviceId", "dbo.Devices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "DeviceId", "dbo.Devices");
            DropIndex("dbo.BasketItems", new[] { "DeviceId" });
            AlterColumn("dbo.BasketItems", "DeviceId", c => c.Int());
            RenameColumn(table: "dbo.BasketItems", name: "DeviceId", newName: "Device_Id");
            CreateIndex("dbo.BasketItems", "Device_Id");
            AddForeignKey("dbo.BasketItems", "Device_Id", "dbo.Devices", "Id");
        }
    }
}
