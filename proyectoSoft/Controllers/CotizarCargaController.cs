using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace proyectoSoft.Controllers
{
    public class CotizarCargaController : Controller
    {
        private CN_Descuento _descuentoNegocio = new CN_Descuento();
        private CN_Impuesto _impuestoNegocio = new CN_Impuesto();
        private CN_CotizarCarga _cotizaCargaNegocio = new CN_CotizarCarga();

        // GET: CotizarCarga
        public ActionResult CotizarCarga()
        {
            return View();
        }

 

     

        [HttpGet]
        public JsonResult ListarTiposDeCarga()
        {
            var tiposDeCarga = new CN_TiposDeCarga().Listar();
            return Json(tiposDeCarga, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarDescuentos()
        {
            var descuentos = _descuentoNegocio.Listar();
            return Json(descuentos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarImpuestos()
        {
            var impuestos = _impuestoNegocio.Listar();
            return Json(impuestos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargasCotizadas()
        {
            return View();
        }


    }
}
