namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodicto_imagen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productos", "Imagen", c => c.Binary());
            AddColumn("dbo.Productos", "ImageURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productos", "ImageURL");
            DropColumn("dbo.Productos", "Imagen");
        }
    }
}
