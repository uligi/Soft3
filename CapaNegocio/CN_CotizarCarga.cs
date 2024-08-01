using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_CotizarCarga
    {
        private CD_CotizarCarga objCapaDatos = new CD_CotizarCarga();

        public int RegistrarCotizacion(CotizarCarga cotizaCarga, out string Mensaje)
        {
            return objCapaDatos.RegistrarCotizacion(cotizaCarga, out Mensaje);
        }
    }
}
