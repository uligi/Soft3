﻿using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
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
                Cedula = c.Persona.Cedula,
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
        public JsonResult ListarDirecciones(int clienteID)
        {
            List<Direccion> lista = new CN_Direcciones().ListarPorCliente(clienteID);
            var result = lista.Select(d => new
            {
                d.DireccionID,
                d.NombreDireccion,
                d.DireccionDetallada,
                Provincia = d.Provincia.Descripcion,
                Canton = d.Canton.Descripcion,
                Distrito = d.Distrito.Descripcion
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
        public JsonResult ListarTelefonos(int clienteID)
        {
            List<Telefono> lista = new CN_Telefono().ListarPorCliente(clienteID);
            var result = lista.Select(t => new
            {
                t.TelefonoID,
                t.NumeroTelefono,
                TipoTelefono = t.TipoTelefono.Descripcion
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
                resultado = new CN_Telefono().Editar(telefono, out mensaje) ? 1 : 0;
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

        [HttpGet]
        public JsonResult ListarTiposCliente()
        {
            List<TipoCliente> lista = new CN_TipoCliente().Listar();
            var result = lista.Select(t => new
            {
                t.TipoClienteID,
                t.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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
    }
}
