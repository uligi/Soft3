using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Usuario
        public ActionResult Clientes()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> lista = new CN_Clientes().Listar();
            var result = lista.Select(u => new
            {
                ClienteID = u.ClienteID,
                Cedula = u.Cedula,
                Nombre = u.Persona.Nombre,
                Apellido1 = u.Persona.Apellido1,
                Apellido2 = u.Persona.Apellido2,
                TipoCliente = u.TipoCliente,
                Correo = u.Persona.Correo.DireccionCorreo
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult GuardarClientes(Cliente clientes)
        {
            string mensaje = string.Empty;
            object resultado;
            if (clientes.ClienteID== 0)
            {
                resultado = new CN_Clientes().Registrar(clientes, out mensaje);
            }
            else
            {
                resultado = new CN_Clientes().Editar(clientes, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje =mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCliente(int clienteID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Clientes().Eliminar(clienteID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DesactivarCliente(int clienteID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Clientes().DesactivarCliente(clienteID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        

        public ActionResult Correos()
        {
            return View();
        }

        public JsonResult ListarTipoClientes()
        {
            List<TipoCliente> lista = new CN_TipoCliente().Listar();
            var result = lista.Select(r => new
            {
                r.TipoClienteID
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
