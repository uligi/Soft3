using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_CotizarCarga
    {
        private CD_CotizarCarga objCapaDatos = new CD_CotizarCarga();


        public List<CotizarCarga> Listar()
        {
            return objCapaDatos.Listar();
        }
        public bool RegistrarCotizacion(CotizarCarga cotizacion, out string mensaje)
        {
            return new CD_CotizarCarga().RegistrarCotizacion(cotizacion, out mensaje);
        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }

        public CotizarCarga ObtenerCotizacionPorID(int id)
        {
            return objCapaDatos.ObtenerCotizacionPorID(id);
        }

    }
}
