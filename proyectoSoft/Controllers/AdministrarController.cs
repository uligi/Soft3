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
        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuarios> oLista = new CN_Usuarios().Listar();
            var result = oLista.Select(u => new
            {
                u.UsuarioID,
                u.Persona.Nombre,
                u.Persona.Apellido1,
                u.Persona.Apellido2,
                u.Persona.Correo.DireccionCorreo,
                Rol = u.Rol.Rol
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios usuario)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (usuario.UsuarioID == 0)
            {
                resultado = new CN_Usuarios().RegistrarUsuario(usuario, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().ActualizarUsuario(usuario, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int usuarioID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Usuarios().EliminarUsuario(usuarioID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RestablecerContraseña(int usuarioID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Usuarios().RestablecerContraseña(usuarioID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerRoles()
        {
            List<Roles> lista = new CN_Rol().Listar();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
