using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Descuento
    {
        public int DescuentoID { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public int TipoDescuentoID { get; set; }
        public TipoDescuento TipoDescuento { get; set; }
    }
}
