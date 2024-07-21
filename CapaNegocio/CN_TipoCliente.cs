using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoCliente
    {
        private CD_TipoCliente objCapaDato = new CD_TipoCliente();

        public List<TipoCliente> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(TipoCliente obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(TipoCliente obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
