using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Direccion
    {
        public List<Direccion> Listar()
        {
            List<Direccion> lista = new List<Direccion>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT DireccionID, Descripcion, DistritoID, CantonID, ProvinciaID FROM Direcciones";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(new Direccion()
                            {
                                DireccionID = Convert.ToInt32(rdr["DireccionID"]),
                                Descripcion = rdr["Descripcion"].ToString(),
                                DistritoID = Convert.ToInt32(rdr["DistritoID"]),
                                CantonID = Convert.ToInt32(rdr["CantonID"]),
                                ProvinciaID = Convert.ToInt32(rdr["ProvinciaID"])
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Direccion>();
            }

            return lista;
        }

        public int RegistrarDireccion(Direccion direccion, out string Mensaje)
        {
            int idAutogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("CrearDireccion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Descripcion", direccion.Descripcion);
                    cmd.Parameters.AddWithValue("DistritoID", direccion.DistritoID);
                    cmd.Parameters.AddWithValue("CantonID", direccion.CantonID);
                    cmd.Parameters.AddWithValue("ProvinciaID", direccion.ProvinciaID);

                    SqlParameter resultadoParam = new SqlParameter("Resultado", SqlDbType.Int);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                Mensaje = ex.Message;
            }

            return idAutogenerado;
        }

        public int ActualizarDireccion(Direccion direccion, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("ActualizarDireccion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DireccionID", direccion.DireccionID);
                    cmd.Parameters.AddWithValue("@Descripcion", direccion.Descripcion);
                    cmd.Parameters.AddWithValue("@DistritoID", direccion.DistritoID);
                    cmd.Parameters.AddWithValue("@CantonID", direccion.CantonID);
                    cmd.Parameters.AddWithValue("@ProvinciaID", direccion.ProvinciaID);

                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Bit);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    oconexion.Open();
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

        public bool EliminarDireccion(int direccionID, out string Mensaje)
        {
            bool exito = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarDireccion", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DireccionID", direccionID);

                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Bit);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    mensajeParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mensajeParam);

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    exito = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                exito = false;
                Mensaje = ex.Message;
            }

            return exito;
        }
    }
}
