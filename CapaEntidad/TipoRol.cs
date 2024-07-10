using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TipoRol
    
    {
   
        public int TipoRolID { get; set; }
   
        public string Descripcion { get; set; }
        public ICollection<Rol> Roles { get; set; }
    }
}
