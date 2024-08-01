using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Direccion
    {

        public int DireccionID { get; set; }
        public string NombreDireccion { get; set; }
        public string DireccionDetallada { get; set; }
        public int ProvinciaID { get; set; }
        public int CantonID { get; set; }
        public int DistritoID { get; set; }
        public int ClienteID { get; set; }

        public Provincia Provincia { get; set; }
        public Canton Canton { get; set; }
        public Distrito Distrito { get; set; }
    }
}
