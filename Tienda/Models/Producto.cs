﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tienda.CustomValidations;

namespace Tienda.Models
{
    [Table("Productos")]
    public class Producto: Entidad
    {
        
        public float Costo { get; set; }

        [CompararPrecio("Costo","El precio de venta debe ser mayor o igual que el costo de compra")]
        public float Precio { get; set; }

        [DataType(DataType.ImageUrl)]
        public byte[] Imagen { get; set; }


        public string ImageURL { get; set; }

        #region FOREINGKEYS
        [Display(Name ="Categoria")]
        public int categoriaid { get;  set;}

        [ForeignKey("categoriaid")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Marca")]
        public int? marcaid { get; set; }

        [ForeignKey("marcaid")]
        public virtual Marca Marca { get; set; }
        #endregion

    }
}