using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuarios
    {
      
        public int UsuarioID { get; set; }

    
        public string Nombre { get; set; }


        public string Correo { get; set; }


        public string Contrasena { get; set; }

 
        public int RolID { get; set; }

    
        public int Cedula { get; set; }

        public Rol Rol { get; set; }

     
        public Persona Persona { get; set; }
    }
}
