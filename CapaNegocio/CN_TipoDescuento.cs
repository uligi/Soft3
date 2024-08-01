using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoDescuento
    {
        private CD_TipoDescuento objCapaDato = new CD_TipoDescuento();

        public List<TipoDescuento> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarTipoDescuento(TipoDescuento tipoDescuento, out string mensaje)
        {
            return objCapaDato.RegistrarTipoDescuento(tipoDescuento, out mensaje);
        }

        public int ActualizarTipoDescuento(TipoDescuento tipoDescuento, out string mensaje)
        {
            return objCapaDato.ActualizarTipoDescuento(tipoDescuento, out mensaje);
        }

        public bool EliminarTipoDescuento(int tipoDescuentoID, out string mensaje)
        {
            return objCapaDato.EliminarTipoDescuento(tipoDescuentoID, out mensaje);
        }
    }
}
