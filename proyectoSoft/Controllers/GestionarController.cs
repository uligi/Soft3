﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;


namespace proyectoSoft.Controllers
{
    public class GestionarController : Controller
    {
        // GET: Gestionar


        public ActionResult TipoImpuestos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoImpuestos()
        {
            List<TipoImpuesto> lista = new CN_TipoImpuesto().Listar();
            var result = lista.Select(i => new
            {
                i.TipoImpuestoID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoImpuesto(TipoImpuesto tipoImpuesto)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (tipoImpuesto.TipoImpuestoID == 0)
            {
                resultado = new CN_TipoImpuesto().RegistrarTipoImpuesto(tipoImpuesto, out mensaje);
            }
            else
            {
                resultado = new CN_TipoImpuesto().ActualizarTipoImpuesto(tipoImpuesto, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoImpuesto(int tipoImpuestoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_TipoImpuesto().EliminarTipoImpuesto(tipoImpuestoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TipoDescuentos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoDescuentos()
        {
            List<TipoDescuento> lista = new CN_TipoDescuento().Listar();
            var result = lista.Select(i => new
            {
                i.TipoDescuentoID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoDescuento(TipoDescuento tipoDescuento)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (tipoDescuento.TipoDescuentoID == 0)
            {
                resultado = new CN_TipoDescuento().RegistrarTipoDescuento(tipoDescuento, out mensaje);
            }
            else
            {
                resultado = new CN_TipoDescuento().ActualizarTipoDescuento(tipoDescuento, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoDescuento(int tipoDescuentoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_TipoDescuento().EliminarTipoDescuento(tipoDescuentoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Canton()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCanton()
        {
            List<Canton> lista = new CN_Canton().Listar();
            var result = lista.Select(i => new
            {
                i.CantonID,
                i.Descripcion,
                i.ProvinciaID,
                i.ProvinciaDescripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProvincias()
        {
            List<Provincia> lista = new CN_Provincia().Listar();
            var result = lista.Select(i => new
            {
                i.ProvinciaID,
                i.Descripcion
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCanton(Canton canton)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (canton.CantonID == 0)
            {
                resultado = new CN_Canton().Registrar(canton, out mensaje);
            }
            else
            {
                resultado = new CN_Canton().Editar(canton, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCanton(int cantonID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Canton().Eliminar(cantonID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Provincia()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarProvincia()
        {
            List<Provincia> lista = new CN_Provincia().Listar();
            var result = lista.Select(i => new
            {
                i.ProvinciaID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProvincia(Provincia provincia)
        {
            string mensaje = string.Empty;
            var resultado = 0;
            if (provincia.ProvinciaID == 0)
            {
                resultado = new CN_Provincia().Registrar(provincia, out mensaje);
            }
            else
            {
                resultado = new CN_Provincia().Editar(provincia, out mensaje);
                
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProvincia(int provinciaID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Provincia().Eliminar(provinciaID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Distrito()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarDistrito()
        {
            List<Distrito> lista = new CN_Distrito().Listar();
            var result = lista.Select(i => new
            {
                i.DistritoID,
                i.Descripcion,
                CantonDescripcion = i.Canton.Descripcion,
                ProvinciaDescripcion = i.Canton.Provincia.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDistrito(Distrito distrito)
        {
            string mensaje = string.Empty;
            int resultado = 0;
            if (distrito.DistritoID == 0)
            {
                resultado = new CN_Distrito().Registrar(distrito, out mensaje);
            }
            else
            {
                resultado = new CN_Distrito().Editar(distrito, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarDistrito(int distritoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Distrito().Eliminar(distritoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCantonesPorProvincia(int provinciaID)
        {
            List<Canton> lista = new CN_Canton().ListarPorProvincia(provinciaID);
            var result = lista.Select(i => new
            {
                i.CantonID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TipoCorreo()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoCorreo()
        {
            List<TipoCorreo> lista = new CN_TipoCorreo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoCorreo(TipoCorreo objeto)
        {
            string mensaje = string.Empty;
            var resultado = 0;
            if (objeto.TipoCorreoID == 0)
            {
                resultado = new CN_TipoCorreo().Registrar(objeto, out mensaje);
            }
            else
            {
                var response = new CN_TipoCorreo().Editar(objeto, out mensaje);
                resultado = response ? 1 : 0;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoCorreo(int id)
        {
            string mensaje = string.Empty;
            var resultado = new CN_TipoCorreo().Eliminar(id, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TipoTelefono()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoTelefono()
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
        public JsonResult GuardarTipoTelefono(TipoTelefono tipoTelefono)
        {
            string mensaje = string.Empty;
            bool resultado = false;
            if (tipoTelefono.TipoTelefonoID == 0)
            {
                resultado = new CN_TipoTelefono().Registrar(tipoTelefono, out mensaje);
            }
            else
            {
                resultado = new CN_TipoTelefono().Editar(tipoTelefono, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoTelefono(int tipoTelefonoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_TipoTelefono().Eliminar(tipoTelefonoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TipoPago()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoPago()
        {
            List<TipoPago> lista = new CN_TipoPago().Listar();
            var result = lista.Select(i => new
            {
                i.TipoPagoID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoPago(TipoPago tipoPago)
        {
            string mensaje = string.Empty;
            bool resultado;
            if (tipoPago.TipoPagoID == 0)
            {
                resultado = new CN_TipoPago().Registrar(tipoPago, out mensaje);
            }
            else
            {
                resultado = new CN_TipoPago().Editar(tipoPago, out mensaje);
            }
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoPago(int tipoPagoID)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_TipoPago().Eliminar(tipoPagoID, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult TiposDeCarga()
        {
            return View();
        }

       
        [HttpGet]
        public JsonResult ListarTiposDeCarga()
        {
            List<TiposDeCarga> oLista = new CN_TiposDeCarga().Listar();
            var result = oLista.Select(t => new
            {
                t.TiposDeCargaID,
                t.Nombre,
                t.Descripcion,
                t.TarifaPorKilo,
                FechaRegistro = t.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") // Formatear la fecha
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarTipoDeCarga(TiposDeCarga objeto)
        {
            string mensaje = string.Empty;
            var resultado = 0;

            if (objeto.TiposDeCargaID == 0)
            {
                resultado = new CN_TiposDeCarga().Registrar(objeto, out mensaje);
            }
            else
            {
                var respuesta = new CN_TiposDeCarga().Editar(objeto, out mensaje);
                if (respuesta)
                {
                    resultado = objeto.TiposDeCargaID;
                }
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoDeCarga(int id)
        {
            string mensaje = string.Empty;
            var resultado = new CN_TiposDeCarga().Eliminar(id, out mensaje);

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }



        // GET: Permisos
        public ActionResult Permisos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarPermisos()
        {
            List<Permisos> oLista = new CN_Permisos().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPermiso(Permisos permiso)
        {
            string mensaje = string.Empty;
            bool resultado;

            if (permiso.PermisoID == 0)
            {
                resultado = new CN_Permisos().Registrar(permiso, out mensaje);
            }
            else
            {
                resultado = new CN_Permisos().Editar(permiso, out mensaje);
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarPermiso(int id)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Permisos().Eliminar(id, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}