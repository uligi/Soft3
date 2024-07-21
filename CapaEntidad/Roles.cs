using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Roles
    {

        public int RolID { get; set; }
        public string Rol { get; set; }
        public int PermisoID { get; set; }
        public Permisos Permisos { get; set; }

        public String TipoRolDescripcion { get; set; }




    }
}
