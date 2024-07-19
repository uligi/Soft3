using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Impuesto
    {
        private CD_Impuesto objCapaDato = new CD_Impuesto();

        public List<Impuesto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Impuesto obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(Impuesto obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
