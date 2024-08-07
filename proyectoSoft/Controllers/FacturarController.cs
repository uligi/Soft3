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
        public ActionResult ListarCargasSeleccionadas(int cotizarCargaID, int UsuarioID)
        {
          

            // Obtener los datos de la factura
            DetalleDeFactura factura = CN_DetalleDeFactura.ListarCargasSeleccionadas(cotizarCargaID, UsuarioID);


            return View(factura);
        }
    }
}