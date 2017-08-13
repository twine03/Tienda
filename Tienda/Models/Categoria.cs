using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tienda.Models
{
    public class Categoria : Entidad
    {
        [Remote("IsCategoriaNameExist", "Validation", ErrorMessage = "Nombre de categoria ya existe")]
        public override string Nombre { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        
    }
}