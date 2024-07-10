using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Distrito
    {
        private CD_Distrito objCapaDatos = new CD_Distrito();

        public List<Distrito> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
