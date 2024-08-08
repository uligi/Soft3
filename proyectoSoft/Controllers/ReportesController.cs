using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private CN_Reporte cnReporte = new CN_Reporte();

        public ActionResult MontosPorTiposCargas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerMontosPorTipoDeCarga(int tipoCargaID)
        {
            var lista = cnReporte.ObtenerMontosPorTipoDeCarga(tipoCargaID);
            var result = lista.Select(r => new
            {
                TipoCarga = r.TipoCarga,
                MontoTotal = r.MontoTotal
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}
