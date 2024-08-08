using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {

        //*********************************************************************************************
        // ************************* Clientes *******************************************************
        //*********************************************************************************************
        private CN_Clientes objCapaNegocio = new CN_Clientes();

        public ActionResult Clientes()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Clientes> lista = new CN_Clientes().Listar();
            var result = lista.Select(c => new
            {
                ClienteID = c.ClienteID,
                Cedula = c.Cedula,
                Nombre = c.Persona.Nombre,
                Apellido1 = c.Persona.Apellido1,
                Apellido2 = c.Persona.Apellido2,
                Correo = c.Persona.Correo.DireccionCorreo,
                TipoCliente = c.TipoCliente.Descripcion,
                Activo = c.Activo,
                Fecha = c.Fecha.ToString("yyyy-MM-dd")
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCliente(Clientes cliente)
        {
            string mensaje = string.Empty;
            object resultado;

            if (cliente.Persona == null)
            {
                cliente.Persona = new Persona();
            }
            if (cliente.Persona.Correo == null)
            {
                cliente.Persona.Correo = new Correo();
            }
            if (cliente.Pago == null)
            {
                cliente.Pago = new Pago();
            }

            if (cliente.ClienteID == 0)
            {
                resultado = new CN_Clientes().Registrar(cliente, out mensaje);
            }
            else
            {
                resultado = new CN_Clientes().Editar(cliente, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCliente(int clienteID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Clientes().Eliminar(clienteID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActivarDesactivarCliente(int clienteID, bool activo)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Clientes().ActivarDesactivarCliente(clienteID, activo, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerClientePorCedula(int cedula)
        {
            var cliente = objCapaNegocio.ObtenerClientePorCedula(cedula);
            if (cliente != null)
            {
                var result = new
                {
                    cliente.ClienteID,
                    cliente.Persona.Nombre,
                    cliente.Persona.Apellido1,
                    cliente.Persona.Apellido2,
                    Correo = cliente.Persona.Correo.DireccionCorreo,
                    Telefonos = cliente.Persona.Telefonos.Select(t => new { t.TelefonoID, t.NumeroTelefono }).ToList(),
                    Direcciones = cliente.Persona.Direcciones.Select(d => new { d.DireccionID, d.NombreDireccion }).ToList()
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        //*********************************************************************************************
        // ************************* Direcciones *******************************************************
        //*********************************************************************************************


        [HttpGet]
        public JsonResult ListarDirecciones(int ClienteID)
        {
            System.Diagnostics.Debug.WriteLine("Cédula recibida: " + ClienteID);
            List<Direccion> lista = new CN_Direcciones().ListarDirecciones(ClienteID);
            var result = lista.Select(d => new
            {
                d.DireccionID,
                d.NombreDireccion,
                d.DireccionDetallada,
                ProvinciaDescripcion = d.Provincia.Descripcion,
                CantonDescripcion = d.Canton.Descripcion,
                DistritoDescripcion = d.Distrito.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GuardarDireccion(Direccion direccion)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (direccion.DireccionID == 0)
            {
                resultado = new CN_Direcciones().Registrar(direccion, out mensaje);
            }
            else
            {
                resultado = new CN_Direcciones().Editar(direccion, out mensaje) ? 1 : 0;
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarDireccion(int direccionID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Direcciones().Eliminar(direccionID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProvincias()
        {
            List<Provincia> lista = new CN_Provincia().Listar();
            var result = lista.Select(p => new
            {
                p.ProvinciaID,
                p.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCantonesPorProvincia(int provinciaID)
        {
            List<Canton> lista = new CN_Canton().ListarPorProvincia(provinciaID);
            var result = lista.Select(c => new
            {
                c.CantonID,
                c.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarDistritosPorCanton(int cantonID)
        {
            List<Distrito> lista = new CN_Distrito().ListarPorCanton(cantonID);
            var result = lista.Select(d => new
            {
                d.DistritoID,
                d.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        //*********************************************************************************************
        // ************************* Telefonos *******************************************************
        //*********************************************************************************************

        [HttpGet]
        public JsonResult ListarPorCliente(int Cedula)
        {
            System.Diagnostics.Debug.WriteLine("Cédula recibida: " + Cedula);

            List<Telefono> lista = new CN_Telefono().ListarPorCliente(Cedula);


            var result = lista.Select(i => new
            {
                i.TelefonoID,
                i.NumeroTelefono,
                i.Cedula,
                i.TipoTelefonoID,
                TipoTelefono = i.TipoTelefono.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public JsonResult ListarTiposTelefono()
        {
            List<TipoTelefono> lista = new CN_TipoTelefono().Listar();
            var result = lista.Select(i => new
            {

                i.TipoTelefonoID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTelefono(Telefono telefono)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (telefono.TelefonoID == 0)
            {
                resultado = new CN_Telefono().Registrar(telefono, out mensaje);
            }
            else
            {
                resultado = new CN_Telefono().Editar(telefono, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTelefono(int telefonoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Telefono().Eliminar(telefonoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }



        //*********************************************************************************************
        // ************************* Pagos *******************************************************
        //*********************************************************************************************

        [HttpPost]
        public JsonResult GuardarPago(Pago pago)
        {
            string mensaje = string.Empty;
            int resultado = new CN_Pago().Registrar(pago, out mensaje);

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarTiposPago()
        {
            List<TipoPago> lista = new CN_TipoPago().Listar();
            var result = lista.Select(t => new
            {
                t.TipoPagoID,
                t.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

 

        [HttpGet]
        public JsonResult ListarPagosCliente(int clienteID)
        {
            List<Pago> lista = new CN_Pago().ListarPorCliente(clienteID);
            var result = lista.Select(p => new
            {
                p.PagoID,
                TipoPago = p.TipoPago.Descripcion,
                p.Descripcion,
               
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTiposCorreo()
        {
            List<TipoCorreo> lista = new CN_TipoCorreo().Listar();
            var result = lista.Select(t => new
            {
                t.TipoCorreoID,
                t.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}
