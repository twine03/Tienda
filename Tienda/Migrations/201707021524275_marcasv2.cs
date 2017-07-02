namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marcasv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Productos", "marcaid", "dbo.Marcas");
            DropIndex("dbo.Productos", new[] { "marcaid" });
            AlterColumn("dbo.Productos", "marcaid", c => c.Int());
            CreateIndex("dbo.Productos", "marcaid");
            AddForeignKey("dbo.Productos", "marcaid", "dbo.Marcas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "marcaid", "dbo.Marcas");
            DropIndex("dbo.Productos", new[] { "marcaid" });
            AlterColumn("dbo.Productos", "marcaid", c => c.Int(nullable: false));
            CreateIndex("dbo.Productos", "marcaid");
            AddForeignKey("dbo.Productos", "marcaid", "dbo.Marcas", "Id", cascadeDelete: true);
        }
    }
}
