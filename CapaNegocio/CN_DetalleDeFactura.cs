using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_DetalleDeFactura
    {
        private CD_DetalleDeFactura objCapaDato = new CD_DetalleDeFactura();

        public DetalleDeFactura ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            return objCapaDato.ListarCargasSeleccionadas(cotizarCargaID, usuarioID);
        }

    }
}
