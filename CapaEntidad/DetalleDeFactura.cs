using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleDeFactura
    {
        public int DetalleFacturalID { get; set; }
        public int CotizarCargaID { get; set; }
        public string CedulaCliente { get; set; }
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; }
        public string Apellido1Cliente { get; set; }
        public string Apellido2Cliente { get; set; }
        public string CorreoCliente { get; set; }
        public string DireccionDetallada { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DescripcionCarga { get; set; }
        public string tipoDePago { get; set; }

        public int NumeroTelefono { get; set; }

        public string TipoCarga { get; set; }
        public decimal PrecioPorPeso { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotalGravado { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public decimal TotalConDescuento { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal TotalComprobante { get; set; }
        public int TiposDeCargaID { get; set; }
        public int DescuentoID { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public string TipoDescuento { get; set; }
        public int UsuarioID { get; set; }
        public string Representante { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaEmision { get; set; }
        public Usuarios Usuarios { get; set; }
        public CotizarCarga CotizarCarga { get; set; }
        public TiposDeCarga TiposDeCarga { get; set; }
        public Clientes Clientes { get; set; }
        public Descuento Descuento { get; set; }
    }
}
