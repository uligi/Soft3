using CapaNegocio;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace proyectoSoft.Controllers
{
    [Authorize]
    public class FacturarController : Controller
    {
        private CN_DetalleDeFactura DetalleDeFactura = new CN_DetalleDeFactura();
        // GET: Facturar
        public ActionResult Facturar()
        {

            return View();
        }

        public ActionResult FacturasGuardadas()
        {

            return View();
        }

        [HttpGet]
        public JsonResult ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            var factura = DetalleDeFactura.ListarCargasSeleccionadas(cotizarCargaID, usuarioID);

            if (factura != null)
            {
                var result = new
                {
                    CedulaCliente = factura.CedulaCliente,
                    NombreCliente = factura.NombreCliente,
                    Apellido1Cliente = factura.Apellido1Cliente,
                    Apellido2Cliente = factura.Apellido2Cliente,
                    CorreoCliente = factura.CorreoCliente,
                    TipoCarga = factura.TipoCarga,
                    PrecioPorPeso = factura.PrecioPorPeso,
                    Cantidad = factura.Cantidad,
                    SubTotalGravado = factura.SubTotalGravado,
                    TotalSinDescuento = factura.SubTotalGravado, // Esto se puede ajustar según el cálculo requerido
                    TotalConDescuento = factura.TotalConDescuento,
                    TotalImpuesto = factura.TotalImpuesto,
                    TotalComprobante = factura.TotalComprobante,
                    Representante = factura.Representante
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}