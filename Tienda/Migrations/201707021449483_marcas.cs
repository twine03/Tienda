namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marcas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Productos", "marcaid", c => c.Int(nullable: true));
            CreateIndex("dbo.Productos", "marcaid");
            AddForeignKey("dbo.Productos", "marcaid", "dbo.Marcas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "marcaid", "dbo.Marcas");
            DropIndex("dbo.Productos", new[] { "marcaid" });
            DropColumn("dbo.Productos", "marcaid");
            DropTable("dbo.Marcas");
        }
    }
}
