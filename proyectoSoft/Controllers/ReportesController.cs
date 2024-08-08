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

        public ActionResult MontosPorConceptoDescuentos()
        {
            return View();
        }
        public ActionResult MontosPorPeriodo()
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

        [HttpGet]
        public JsonResult ObtenerMontosPorDescuento(int descuentoID)
        {
            var lista = cnReporte.ObtenerMontosPorDescuento(descuentoID);
            var result = lista.Select(r => new
            {
                Descripcion = r.Descripcion,
                MontoTotal = r.MontoTotal
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerMontosPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = cnReporte.ObtenerMontosPorPeriodo(fechaInicio, fechaFin);
            var result = lista.Select(r => new
            {
                FechaEmision = r.FechaEmision.ToString("yyyy-MM-dd"),
                MontoTotal = r.MontoTotal
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}
