using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class TiendaContext : DbContext
    {

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        public TiendaContext() 
            : base ("TiendaConectionString")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().Property(m => m.Imagen).IsOptional();
            //modelBuilder.Entity<Producto>().HasKey(p => new { p.Id, p.marcaid });
            base.OnModelCreating(modelBuilder);
        }

    }
}