using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoCorreo
    {
        private CD_TipoCorreo objCapaDatos = new CD_TipoCorreo();

        public List<TipoCorreo> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
