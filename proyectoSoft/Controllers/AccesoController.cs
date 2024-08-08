using System.Linq;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;

namespace Administradores.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
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

           
            string hashedClave = CN_Recursos.ConvertirSha256(clave);

          
            Usuarios oUsuario = new CN_Usuario().Listar()
                .Where(u => u.Persona.Correo.DireccionCorreo == correo && u.Contrasena == hashedClave).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            else
            {
                if (oUsuario.RestablecerContrasena)
                {
                    TempData["UsuarioID"] = oUsuario.UsuarioID;
                    return RedirectToAction("CambiarClave");
                }
                FormsAuthentication.SetAuthCookie(oUsuario.Persona.Correo.DireccionCorreo, false);
                Session["NombreUsuario"] = oUsuario.Persona.Nombre;
                Session["Rol"] = oUsuario.Rol.Rol;
                Session["UsuarioID"] = oUsuario.UsuarioID;
                ViewBag.Error = null;
               
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave( string UsuarioID,string claveActual,string nuevaClave, string confirmarClave )
        {

            Usuarios oUsuario = new Usuarios();
            oUsuario = new CN_Usuario().Listar()
                .Where(u => u.UsuarioID==int.Parse(UsuarioID)).FirstOrDefault();
            if (oUsuario.Contrasena != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewData["vClave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewData["vClave"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vClave"] = "";
            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuario().CambiarClave(int.Parse(UsuarioID), nuevaClave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                TempData["UsuarioID"] = UsuarioID;
                ViewBag.Error = mensaje;
                return View();
            }

        }


        public ActionResult CerrarSesion()
        {
           
           FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}
