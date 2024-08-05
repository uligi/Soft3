using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoSoft.Controllers
{
    [Authorize]
    public class ErroresController : Controller
    {
        // GET: Errores
        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error401()
        {
            return View();
        }

        public ActionResult Error(int statusCode, string aspxerrorpath)
        {
            Response.StatusCode = statusCode;

            // Puedes usar `statusCode` para determinar la vista de error que deseas mostrar.
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Página no encontrada.";
                    return View("Error404");
                case 500:
                    ViewBag.ErrorMessage = "Error interno del servidor.";
                    return View("Error500");
                case 401:
                    ViewBag.ErrorMessage = "No autorizado.";
                    return View("Error401");
                default:
                    ViewBag.ErrorMessage = "Error desconocido.";
                    return View("GeneralError"); // Vista genérica para otros errores
            }
        }
    }
}