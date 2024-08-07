﻿using CapaNegocio;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;


namespace proyectoSoft.Controllers
{
    [Authorize]
    public class FacturarController : Controller
    {
        private CN_DetalleDeFactura DetalleDeFactura = new CN_DetalleDeFactura();
        // GET: Facturar
        public ActionResult Facturar()
        {

            return View();
        }

        public ActionResult FacturasGuardadas()
        {

            return View();
        }

        [HttpGet]
        public JsonResult ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            var factura = DetalleDeFactura.ListarCargasSeleccionadas(cotizarCargaID, usuarioID);

            if (factura != null)
            {
                var result = new
                {
                    CedulaCliente = factura.CedulaCliente,
                    NombreCliente = factura.NombreCliente,
                    Apellido1Cliente = factura.Apellido1Cliente,
                    Apellido2Cliente = factura.Apellido2Cliente,
                    CorreoCliente = factura.CorreoCliente,
                    TipoCarga = factura.TipoCarga,
                    PrecioPorPeso = factura.PrecioPorPeso,
                    Cantidad = factura.Cantidad,
                    SubTotalGravado = factura.SubTotalGravado,
                    TotalSinDescuento = factura.SubTotalGravado, // Esto se puede ajustar según el cálculo requerido
                    TotalConDescuento = factura.TotalConDescuento,
                    TotalImpuesto = factura.TotalImpuesto,
                    TotalComprobante = factura.TotalComprobante,
                    Representante = factura.Representante
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarFactura(DetalleDeFactura factura)
        {
            string mensaje = string.Empty;
            object resultado = null;

            if (factura.DetalleFacturalID == 0)
            {
                resultado = new CN_DetalleDeFactura().RegistrarFactura(factura, out mensaje);
            }
            else
            {
                // Aquí puedes agregar lógica para actualizar la factura si es necesario
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarFacturas()
        {
            List<DetalleDeFactura> lista = new CN_DetalleDeFactura().ListarFacturas();
            var result = lista.Select(u => new
            {
                DetalleFacturaID = u.DetalleFacturalID,
                CotizarCargaID = u.CotizarCargaID,
                SubTotalGravado = u.SubTotalGravado,
                FechaEmision = u.FechaEmision.ToString("yyyy-MM-dd"),
                TotalSinDescuento = u.TotalSinDescuento,
                TotalConDescuento = u.TotalConDescuento,
                TotalImpuesto = u.TotalImpuesto,
                TotalComprobante = u.TotalComprobante,
                PrecioPorPeso = u.PrecioPorPeso,
                Cantidad = u.Cantidad,
                UsuarioID = u.UsuarioID,
                Activo = u.Activo,
                TiposDeCargaID = u.TiposDeCargaID,
                TipoCarga = u.TipoCarga,
                CedulaCliente = u.CedulaCliente,
                NombreCliente = u.NombreCliente,
                Apellido1Cliente = u.Apellido1Cliente,
                Apellido2Cliente = u.Apellido2Cliente,
                DescuentoID = u.DescuentoID,
                PorcentajeDescuento = u.PorcentajeDescuento,
                TipoDescuento = u.TipoDescuento,
                Representante = u.Representante
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }




    }
}