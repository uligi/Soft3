using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Pago
    {
        private CD_Pago objCapaDato = new CD_Pago();

        public List<Pago> ListarPorCliente(int clienteID)
        {
            return objCapaDato.ListarPorCliente(clienteID);
        }

        public int Registrar(Pago obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(Pago obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
