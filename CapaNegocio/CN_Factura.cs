using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public  class CN_Factura
    {
        private CD_Factura objCapaDato = new CD_Factura();
        public DetalleDeFactura ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            return objCapaDato.ListarCargasSeleccionadas(cotizarCargaID, usuarioID);
        }
    }
}
