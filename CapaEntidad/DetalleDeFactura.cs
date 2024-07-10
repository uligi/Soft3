using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleDeFactura
    {
   
        public int DetallesDeFacturaID { get; set; }
        
        public decimal Total { get; set; }
     
        public int FacturaID { get; set; }
     
        public int CargasID { get; set; }
     
        public int UsuarioID { get; set; }
       
        public int ImpuestoID { get; set; }
  
        public int DescuentoID { get; set; }
       
        public DateTime FechaEmision { get; set; }
    
        public Factura Factura { get; set; }
 
        public Carga Carga { get; set; }
        
        public Usuarios Usuario { get; set; }
     
        public Impuesto Impuestos { get; set; }
       
        public Descuento Descuentos { get; set; }
    }
}
