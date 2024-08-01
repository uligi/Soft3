using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    // Clase Usuarios
    public class Usuarios
    {
        public int UsuarioID { get; set; }
        public string Contrasena { get; set; }
        public bool RestablecerContrasena { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Cedula { get; set; }
        public int RolID { get; set; }
        public Correo Correo { get; set; }
        public Persona Persona { get; set; }
        public Roles Rol { get; set; }

    }



}

