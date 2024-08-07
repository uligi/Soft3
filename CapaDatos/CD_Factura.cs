using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Factura
    {
        public DetalleDeFactura ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            DetalleDeFactura factura = null;

            using (SqlConnection conn = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerDatosFactura", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CotizarCargaID", cotizarCargaID);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        factura = new Factura
                        {
                            CotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]),
                            TipoCarga = dr["TipoCarga"].ToString(),
                            PrecioPorPeso = Convert.ToDecimal(dr["PrecioPorPeso"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Representante = dr["Representante"].ToString()
                        };
                    }
                }
            }

            return factura;
        }
    }
}
