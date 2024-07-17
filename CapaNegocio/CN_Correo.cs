using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Correo
    {
        private CD_Correo objCapaDato = new CD_Correo();

        public List<Correo> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarCorreo(Correo obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool ActualizarCorreo(Correo obj, out string Mensaje)
        {
            return objCapaDato.Actualizar(obj, out Mensaje);
        }

        public bool EliminarCorreo(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
