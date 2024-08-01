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
        public int CorreoID { get; set; }
        public Correo Correo { get; set; }

        public List<Telefono> Telefonos { get; set; }
        public List<Direccion> Direcciones { get; set; }
    }
}
