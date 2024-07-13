using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuarios> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarUsuario(Usuarios usuario, out string mensaje)
        {
            return objCapaDato.RegistrarUsuario(usuario, out mensaje);
        }

        public int ActualizarUsuario(Usuarios usuario, out string mensaje)
        {
            return objCapaDato.ActualizarUsuario(usuario, out mensaje);
        }

        public bool EliminarUsuario(int usuarioID, out string mensaje)
        {
            return objCapaDato.EliminarUsuario(usuarioID, out mensaje);
        }

        public bool RestablecerContraseña(int usuarioID, out string mensaje)
        {
            return objCapaDato.RestablecerContraseña(usuarioID, out mensaje);
        }
    }
}
