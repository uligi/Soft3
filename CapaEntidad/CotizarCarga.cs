using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CotizarCarga
    {
        public int CotizarCargaID { get; set; }
        public decimal Peso { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPagar { get; set; }
        public int TiposDeCargaID { get; set; }
        public int ClienteID { get; set; }
        public int DireccionID { get; set; }
        public int DescuentoID { get; set; }
        public bool Activo { get; set; }

        public bool CargaFacturada { get; set; }

        public TiposDeCarga TiposDeCarga { get; set; }
        public Clientes Clientes { get; set; }
        public Direccion Direccion { get; set; }

        public Descuento Descuento { get; set; }

        public Persona Persona { get; set; }

    }
}
