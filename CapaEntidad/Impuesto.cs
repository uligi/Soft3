using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Impuesto
    {
        public int ImpuestoID { get; set; }
        public decimal Porcentaje { get; set; }
        public int TipoImpuestoID { get; set; }
        public TipoImpuesto TipoImpuesto { get; set; }
    }
}
