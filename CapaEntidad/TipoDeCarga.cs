using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TipoDeCarga
    {

        public int TipoDeCargaID { get; set; }
     
        public string Descripcion { get; set; }
     
        public decimal TarifaPorKilo { get; set; }
        public ICollection<Carga> Cargas { get; set; }
    }
}
