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
            else if (string.IsNullOrEmpty(obj.Persona.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Persona.Correo.DireccionCorreo))
            {
                Mensaje = "El correo es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Persona.Correo.DireccionCorreo))
            {
                Mensaje = "Debe elegir un tipo de coreo";
            }
            else if (string.IsNullOrEmpty(obj.TipoCliente.Descripcion) || string.IsNullOrWhiteSpace(obj.TipoCliente.Descripcion))
            {
                Mensaje = "Debe elegir un tipo de cliente";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Clientes obj, out string Mensaje)
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
            else if (string.IsNullOrEmpty(obj.Persona.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Persona.Correo.DireccionCorreo))
            {
                Mensaje = "El correo es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Persona.Correo.DireccionCorreo) || string.IsNullOrWhiteSpace(obj.Persona.Correo.DireccionCorreo))
            {
                Mensaje = "Debe elegir un tipo de coreo";
            }
            else if (string.IsNullOrEmpty(obj.TipoCliente.Descripcion) || string.IsNullOrWhiteSpace(obj.TipoCliente.Descripcion))
            {
                Mensaje = "Debe elegir un tipo de cliente";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
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
