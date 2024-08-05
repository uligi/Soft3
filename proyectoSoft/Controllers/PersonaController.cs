using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using CapaEntidad;
using CapaNegocio;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Persona()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarPersonas()
        {
            List<Persona> oLista = new CN_Persona().Listar();
            var result = oLista.Select(p => new
            {
                p.Cedula,
                p.Nombre,
                p.Apellido1,
                p.Apellido2,
                p.CorreoID,
                Correo = p.Correo.DireccionCorreo,
                TipoCorreo = p.Correo.TipoCorreo.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPersona(Persona persona)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            bool correoRegistrado = false;

            try
            {
                using (var transaction = new TransactionScope())
                {
                    // Primero registrar o actualizar el correo
                    if (persona.Correo.CorreoID == 0)
                    {
                        resultado = new CN_Correo().RegistrarCorreo(persona.Correo, out mensaje);
                        correoRegistrado = resultado > 0;
                    }
                    else
                    {
                        correoRegistrado = new CN_Correo().ActualizarCorreo(persona.Correo, out mensaje);
                        resultado = correoRegistrado ? persona.Correo.CorreoID : 0;
                    }

                    if (!correoRegistrado)
                    {
                        throw new Exception("Error al registrar/actualizar el correo: " + mensaje);
                    }

                    // Registrar o actualizar la persona
                    persona.CorreoID = resultado;
                    if (persona.Cedula == 0)
                    {
                        resultado = new CN_Persona().RegistrarPersona(persona, out mensaje);
                    }
                    else
                    {
                        resultado = new CN_Persona().ActualizarPersona(persona, out mensaje) ? persona.Cedula : 0;
                    }

                    if (resultado <= 0)
                    {
                        throw new Exception("Error al registrar/actualizar la persona: " + mensaje);
                    }

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = 0;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarPersona(int cedula)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Persona().EliminarPersona(cedula, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}
