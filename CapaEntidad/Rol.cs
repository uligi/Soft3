using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Rol
    {

        public int RolID { get; set; }
 
        public int TipoRolID { get; set; }
 
  
        public string Descripcion { get; set; }
     
        public TipoRol TipoRol { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
