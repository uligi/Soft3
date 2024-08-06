using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Direcciones
    {
        private CD_Direccion objCapaDato = new CD_Direccion();

        public List<Direccion> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Direccion obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(Direccion obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public List<Direccion> ListarDirecciones(int ClienteID)
        {
            return objCapaDato.ListarDirecciones(ClienteID);
        }
    }
}
