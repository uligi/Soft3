using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CotizarCarga
    {
        public int RegistrarCotizacion(CotizarCarga cotizaCarga, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCotizacion", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Peso", cotizaCarga.Peso);
                    cmd.Parameters.AddWithValue("@Total", cotizaCarga.Total);
                    cmd.Parameters.AddWithValue("@TotalDescuento", cotizaCarga.TotalDescuento);
                    cmd.Parameters.AddWithValue("@TotalImpuesto", cotizaCarga.TotalImpuesto);
                    cmd.Parameters.AddWithValue("@TotalPagar", cotizaCarga.TotalPagar);
                    cmd.Parameters.AddWithValue("@TiposDeCargaID", cotizaCarga.TiposDeCargaID);
                    cmd.Parameters.AddWithValue("@ClienteID", cotizaCarga.ClienteID);
                    cmd.Parameters.AddWithValue("@FacturaID", cotizaCarga.FacturaID);
                    cmd.Parameters.AddWithValue("@DireccionID", cotizaCarga.DireccionID);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
