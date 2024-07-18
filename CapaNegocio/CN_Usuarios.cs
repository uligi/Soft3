using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCapaDato = new CD_Usuario();

        public List<Usuarios> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            string clave = CN_Recursos.GenerarClave();
            obj.Contrasena = CN_Recursos.ConvertirSha256(clave); // Hash password
            int resultado = objCapaDato.Registrar(obj, out Mensaje);

            if (resultado > 0)
            {
                // Send email with new password
                if (obj.Persona != null && obj.Persona.Correo != null)
                {
                    string asunto = "Bienvenido al Sistema";
                    string mensaje = $"Su nueva contraseña es: {clave}";
                    CN_Recursos.EnviarCorreo(obj.Persona.Correo.DireccionCorreo, asunto, mensaje);
                }
            }

            return resultado;
        }


        public bool Editar(Usuarios obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool DesactivarUsuario(int id, out string Mensaje)
        {
            return objCapaDato.DesactivarUsuario(id, out Mensaje);
        }

        public bool RestablecerContrasena(int id, out string Mensaje)
        {
            string nuevaContrasena = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.RestablecerContrasena(id, CN_Recursos.ConvertirSha256(nuevaContrasena), out Mensaje);
            if (resultado)
            {
                // Send email with new password
                Usuarios usuario = Listar().Find(u => u.UsuarioID == id);
                if (usuario != null && usuario.Persona != null && usuario.Persona.Correo != null)
                {
                    string asunto = "Restablecimiento de Contraseña";
                    string mensaje = $"Su nueva contraseña es: {nuevaContrasena}";
                    CN_Recursos.EnviarCorreo(usuario.Persona.Correo.DireccionCorreo, asunto, mensaje);
                }
            }
            return resultado;
        }
    }
}
