using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace proyectoSoft.Controllers
{
    [Authorize]
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

        [HttpPost]
        public JsonResult GuardarCarga(CotizarCarga cotizacion)
        {
            string mensaje = string.Empty;
            object resultado;

            if (cotizacion.CotizarCargaID == 0)
            {
                resultado = new CN_CotizarCarga().RegistrarCotizacion(cotizacion, out mensaje);
            }
            else
            {
                resultado = new CN_CotizarCarga().ActualizarCotizacion(cotizacion, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCotizacion(int cotizaCargaID)
        {
            string mensaje = string.Empty;
            bool resultado = _cotizaCargaNegocio.Eliminar(cotizaCargaID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult EliminarTipoDeCarga(int cotizaCargaID)
        {
            string mensaje = string.Empty;
            bool resultado = _cotizaCargaNegocio.Eliminar(cotizaCargaID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        // GET: Facturar Cotizacion
        public ActionResult FacturarCotizacion(int id)
        {
            // Lógica para facturar la cotización
            // Redirigir a la vista de facturación o realizar el proceso adecuado
            return View(); // Implementar la lógica según los requisitos
        }

        [HttpGet]
        public JsonResult ListarCotizaciones()
        {
            List<CotizarCarga> lista = new CN_CotizarCarga().Listar();
            var result = lista.Select(u => new
            {
                CotizarCargaID = u.CotizarCargaID,
                Cedula = u.Clientes.Persona.Cedula,
                Nombre = u.Clientes.Persona.Nombre,
                Apellido1 = u.Clientes.Persona.Apellido1,
                Apellido2 = u.Clientes.Persona.Apellido2,
                Correo = u.Clientes.Persona.Correo.DireccionCorreo,
                TipoCarga = u.TiposDeCarga.Nombre,
                TotalPagar = u.TotalPagar,
                Fecha = u.Fecha.ToString("yyyy-MM-dd")
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}
