using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Telefono
    {
        private CD_Telefono objCapaDato = new CD_Telefono();

        public List<Telefono> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<Telefono> ListarTelefonos(int clienteID)
        {
            return objCapaDato.Listar();
        }
        public List<Telefono> ListarPorCliente(int clienteID)
        {
            return objCapaDato.ListarPorCliente(clienteID);
        }
        public int Registrar(Telefono obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(Telefono obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
