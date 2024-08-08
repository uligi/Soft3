using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuarios> lista = new CN_Usuario().Listar();
            var result = lista.Select(u => new
            {
                UsuarioID = u.UsuarioID,
                Cedula = u.Cedula,
                Nombre = u.Persona.Nombre,
                Apellido1 = u.Persona.Apellido1,
                Apellido2 = u.Persona.Apellido2,
                Rol = u.Rol.Rol,
                Correo = u.Persona.Correo.DireccionCorreo,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion.ToString("yyyy-MM-dd")
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios usuario)
        {
            string mensaje = string.Empty;
            object resultado;
            if (usuario.UsuarioID == 0)
            {
                resultado = new CN_Usuario().Registrar(usuario, out mensaje);
            }
            else
            {
                resultado = new CN_Usuario().Editar(usuario, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje =mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int usuarioID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Usuario().Eliminar(usuarioID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DesactivarUsuario(int usuarioID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Usuario().DesactivarUsuario(usuarioID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult RestablcerContrasena(int UsuarioID)
        {

            Usuarios oUsuario = new CN_Usuario().Listar()
                .Where(u => u.UsuarioID == UsuarioID ).FirstOrDefault();

            if (oUsuario == null)
            {

                ViewBag.Error = "No se encontrò el usuario";

                return View();
            }

            string mensaje = string.Empty ;
            bool respuesta = new CN_Usuario().RestablecerContrasena(oUsuario.UsuarioID, oUsuario.Persona.Correo.DireccionCorreo, out mensaje);

            if (respuesta)
            {

                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();

            }
        }

        public ActionResult Correos()
        {
            return View();
        }

        public JsonResult ListarRoles()
        {
            List<Roles> lista = new CN_Roles().Listar();
            var result = lista.Select(r => new
            {
                r.RolID,
                r.Rol
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCorreos()
        {
            List<Correo> oLista = new CN_Correo().Listar();
            var result = oLista.Select(c => new
            {
                c.CorreoID,
                c.DireccionCorreo,
                TipoCorreo = c.TipoCorreo.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerTiposCorreo()
        {
            List<TipoCorreo> tiposCorreo = new CN_TipoCorreo().Listar();
            return Json(tiposCorreo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCorreo(Correo correo)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (correo.CorreoID == 0)
            {
                resultado = new CN_Correo().RegistrarCorreo(correo, out mensaje);
            }
            else
            {
                resultado = new CN_Correo().ActualizarCorreo(correo, out mensaje) ? 1 : 0;
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCorreo(int correoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Correo().EliminarCorreo(correoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
