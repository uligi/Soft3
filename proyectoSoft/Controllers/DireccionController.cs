using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace proyectoSoft.Controllers
{
    public class DireccionController : Controller
    {
        // GET: Direccion
        public ActionResult Direccion()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarDirecciones()
        {
            List<Direccion> lista = new CN_Direccion().Listar();
            var result = lista.Select(i => new
            {
                i.DireccionID,
                i.NombreDireccion,
                i.DireccionDetallada,
                i.ProvinciaID,
                Provincia = i.Provincia.Descripcion,
                i.CantonID,
                Canton = i.Canton.Descripcion,
                i.DistritoID,
                Distrito = i.Distrito.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDireccion(Direccion direccion)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (direccion.DireccionID == 0)
            {
                resultado = new CN_Direccion().Registrar(direccion, out mensaje);
            }
            else
            {
                resultado = new CN_Direccion().Actualizar(direccion, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarDireccion(int direccionID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Direccion().Eliminar(direccionID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
