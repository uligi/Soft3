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

        public List<ReporteMontoPorDescuento> ObtenerMontosPorDescuento(int descuentoID)
        {
            List<ReporteMontoPorDescuento> lista = new List<ReporteMontoPorDescuento>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerMontosPorDescuento", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DescuentoID", descuentoID);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteMontoPorDescuento()
                            {
                                Descripcion = dr["Descripcion"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                lista = new List<ReporteMontoPorDescuento>();
            }

            return lista;
        }


        public List<ReporteMontoPorPeriodo> ObtenerMontosPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ReporteMontoPorPeriodo> lista = new List<ReporteMontoPorPeriodo>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerMontosPorPeriodo", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteMontoPorPeriodo()
                            {
                                FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                lista = new List<ReporteMontoPorPeriodo>();
            }

            return lista;
        }
    }
}
