using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Clientes
    {
        private CD_Clientes objCapaDato = new CD_Clientes();

        public List<Clientes> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Clientes obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(Clientes obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool ActivarDesactivarCliente(int id, bool activo, out string Mensaje)
        {
            return objCapaDato.ActivarDesactivarCliente(id, activo, out Mensaje);
        }

        private CD_Clientes objCapaDatos = new CD_Clientes();
        public Clientes ObtenerClientePorCedula(int cedula)
        {
            return objCapaDatos.ObtenerClientePorCedula(cedula);
        }


    }
}
