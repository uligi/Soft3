using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoTelefono
    {
        private CD_TipoTelefono objCapaDatos = new CD_TipoTelefono();

        public List<TipoTelefono> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
