using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Telefono
    {
        private CD_Telefono objCapaDato = new CD_Telefono();

        public List<Telefono> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<Telefono> ListarPorCliente(int Cedula)
        {
            return objCapaDato.ListarPorCliente(Cedula);
        }
        public int Registrar(Telefono obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public int Editar(Telefono obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public List<Telefono> ListarPorCedula(int Cedula)
        {
            return objCapaDato.ListarPorCedula(Cedula);
        }
    }
}
