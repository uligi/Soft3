using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Canton
    {
        private CD_Canton objCapaDatos = new CD_Canton();

        public List<Canton> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
