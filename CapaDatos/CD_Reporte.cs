using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<ReporteMontoPorCarga> ObtenerMontosPorTipoDeCarga(int tipoCargaID)
        {
            List<ReporteMontoPorCarga> lista = new List<ReporteMontoPorCarga>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerMontosPorTipoDeCarga", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoCargaID", tipoCargaID);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteMontoPorCarga()
                            {
                                TipoCarga = dr["TipoCarga"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                lista = new List<ReporteMontoPorCarga>();
            }

            return lista;
        }
    }
}
