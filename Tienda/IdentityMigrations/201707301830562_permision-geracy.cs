namespace Tienda.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permisiongeracy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetPermisions", "ParentID", c => c.Int());
            CreateIndex("dbo.AspNetPermisions", "ParentID");
            AddForeignKey("dbo.AspNetPermisions", "ParentID", "dbo.AspNetPermisions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetPermisions", "ParentID", "dbo.AspNetPermisions");
            DropIndex("dbo.AspNetPermisions", new[] { "ParentID" });
            DropColumn("dbo.AspNetPermisions", "ParentID");
        }
    }
}
