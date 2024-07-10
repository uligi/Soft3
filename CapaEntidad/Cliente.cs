using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {

        public int ClienteID { get; set; }
    
        public int Cedula { get; set; }
   
        public int TipoClienteID { get; set; }
     
        public int PagoID { get; set; }
        
        public Persona Persona { get; set; }
      
        public TipoCliente TipoCliente { get; set; }
       
        public Pago Pago { get; set; }
        public ICollection<Carga> Cargas { get; set; }
        public ICollection<Factura> Facturas { get; set; }

        public Cliente()
        {
            Cargas = new HashSet<Carga>();
            Facturas = new HashSet<Factura>();
        }
    }
}
