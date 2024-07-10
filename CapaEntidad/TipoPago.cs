using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TipoPago
    {
 
        public int TipoPagoID { get; set; }
    
        public string Tipo { get; set; }
        public ICollection<Pago> Pagos { get; set; }
    }
}
