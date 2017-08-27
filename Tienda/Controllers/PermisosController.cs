using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class PermisosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Permisos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GerPermisos()
        {
            var permisos = from p in db.Permisions
                           select new {
                               id = p.Id.ToString(),
                               parent = (p.Id==p.ParentID)?"#":p.ParentID.ToString(),
                               text = p.Description,
                               type = p.PermisionType.Name};
            return Json(permisos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddPermiso(int id)
        {
            var permiso = db.Permisions.Find(id);
            ViewBag.PermisionTypes = new SelectList(
                                            getPermisionTypes(permiso.PermisionType,
                                            permiso.ParentID == permiso.Id)
                                            , "Id", "Name");

            ViewBag.ParentId = permiso.Id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddPermiso(Permision permiso)
        {
            if (ModelState.IsValid)
            {
                var permisoparent = db.Permisions.Find(permiso.ParentID);
                //var myPermision = db.Permisions.Create();
                //myPermision.PermisionTypeID = permiso.PermisionTypeID;
                //myPermision.Description = permiso.Description;
                //myPermision.Name = myPermision.Name;
                permiso.Parent = permisoparent;
                db.Permisions.Add(permiso);
                db.SaveChanges();
                string url = Url.Action("Index", "Permiso");
                return Json(new { success = true, mensaje = "Permiso registrado exitosamente" });

            }
            else
                return PartialView(permiso);
        }
       

        public ActionResult EditPermiso(int id)
        {
            var permiso = db.Permisions.Find(id);
            ViewBag.PermisionTypes = new SelectList(
                                            getPermisionTypes(permiso.Parent.PermisionType, 
                                            permiso.ParentID==permiso.Id)
                                            , "Id", "Name");

            ViewBag.ParentId = permiso.ParentID;
            return PartialView("AddPermiso", permiso);
        }
        [HttpPost]
        public ActionResult EditPermiso(Permision permiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permiso).State = EntityState.Modified;
                db.SaveChanges();
                string url = Url.Action("Index", "Permiso");
                return Json(new { success = true, mensaje = "Permiso registrado exitosamente" });

            }
            else
                return PartialView("AddPermiso", permiso);
        }

        [NonAction]
        public List<PermisionType> getPermisionTypes(PermisionType currentType, bool isParent)
        {
            var allowedTypes = new[] { "Otros" };
            if(isParent)
                allowedTypes = new[] { "Modulo"};
            else
                if (currentType.Name == "Modulo")
                {
                    allowedTypes = new[] { "Sub Modulo", "Menu", "Boton" };
                }else if (currentType.Name == "Sub Modulo")
                {
                    allowedTypes = new[] { "Menu", "Boton" };
                }else if (currentType.Name == "Menu")
                {
                    allowedTypes = new[] { "Sub Menu"};
                }else if (currentType.Name == "Sub Menu")
                {
                    allowedTypes = new[] { "Boton" };
                }
            return db.PermisionTypes.Where(p => allowedTypes.Contains(p.Name)).ToList();
        }
    }
}