using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoImpuesto
    {
        private CD_TipoImpuesto objCapaDato = new CD_TipoImpuesto();

        public List<TipoImpuesto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarTipoImpuesto(TipoImpuesto tipoImpuesto, out string mensaje)
        {
            return objCapaDato.RegistrarTipoImpuesto(tipoImpuesto, out mensaje);
        }

        public int ActualizarTipoImpuesto(TipoImpuesto tipoImpuesto, out string mensaje)
        {
            return objCapaDato.ActualizarTipoImpuesto(tipoImpuesto, out mensaje);
        }

        public bool EliminarTipoImpuesto(int tipoImpuestoID, out string mensaje)
        {
            return objCapaDato.EliminarTipoImpuesto(tipoImpuestoID, out mensaje);
        }

 
    }
}
