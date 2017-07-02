namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductoCategoriaCreate : DbMigration
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
                        Nombre = c.String(),
                        Costo = c.Single(nullable: false),
                        Precio = c.Single(nullable: false),
                        categoriaid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.categoriaid, cascadeDelete: true)
                .Index(t => t.categoriaid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "categoriaid", "dbo.Categorias");
            DropIndex("dbo.Productos", new[] { "categoriaid" });
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
