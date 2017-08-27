using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }
        public ActionResult GerPermisos()
        {
            var permisos = from p in db.Permisions
                           select new
                           {
                               id = p.Id.ToString(),
                               parent = (p.Id == p.ParentID) ? "#" : p.ParentID.ToString(),
                               text = p.Description,
                               type = p.PermisionType.Name
                           };
            return Json(permisos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearRoles()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CrearRoles(AplicationRole role)
        {
            var rolemanager = new CustomRolePrivider();
            rolemanager.CreateRole(role.Name);
            return PartialView();
        }
        [HttpPost]
        public ActionResult SaveRolePermisos()
        {
            
            return RedirectToAction("Index", "Roles");
        }
    }
}