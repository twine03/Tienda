using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Tienda.Models;

namespace Tienda
{
    public static class Utilidades
    {
        /// <summary>
        /// Metodo carga una imagen en la base de datos como un arreglo de bytes
        /// </summary>
        /// <param name="file">Archivo de imagen cargado mediante una peticion Web</param>
        /// <param name="album">Objeto album al que va a ser cargado el archivo como un arreglo de bytes</param>
        public static void UploadImageProductoToDataBase(HttpPostedFileBase file, 
            Producto producto)
        {
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }
            else
            {
                if (producto.Id > 0)
                {
                    TiendaContext db = new TiendaContext();
                    var q = from temp in db.Productos where temp.Id 
                            == producto.Id select temp.Imagen;
                    byte[] cover = q.First();
                    producto.Imagen = cover;
                }
                else
                    producto.Imagen = null;
            }
        }

        /// <summary>
        /// Metodo convierte un archivo cargado mediante una peticion web en un arreglo de bytes
        /// </summary>
        /// <param name="file">Archivo de imagen cargado mediante una peticion Web</param>
        /// <returns>Arreglo de Bytes</returns>
        private static byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageByte = reader.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        internal static byte[] ImageToArray(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        internal static byte[] GetImageProductoFromDataBase(int id)
        {
            TiendaContext db = new TiendaContext();
            var q = from tmp in db.Productos
                    where tmp.Id == id select tmp.Imagen;
            byte[] imagen = q.First();
            return imagen;
        }
    }
}