namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producto_imageurl_notrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productos", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productos", "ImageURL", c => c.String(nullable: false));
        }
    }
}
