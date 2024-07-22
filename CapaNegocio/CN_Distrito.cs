using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Distrito
    {
        private CD_Distrito objCapaDato = new CD_Distrito();

        public List<Distrito> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Distrito obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public int Editar(Distrito obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
        public List<Distrito> ListarPorCanton(int cantonID)
        {
            return objCapaDato.ListarPorCanton(cantonID);
        }
    }
}
