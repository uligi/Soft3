using CapaEntidad;
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

        public ActionResult Provincia()
        {
            return View();
        }
        public ActionResult Distrito()
        {
            return View();
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

        public ActionResult TipoPago()
        {
            return View();
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
        public JsonResult Listar()
        {
            List<Permisos> oLista = new CN_Permisos().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Permisos permiso)
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
        public JsonResult Eliminar(int id)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Permisos().Eliminar(id, out mensaje);
            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}