namespace Tienda.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permisiontypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetPermisionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetPermisions", "PermisionTypeID", c => c.Int());
            CreateIndex("dbo.AspNetPermisions", "PermisionTypeID");
            AddForeignKey("dbo.AspNetPermisions", "PermisionTypeID", "dbo.AspNetPermisionTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetPermisions", "PermisionTypeID", "dbo.AspNetPermisionTypes");
            DropIndex("dbo.AspNetPermisions", new[] { "PermisionTypeID" });
            DropColumn("dbo.AspNetPermisions", "PermisionTypeID");
            DropTable("dbo.AspNetPermisionTypes");
        }
    }
}
