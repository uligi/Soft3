using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Canton
    {

        public int CantonID { get; set; }
        public string Descripcion { get; set; }
        public int ProvinciaID { get; set; }
        public bool Activo { get; set; }
        public string ProvinciaDescripcion { get; set; }
        public Provincia Provincia { get; set; }
    }
}