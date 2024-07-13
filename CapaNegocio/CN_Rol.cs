using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Rol
    {
        private CD_Roles objCapaDatos = new CD_Roles();

        public List<Roles> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
