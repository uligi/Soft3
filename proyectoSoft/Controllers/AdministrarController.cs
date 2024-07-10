using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace proyectoSoft.Controllers
{
    public class AdministrarController : Controller
    {
        // GET: Administrar
        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {

            List<Usuarios> oLista = new List<Usuarios>();

            oLista = new CN_Usuarios().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.UsuarioID == 0)
            {
                resultado = new CN_Usuarios().RegistrarUsuario(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().ActualizarUsuario(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int cedula)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().EliminarUsuario(cedula,out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult Descuentos()
        {
            return View();
        }

        public ActionResult Impuestos()
        {
            return View();
        }

        public ActionResult TiposDeCargas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerProvincias()
        {
            List<Provincia> provincias = new CN_Provincia().Listar();
            return Json(provincias, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerCantones()
        {
            List<Canton> cantones = new CN_Canton().Listar();
            return Json(cantones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerDistritos()
        {
            List<Distrito> distritos = new CN_Distrito().Listar();
            return Json(distritos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerRoles()
        {
            List<Rol> roles = new CN_Rol().Listar();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerTiposTelefono()
        {
            List<TipoTelefono> tiposTelefono = new CN_TipoTelefono().Listar();
            return Json(tiposTelefono, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerTiposCorreo()
        {
            List<TipoCorreo> tiposCorreo = new CN_TipoCorreo().Listar();
            return Json(tiposCorreo, JsonRequestBehavior.AllowGet);
        }
    }
}
