using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Factura
    {
 
        public int FacturasID { get; set; }
      
        public DateTime Fecha { get; set; }
      
        public decimal TotalSinDescuento { get; set; }
      
        public decimal TotalFinal { get; set; }
   
        public int ClienteID { get; set; }
   
        public int CargasID { get; set; }
   
        public Clientes Cliente { get; set; }
     
        public Carga Carga { get; set; }
        public ICollection<DetalleDeFactura> DetallesDeFactura { get; set; }
    }
}
