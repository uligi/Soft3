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

     

        public string Numero { get; set; }

     
        public int TipoTelefonoID { get; set; }


        public TipoTelefono TipoTelefono { get; set; }

        public ICollection<Persona> Personas { get; set; }
    }
}
