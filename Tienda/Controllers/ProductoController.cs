using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ProductoController : Controller
    {
        private TiendaContext db = new TiendaContext();

        // GET: Producto
        [Authorize(Roles ="0004")]
        public ActionResult Index()
        {
            var productos = db.Productos.ToList();
            return View(productos);
        }
        [Authorize(Roles = "0004")]
        public ActionResult Lista()
        {
            var productos = db.Productos.ToList();
            return PartialView("_Lista", productos);
        }

        // GET: Producto/Details/5
        [Authorize(Roles = "0004")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Page500.cshtml");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                ViewBag.mensaje = "El producto buscado no existe";
                return View("~/Views/Page400.cshtml");
                //return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        [Authorize(Roles = "0001")]
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewData["categorias"] = db.Categorias.ToList();
                ViewBag.Marcas = new SelectList(db.Marcas.ToList(), "Id", "Nombre");
                return PartialView("_Create");
            }
            else
            {
                return Redirect("/Account/LoginPartial");
            }
           
        }
        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file != null)
                {
                    Utilidades.UploadImageProductoToDataBase(file, producto);
                }
                db.Productos.Add(producto);
                db.SaveChanges();
                string url = Url.Action("Index", "Producto", new { id = producto.Id });
                return Json(new { success = true, mensaje = "Producto registrado exitosamente" });
                //return RedirectToAction("Index");
            }
            ViewData["categorias"] = db.Categorias.ToList();
            ViewBag.Marcas = new SelectList(db.Marcas.ToList(), "Id", "Nombre");
            return PartialView("_Create",producto);
        }

        // GET: Producto/Edit/5
        [Authorize(Roles ="0002")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewData["Categorias"] = db.Categorias.ToList();
            ViewBag.Marcas = new SelectList(db.Marcas.ToList(), "Id", "Nombre");
            return PartialView("_Edit",producto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Costo,Precio,categoriaid, marcaid")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file != null)
                {
                    Utilidades.UploadImageProductoToDataBase(file, producto);
                }
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                string url = Url.Action("Lista", "Producto", new { id = producto.Id });
                return Json(new {url=url, success = true, mensaje = "Producto registrado exitosamente" });
            }
            ViewData["Categorias"] = db.Categorias.ToList();
            ViewBag.Marcas = new SelectList(db.Marcas.ToList(), "Id", "Nombre");
            return View("_Edit",producto);
        }

        // GET: Producto/Delete/5
        [Authorize(Roles ="Borrar")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RegresarImagen(int Id)
        {
            if (Id == 0)
            {
                Image Imagen = 
                    Image.FromFile(
                        Server.MapPath("~/Images/image_no_found.jpg")
                        );
                Byte[] arr = Utilidades.ImageToArray(Imagen);
                return File(arr, "image/jpg");
            }
            else
            {
                byte[] imagen = 
                    Utilidades.GetImageProductoFromDataBase(Id);
                if (imagen != null)
                    return File(imagen, "image/jpg");
                else
                    return null;
            }
        }
    }
}
