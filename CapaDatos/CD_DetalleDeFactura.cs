using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DetalleDeFactura
    {
        public DetalleDeFactura ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            DetalleDeFactura factura = null;

            using (SqlConnection conn = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDatosFactura", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CotizarCargaID", cotizarCargaID);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        factura = new DetalleDeFactura
                        {
                            CedulaCliente = dr["CedulaCliente"].ToString(),
                            NombreCliente = dr["NombreCliente"].ToString(),
                            Apellido1Cliente = dr["Apellido1Cliente"].ToString(),
                            Apellido2Cliente = dr["Apellido2Cliente"].ToString(),
                            CorreoCliente = dr["CorreoCliente"].ToString(),
                            CotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]),
                            SubTotalGravado = Convert.ToDecimal(dr["SubTotalGravado"]),
                            TotalConDescuento = Convert.ToDecimal(dr["TotalDescuento"]),
                            TotalComprobante = Convert.ToDecimal(dr["TotalComprobante"]),
                            TotalImpuesto = Convert.ToDecimal(dr["TotalImpuesto"]),
                            TipoCarga = dr["TipoCarga"].ToString(),
                            PrecioPorPeso = Convert.ToDecimal(dr["PrecioPorPeso"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                            Representante = dr["Representante"].ToString()
                        };
                    }
                }
            }

            return factura;
        }


    }
}
