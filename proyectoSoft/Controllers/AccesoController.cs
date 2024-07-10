using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace proyectoSoft.Controllers
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

            // Log the received values
            Console.WriteLine($"Received Correo: {correo}");
            Console.WriteLine($"Received Clave: {clave}");

            // Hash the clave
            string hashedClave = CN_Recursos.ConvertirSha256(clave);
            Console.WriteLine($"Hashed Clave: {hashedClave}");

            // Try to find the user
            Usuarios oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Contrasena == hashedClave).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            else
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }


    }
}