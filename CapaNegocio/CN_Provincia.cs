using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Provincia
    {
        private CD_Provincia objCapaDatos = new CD_Provincia();

        public List<Provincia> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
