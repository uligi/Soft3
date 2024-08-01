using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Distrito
    {

        public int DistritoID { get; set; }
        public string Descripcion { get; set; }
        public int CantonID { get; set; }
        public Canton Canton { get; set; }
    }
}
