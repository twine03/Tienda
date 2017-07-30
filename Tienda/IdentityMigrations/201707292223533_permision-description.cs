namespace Tienda.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permisiondescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetPermisions", "Description", c => c.String());
            AlterColumn("dbo.AspNetPermisions", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetPermisions", "Name", c => c.String());
            DropColumn("dbo.AspNetPermisions", "Description");
        }
    }
}
