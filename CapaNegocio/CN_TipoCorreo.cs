using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoCorreo
    {
        private CD_TipoCorreo objCapaDato = new CD_TipoCorreo();

        public List<TipoCorreo> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(TipoCorreo obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(TipoCorreo obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
