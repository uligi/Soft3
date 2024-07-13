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
                TipoRolDescripcion = r.TipoRolDescripcion // Assuming the entity has this property
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
