using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carga
    {
     
        public int CargaID { get; set; }
      
        public decimal Peso { get; set; }
     
        public DateTime FechaEnvio { get; set; }
      
        public string Destino { get; set; }
        
        public int TipoDeCargaID { get; set; }
    
        public int ClienteID { get; set; }
       
        public TiposDeCarga TipoDeCarga { get; set; }
       
        public Clientes Cliente { get; set; }
        public ICollection<Factura> Facturas { get; set; }
    }
}
