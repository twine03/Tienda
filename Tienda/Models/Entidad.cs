using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Entidad
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}