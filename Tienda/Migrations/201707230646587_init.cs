namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Costo = c.Single(nullable: false),
                        Precio = c.Single(nullable: false),
                        Imagen = c.Binary(),
                        ImageURL = c.String(),
                        categoriaid = c.Int(nullable: false),
                        marcaid = c.Int(),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.categoriaid, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.marcaid)
                .Index(t => t.categoriaid)
                .Index(t => t.marcaid);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "marcaid", "dbo.Marcas");
            DropForeignKey("dbo.Productos", "categoriaid", "dbo.Categorias");
            DropIndex("dbo.Productos", new[] { "marcaid" });
            DropIndex("dbo.Productos", new[] { "categoriaid" });
            DropTable("dbo.Marcas");
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
