using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Direccion
    {
        private CD_Direccion objCapaDato = new CD_Direccion();

        public List<Direccion> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarDireccion(Direccion direccion, out string Mensaje)
        {
            Mensaje = string.Empty;
            return objCapaDato.RegistrarDireccion(direccion, out Mensaje);
        }

        public int ActualizarDireccion(Direccion direccion, out string Mensaje)
        {
            return objCapaDato.ActualizarDireccion(direccion, out Mensaje);
        }

        public bool EliminarDireccion(int direccionID, out string Mensaje)
        {
            return objCapaDato.EliminarDireccion(direccionID, out Mensaje);
        }
    }
}
