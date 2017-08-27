using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ValidationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public JsonResult IsCategoriaNameExist(string Nombre)
        {

            bool isExist = db.Categorias.Where(cat => cat.Nombre.ToLower().Equals(Nombre.ToLower())).FirstOrDefault() != null;
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
    }
}