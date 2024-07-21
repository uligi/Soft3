using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Descuento
    {
        public List<Descuento> Listar()
        {
            List<Descuento> lista = new List<Descuento>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT d.DescuentoID, d.Porcentaje, d.MontoMinimo, d.MontoMaximo, d.TipoDescuentoID, td.Descripcion as TipoDescuento FROM Descuento d JOIN TipoDescuento td ON d.TipoDescuentoID = td.TipoDescuentoID";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Descuento()
                            {
                                DescuentoID = Convert.ToInt32(dr["DescuentoID"]),
                                Porcentaje = Convert.ToDecimal(dr["Porcentaje"]),
                                MontoMinimo = Convert.ToDecimal(dr["MontoMinimo"]),
                                MontoMaximo = Convert.ToDecimal(dr["MontoMaximo"]),
                                TipoDescuentoID = Convert.ToInt32(dr["TipoDescuentoID"]),
                                TipoDescuento = new TipoDescuento() { Descripcion = dr["TipoDescuento"].ToString() }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Descuento>();
            }

            return lista;
        }

        public int Registrar(Descuento descuento, out string mensaje)
        {
            int idAutoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDescuento", oconexion);
                    cmd.Parameters.AddWithValue("Porcentaje", descuento.Porcentaje);
                    cmd.Parameters.AddWithValue("MontoMinimo", descuento.MontoMinimo);
                    cmd.Parameters.AddWithValue("MontoMaximo", descuento.MontoMaximo);
                    cmd.Parameters.AddWithValue("TipoDescuentoID", descuento.TipoDescuentoID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                idAutoGenerado = 0;
                mensaje = ex.Message;
            }

            return idAutoGenerado;
        }

        public int Actualizar(Descuento descuento, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarDescuento", oconexion);
                    cmd.Parameters.AddWithValue("DescuentoID", descuento.DescuentoID);
                    cmd.Parameters.AddWithValue("Porcentaje", descuento.Porcentaje);
                    cmd.Parameters.AddWithValue("MontoMinimo", descuento.MontoMinimo);
                    cmd.Parameters.AddWithValue("MontoMaximo", descuento.MontoMaximo);
                    cmd.Parameters.AddWithValue("TipoDescuentoID", descuento.TipoDescuentoID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int descuentoID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarDescuento", oconexion);
                    cmd.Parameters.AddWithValue("DescuentoID", descuentoID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
