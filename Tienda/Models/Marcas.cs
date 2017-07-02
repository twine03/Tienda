using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Marca: Entidad
    {
        public ICollection<Producto> Productos { get; set; }

    }
}