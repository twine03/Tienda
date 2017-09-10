using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;
namespace Tienda.Controllers
{
    
    public class ReportsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reports
        public ActionResult Productos()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rptProductos.rpt"));
            var ds = (from p in db.Productos
                      select new
                      {
                          id = p.Id,
                          Nombre = p.Nombre,
                          Costo = p.Costo,
                          Precio = p.Precio,
                          idCategoria = p.categoriaid,
                          Categoria = p.Categoria.Nombre
                      }).ToList();

            rd.SetDataSource(ds);

            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
    }
}