using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace Administradores.Controllers
{
    public class AdministrarController : Controller
    {
        // GET: Usuarios
       

        [HttpGet]
        public JsonResult ObtenerRoles()
        {
            List<Roles> lista = new CN_Roles().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


       

        public ActionResult Roles()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Roles> oLista = new CN_Roles().Listar();
            var result = oLista.Select(r => new
            {
                r.RolID,
                r.Rol,
                r.TipoRolID,
                TipoRolDescripcion = r.TipoRolDescripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarRol(Roles rol)
        {
            string mensaje = string.Empty;
            bool resultado = false;
            if (rol.RolID == 0)
            {
                resultado = new CN_Roles().Registrar(rol, out mensaje);
            }
            else
            {
                resultado = new CN_Roles().Editar(rol, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarRol(int id)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Roles().Eliminar(id, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTipoRol()
        {
            List<Permisos> lista = new CN_Permisos().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}
