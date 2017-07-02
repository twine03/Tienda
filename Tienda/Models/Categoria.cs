using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Categoria: Entidad
    {
        public virtual ICollection<Producto> Productos { get; set; }
    }
}