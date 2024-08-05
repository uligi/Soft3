using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administradores.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult CotizarCargas()
        {
            return View();
        }
        public ActionResult Facturar()
        {
            return View();
        }
        public ActionResult Pagos()
        {
            return View();
        }
        public ActionResult Reportes()
        {
            return View();
        }
    }
}