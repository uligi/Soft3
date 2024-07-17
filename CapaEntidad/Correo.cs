using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Correo

 {
        public int CorreoID { get; set; }
        public string DireccionCorreo { get; set; }
        public int TipoCorreoID { get; set; }
        public TipoCorreo TipoCorreo { get; set; }
    }
}
