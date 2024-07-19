using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Direccion
    {
        private CD_Direccion objCapaDato = new CD_Direccion();

        public List<Direccion> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Direccion direccion, out string mensaje)
        {
            return objCapaDato.Registrar(direccion, out mensaje);
        }

        public int Actualizar(Direccion direccion, out string mensaje)
        {
            return objCapaDato.Actualizar(direccion, out mensaje);
        }

        public bool Eliminar(int direccionID, out string mensaje)
        {
            return objCapaDato.Eliminar(direccionID, out mensaje);
        }
    }
}
