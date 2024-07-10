using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public int RegistrarUsuario(Usuarios usuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (usuario.Persona == null)
            {
                usuario.Persona = new Persona();
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de Cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(usuario.Persona.Correo.DireccionCorreo, asunto, mensaje_correo);

                if (respuesta)
                {
                    usuario.Contrasena = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.RegistrarUsuario(usuario, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public int ActualizarUsuario(Usuarios usuario, out string Mensaje)
        {
            return objCapaDato.ActualizarUsuario(usuario, out Mensaje);
        }

        public bool EliminarUsuario(int cedula, out string Mensaje)
        {
            return objCapaDato.EliminarUsuario(cedula, out Mensaje);
        }

        // Métodos auxiliares para validaciones
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = new Regex(@"^\d{8,20}$"); // Ajustar según formato específico
            return phoneRegex.IsMatch(phoneNumber);
        }
    }
}

