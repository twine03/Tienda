namespace Tienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicio : DbMigration
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
                        Imagen = c.Binary(),
                        ImageURL = c.String(),
                        categoriaid = c.Int(nullable: false),
                        marcaid = c.Int(),
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
            
            CreateTable(
                "dbo.AspNetPermisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PermisionTypeID = c.Int(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetPermisions", t => t.ParentID)
                .ForeignKey("dbo.AspNetPermisionTypes", t => t.PermisionTypeID)
                .Index(t => t.PermisionTypeID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.AspNetPermisionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRolePermisions",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        PermisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermisionId })
                .ForeignKey("dbo.AspNetPermisions", t => t.PermisionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermisionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRolePermisions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRolePermisions", "PermisionId", "dbo.AspNetPermisions");
            DropForeignKey("dbo.AspNetPermisions", "PermisionTypeID", "dbo.AspNetPermisionTypes");
            DropForeignKey("dbo.AspNetPermisions", "ParentID", "dbo.AspNetPermisions");
            DropForeignKey("dbo.Productos", "marcaid", "dbo.Marcas");
            DropForeignKey("dbo.Productos", "categoriaid", "dbo.Categorias");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetRolePermisions", new[] { "PermisionId" });
            DropIndex("dbo.AspNetRolePermisions", new[] { "RoleId" });
            DropIndex("dbo.AspNetPermisions", new[] { "ParentID" });
            DropIndex("dbo.AspNetPermisions", new[] { "PermisionTypeID" });
            DropIndex("dbo.Productos", new[] { "marcaid" });
            DropIndex("dbo.Productos", new[] { "categoriaid" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetRolePermisions");
            DropTable("dbo.AspNetPermisionTypes");
            DropTable("dbo.AspNetPermisions");
            DropTable("dbo.Marcas");
            DropTable("dbo.Productos");
            DropTable("dbo.Categorias");
        }
    }
}
