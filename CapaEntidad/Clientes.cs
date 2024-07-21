using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Clientes
    {

        public int ClienteID { get; set; }
    
        public int Cedula { get; set; }
   
        public int TipoClienteID { get; set; }
     
        public int PagoID { get; set; }
        public bool Activo { get; set; }

        public int DireccionID { get; set; }

        public DateTime Fecha { get; set; }

        public Persona Persona { get; set; }

        public Pago Pago { get; set; }

        public Direccion Direccion { get; set; }

        public TipoCliente TipoCliente { get; set; }
       
        
 
    }
}
