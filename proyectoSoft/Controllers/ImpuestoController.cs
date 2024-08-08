using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class ImpuestoController : Controller
    {
        // GET: Impuesto
        public ActionResult Impuesto()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarImpuestos()
        {
            List<Impuesto> lista = new CN_Impuesto().Listar();
            var result = lista.Select(i => new
            {
                ImpuestoID = i.ImpuestoID,
                Porcentaje = i.Porcentaje,
                TipoImpuestoID = i.TipoImpuestoID,
                TipoImpuesto = i.TipoImpuesto.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarImpuesto(Impuesto impuesto)
        {
            string mensaje = string.Empty;
            object resultado;
            if (impuesto.ImpuestoID == 0)
            {
                resultado = new CN_Impuesto().Registrar(impuesto, out mensaje);
            }
            else
            {
                resultado = new CN_Impuesto().Editar(impuesto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarImpuesto(int impuestoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Impuesto().Eliminar(impuestoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
