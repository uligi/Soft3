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
        public string NombreCliente { get; set; }
        public string Apellido1Cliente { get; set; }
        public string Apellido2Cliente { get; set; }
        public string CorreoCliente { get; set; }
        public string TipoCarga { get; set; }
        public decimal PrecioPorPeso { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotalGravado { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public decimal TotalConDescuento { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal TotalComprobante { get; set; }
        public int UsuarioID { get; set; }
        public string Representante { get; set; }
        public bool Activo { get; set; }
        public Usuarios Usuarios { get; set; }
        public CotizarCarga CotizarCarga { get; set; }
    }
}
