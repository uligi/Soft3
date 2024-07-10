using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Persona
    {
  
        public int Cedula { get; set; }

    
        public string Nombre { get; set; }

 
        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

   
        public int DireccionID { get; set; }

        public int TelefonoID { get; set; }

        public int CorreoID { get; set; }

        public Direccion Direccion { get; set; }
        public Telefono Telefono { get; set; }
        public Correo Correo { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }

        public Persona()
        {
            Nombre = string.Empty;
            Apellido1 = string.Empty;
            Apellido2 = string.Empty;
            Direccion = new Direccion();
            Telefono = new Telefono();
            Correo = new Correo();
            Clientes = new HashSet<Cliente>();
            Usuarios = new HashSet<Usuarios>();
        }
    }
}
