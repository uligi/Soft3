using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Clientes
    {
        private CD_Clientes objCapaDato = new CD_Clientes();

        public List<Cliente> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            string clave = CN_Recursos.GenerarClave();
            int resultado = objCapaDato.Registrar(obj, out Mensaje);

            if (resultado > 0)
            {
                // Send email with new password
                if (obj.Persona != null && obj.Persona.Correo != null)
                {
                    string asunto = "Bienvenido al Sistema";
                    CN_Recursos.EnviarCorreo(obj.Persona.Correo.DireccionCorreo, asunto, Mensaje);
                }
            }

            return resultado;
        }


        public bool Editar(Cliente obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool DesactivarCliente(int id, out string Mensaje)
        {
            return objCapaDato.DesactivarCliente(id, out Mensaje);
        }
  
    }
}
