using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoTelefono
    {
        private CD_TipoTelefono objCapaDato = new CD_TipoTelefono();

        public List<TipoTelefono> Listar()
        {
            return objCapaDato.Listar();
        }

        public bool Registrar(TipoTelefono obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(TipoTelefono obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
