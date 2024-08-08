using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult MontosPorTiposCargas()
        {
            return View();
        }
    }
}