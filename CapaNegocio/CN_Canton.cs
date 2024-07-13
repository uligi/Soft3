using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Canton
    {
        private CD_Canton objCapaDato = new CD_Canton();

        public List<Canton> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Canton obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public int Editar(Canton obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
        public List<Canton> ListarPorProvincia(int provinciaID)
        {
            return objCapaDato.ListarPorProvincia(provinciaID);
        }
    }
}
