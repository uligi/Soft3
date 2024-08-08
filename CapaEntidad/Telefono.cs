using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Telefono
    {

        public int TelefonoID { get; set; }
        public string NumeroTelefono { get; set; }
        public int Cedula { get; set; }
        public int TipoTelefonoID { get; set; }
        public string TipoTelefonoDescripcion { get; set; }
        public bool Activo { get; set; }

        public TipoTelefono TipoTelefono { get; set; }
    }
}
