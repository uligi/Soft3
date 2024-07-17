using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Persona
    {
        private CD_Persona objCapaDato = new CD_Persona();

        public List<Persona> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarPersona(Persona obj, out string Mensaje)
        {
            CD_Correo objCapaDatoCorreo = new CD_Correo();
            int correoID = objCapaDatoCorreo.Registrar(obj.Correo, out Mensaje);
            if (correoID > 0)
            {
                obj.CorreoID = correoID;
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                Mensaje = "Error al registrar el correo.";
                return 0;
            }
        }

        public bool ActualizarPersona(Persona obj, out string Mensaje)
        {
            CD_Correo objCapaDatoCorreo = new CD_Correo();
            bool correoActualizado = objCapaDatoCorreo.Actualizar(obj.Correo, out Mensaje);
            if (correoActualizado)
            {
                return objCapaDato.Actualizar(obj, out Mensaje);
            }
            else
            {
                Mensaje = "Error al actualizar el correo.";
                return false;
            }
        }

        public bool EliminarPersona(int cedula, out string Mensaje)
        {
            return objCapaDato.Eliminar(cedula, out Mensaje);
        }
    }
}
