using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_TipoImpuesto
    {
        public List<TipoImpuesto> Listar()
        {
            List<TipoImpuesto> lista = new List<TipoImpuesto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT TipoImpuestoID, Descripcion FROM TipoImpuesto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TipoImpuesto()
                            {
                                TipoImpuestoID = Convert.ToInt32(dr["TipoImpuestoID"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<TipoImpuesto>();
            }

            return lista;
        }

        public int RegistrarTipoImpuesto(TipoImpuesto tipoImpuesto, out string mensaje)
        {
            int idAutoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTipoImpuesto", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", tipoImpuesto.Descripcion);
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

        public int ActualizarTipoImpuesto(TipoImpuesto tipoImpuesto, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarTipoImpuesto", oconexion);
                    cmd.Parameters.AddWithValue("TipoImpuestoID", tipoImpuesto.TipoImpuestoID);
                    cmd.Parameters.AddWithValue("Descripcion", tipoImpuesto.Descripcion);
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

        public bool EliminarTipoImpuesto(int tipoImpuestoID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarTipoImpuesto", oconexion);
                    cmd.Parameters.AddWithValue("TipoImpuestoID", tipoImpuestoID);
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
