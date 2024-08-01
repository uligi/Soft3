﻿using System.Linq;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace Administradores.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Correo y contraseña no pueden estar vacíos.";
                return View();
            }

            // Hash the clave
            string hashedClave = CN_Recursos.ConvertirSha256(clave);

            // Try to find the user
            Usuarios oUsuario = new CN_Usuario().Listar()
                .FirstOrDefault(u => u.Persona.Correo.DireccionCorreo == correo && u.Contrasena == hashedClave);

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            else
            {
                ViewBag.Error = null;
                Session["Usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            // Eliminar la sesión del usuario
            Session["Usuario"] = null;
            // Redirigir al usuario a la página de inicio de sesión
            return RedirectToAction("Index", "Acceso");
        }
    }
}
