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
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Persona.Nombre) || string.IsNullOrWhiteSpace(obj.Persona.Nombre))
            {
                Mensaje = "El nombre es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Apellido1) || string.IsNullOrWhiteSpace(obj.Persona.Apellido1))
            {
                Mensaje = "El primer apellido es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Apellido2) || string.IsNullOrWhiteSpace(obj.Persona.Apellido2))
            {
                Mensaje = "El segundo apellido es obligatorio";
            }
            else if (obj.Persona.Cedula == 0)
            {
                Mensaje = "La cedula es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Correo.DireccionCorreo))
            {
                Mensaje = "El correo es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Correo.DireccionCorreo))
            {
                Mensaje = "Debe elegir un tipo de coreo";
            }
            else if (string.IsNullOrEmpty(obj.Rol.TipoRolDescripcion) || string.IsNullOrWhiteSpace(obj.Rol.TipoRolDescripcion))
            {
                Mensaje = "Debe elegir un tipo de rol";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);

            }
            else
            {
                return 0;
            }

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
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Persona.Nombre) || string.IsNullOrWhiteSpace(obj.Persona.Nombre))
            {
                Mensaje = "El nombre es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Apellido1) || string.IsNullOrWhiteSpace(obj.Persona.Apellido1))
            {
                Mensaje = "El primer apellido es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Apellido2) || string.IsNullOrWhiteSpace(obj.Persona.Apellido2))
            {
                Mensaje = "El segundo apellido es obligatorio";
            }
            else if (obj.Persona.Cedula == 0)
            {
                Mensaje = "La cedula es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Correo.DireccionCorreo))
            {
                Mensaje = "El correo es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Correo.DireccionCorreo))
            {
                Mensaje = "Debe elegir un tipo de coreo";
            }
            else if (string.IsNullOrEmpty(obj.Rol.TipoRolDescripcion) || string.IsNullOrWhiteSpace(obj.Rol.TipoRolDescripcion))
            {
                Mensaje = "Debe elegir un tipo de rol";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);

            }
            else
            {
                return 0;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int UsuarioID, string nuevaClave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(UsuarioID, nuevaClave, out Mensaje);
        }

        public bool DesactivarUsuario(int id, out string Mensaje)
        {
            return objCapaDato.DesactivarUsuario(id, out Mensaje);
        }

        public bool RestablecerContrasena(int UsuarioID, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.RestablecerContrasena(UsuarioID, CN_Recursos.ConvertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3><br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contrasena";
                return false;

            }

         
        }

    }
}
