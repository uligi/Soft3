using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CotizarDescuento
    {
        public int CotizarDescuentoID { get; set; }
        public int DescuentoID { get; set; }
        public int CotizarCargaID { get; set; }
        public Descuento Descuento { get; set; }
        public CotizarCarga CotizarCarga { get; set; }
    }
}
