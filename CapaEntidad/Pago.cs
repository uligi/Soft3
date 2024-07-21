using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pago
    {

        public int PagoID { get; set; }
    
        public string Descripcion { get; set; }

        public int TipoPagoID { get; set; }

        public int ClienteID { get; set; }

        public TipoPago TipoPago { get; set; }
        public Clientes Clientes { get; set; }

    }
}
