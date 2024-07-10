using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Rol
    {
        private CD_Rol objCapaDatos = new CD_Rol();

        public List<Rol> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
