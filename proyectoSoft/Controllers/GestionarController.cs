using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using System.IO;



namespace proyectoSoft.Controllers
{
    [Authorize]
    public class GestionarController : Controller
    {
        // GET: Gestionar

        // **********************************************Tipos de Impuestos****************************************************//
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


        // **********************************************Tipos de Descuentos****************************************************//
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
        // **********************************************Canton****************************************************//
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

        [HttpPost]
        public JsonResult CargarCantonesDesdeCSV(string csvData)
        {
            string mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Convertir el CSV en una lista de objetos Canton
                List<Canton> cantones = new List<Canton>();
                string[] lines = csvData.Split('\n');
                foreach (string line in lines.Skip(1))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        Canton canton = new Canton
                        {
                            Descripcion = values[0].Trim(),
                            ProvinciaID = Convert.ToInt32(values[1].Trim())
                        };
                        cantones.Add(canton);
                    }
                }

                // Guardar los cantones en la base de datos
                foreach (var canton in cantones)
                {
                    int result = new CN_Canton().Registrar(canton, out mensaje);
                    if (result == 0)
                    {
                        throw new Exception(mensaje);
                    }
                }
                resultado = true;
                mensaje = "Cantones cargados correctamente.";
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarPlantillaCSVCanton()
        {
            List<Provincia> provincias = new CN_Provincia().Listar();
            string csvContent = "Descripcion,ProvinciaID\n"; // Puedes añadir más campos aquí según lo necesites

            foreach (var provincia in provincias)
            {
                csvContent += $"Provincia: {provincia.Descripcion}, {provincia.ProvinciaID}\n";
            }

            // Asegurarse de usar la codificación UTF-8 con BOM para manejar caracteres especiales
            byte[] buffer = System.Text.Encoding.UTF8.GetPreamble().Concat(System.Text.Encoding.UTF8.GetBytes(csvContent)).ToArray();
            return File(buffer, "text/csv", "plantilla_cantones.csv");
        }


        // **********************************************Provincias****************************************************//
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

        [HttpPost]
        public JsonResult CargarProvinciasDesdeCSV(string csvData)
        {
            string mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Convertir el CSV en una lista de objetos Provincia
                List<Provincia> provincias = new List<Provincia>();
                string[] lines = csvData.Split('\n');
                foreach (string line in lines.Skip(1))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        Provincia provincia = new Provincia
                        {
                            Descripcion = values[0].Trim()
                        };
                        provincias.Add(provincia);
                    }
                }

                // Guardar las provincias en la base de datos
                foreach (var provincia in provincias)
                {
                    int result = new CN_Provincia().Registrar(provincia, out mensaje);
                    if (result == 0)
                    {
                        throw new Exception(mensaje);
                    }
                }
                resultado = true;
                mensaje = "Provincias cargadas correctamente.";
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarPlantillaCSVProvincia()
        {
            string csvContent = "Descripcion\n"; // Puedes añadir más campos aquí según lo necesites

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(buffer, "text/csv", "plantilla_provincias.csv");
        }

        // **********************************************Distritos****************************************************//



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

        public JsonResult ListarPorCanton(int CantonID)
        {
            List<Distrito> lista = new CN_Distrito().ListarPorCanton(CantonID);
            var result = lista.Select(i => new
            {
                i.DistritoID,
                i.Descripcion
            }).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarDistritosDesdeCSV(string csvData)
        {
            string mensaje = string.Empty;
            bool resultado = false;

            try
            {
                // Convertir el CSV en una lista de objetos Distrito
                List<Distrito> distritos = new List<Distrito>();
                string[] lines = csvData.Split('\n');
                foreach (string line in lines.Skip(1))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        Distrito distrito = new Distrito
                        {
                            Descripcion = values[0].Trim(),
                            CantonID = int.Parse(values[1].Trim())
                        };
                        distritos.Add(distrito);
                    }
                }

                // Guardar los distritos en la base de datos
                foreach (var distrito in distritos)
                {
                    int result = new CN_Distrito().Registrar(distrito, out mensaje);
                    if (result == 0)
                    {
                        throw new Exception(mensaje);
                    }
                }
                resultado = true;
                mensaje = "Distritos cargados correctamente.";
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarPlantillaDistritoCSV()
        {
            string csvContent = "Descripcion,CantonID\n"; // Puedes añadir más campos aquí según lo necesites

            // Aquí agregamos los cantones con sus respectivos IDs
            List<Canton> cantones = new CN_Canton().Listar();
            foreach (var canton in cantones)
            {
                csvContent += $"Canton: {canton.Descripcion}, {canton.CantonID}\n";
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(buffer, "text/csv", "plantilla_distritos.csv");
        }
        // **********************************************Tipo de Correo ****************************************************//

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
        public ActionResult TipoCliente()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoCliente()
        {
            List<TipoCliente> lista = new CN_TipoCliente().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoCliente(TipoCliente objeto)
        {
            string mensaje = string.Empty;
            var resultado = 0;
            if (objeto.TipoClienteID == 0)
            {
                resultado = new CN_TipoCliente().Registrar(objeto, out mensaje);
            }
            else
            {
                var response = new CN_TipoCliente().Editar(objeto, out mensaje);
                resultado = response ? 1 : 0;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoCliente(int id)
        {
            string mensaje = string.Empty;
            var resultado = new CN_TipoCliente().Eliminar(id, out mensaje);
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