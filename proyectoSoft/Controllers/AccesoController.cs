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
                ViewBag.Error = null;
               
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CerrarSesion()
        {
           
           FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}
