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
    public class DescuentoController : Controller
    {
        // GET: Descuento
        public ActionResult Descuentos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarDescuentos()
        {
            List<Descuento> lista = new CN_Descuento().Listar();
            var result = lista.Select(i => new
            {
                i.DescuentoID,
                i.Porcentaje,
                TipoDescuento = i.TipoDescuento.Descripcion,
                i.MontoMinimo,
                i.MontoMaximo,
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDescuento(Descuento descuento)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (descuento.DescuentoID == 0)
            {
                resultado = new CN_Descuento().Registrar(descuento, out mensaje);
            }
            else
            {
                resultado = new CN_Descuento().Actualizar(descuento, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarDescuento(int descuentoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Descuento().Eliminar(descuentoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
