using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Provincia
    {
   
        public int ProvinciaID { get; set; }

   
        public string Descripcion { get; set; }

        public ICollection<Canton> Cantones { get; set; }

        public Provincia()
        {
            Cantones = new HashSet<Canton>();
        }
    }
}
