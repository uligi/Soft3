using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TiposDeCarga
    {

        public int TiposDeCargaID { get; set; }
        public string Nombre { get; set; }

        public int Stock { get; set; }
        public string Descripcion { get; set; }
     
        public decimal TarifaPorKilo { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
