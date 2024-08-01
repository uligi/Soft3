using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CotizarImpuesto
    {
        public int CotizarDescuentoID { get; set; }
        public int ImpuestoID { get; set; }
        public int CotizarCargaID { get; set; }
        public Impuesto Impuesto { get; set; }
        public CotizarCarga CotizarCarga { get; set; }
    }
}
