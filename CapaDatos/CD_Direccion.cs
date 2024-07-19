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
                    string query = @"SELECT d.DireccionID, d.NombreDireccion, d.DireccionDetallada, 
                                     d.ProvinciaID, p.Descripcion as Provincia, 
                                     d.CantonID, c.Descripcion as Canton, 
                                     d.DistritoID, dis.Descripcion as Distrito 
                                     FROM Direcciones d 
                                     JOIN Provincia p ON d.ProvinciaID = p.ProvinciaID 
                                     JOIN Canton c ON d.CantonID = c.CantonID 
                                     JOIN Distrito dis ON d.DistritoID = dis.DistritoID";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Direccion()
                            {
                                DireccionID = Convert.ToInt32(dr["DireccionID"]),
                                NombreDireccion = dr["NombreDireccion"].ToString(),
                                DireccionDetallada = dr["DireccionDetallada"].ToString(),
                                ProvinciaID = Convert.ToInt32(dr["ProvinciaID"]),
                                CantonID = Convert.ToInt32(dr["CantonID"]),
                                DistritoID = Convert.ToInt32(dr["DistritoID"]),
                                Provincia = new Provincia() { Descripcion = dr["Provincia"].ToString() },
                                Canton = new Canton() { Descripcion = dr["Canton"].ToString() },
                                Distrito = new Distrito() { Descripcion = dr["Distrito"].ToString() }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Direccion>();
            }

            return lista;
        }

        public int Registrar(Direccion direccion, out string mensaje)
        {
            int idAutoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDireccion", oconexion);
                    cmd.Parameters.AddWithValue("NombreDireccion", direccion.NombreDireccion);
                    cmd.Parameters.AddWithValue("DireccionDetallada", direccion.DireccionDetallada);
                    cmd.Parameters.AddWithValue("ProvinciaID", direccion.ProvinciaID);
                    cmd.Parameters.AddWithValue("CantonID", direccion.CantonID);
                    cmd.Parameters.AddWithValue("DistritoID", direccion.DistritoID);
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

        public int Actualizar(Direccion direccion, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarDireccion", oconexion);
                    cmd.Parameters.AddWithValue("DireccionID", direccion.DireccionID);
                    cmd.Parameters.AddWithValue("NombreDireccion", direccion.NombreDireccion);
                    cmd.Parameters.AddWithValue("DireccionDetallada", direccion.DireccionDetallada);
                    cmd.Parameters.AddWithValue("ProvinciaID", direccion.ProvinciaID);
                    cmd.Parameters.AddWithValue("CantonID", direccion.CantonID);
                    cmd.Parameters.AddWithValue("DistritoID", direccion.DistritoID);
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

        public bool Eliminar(int direccionID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarDireccion", oconexion);
                    cmd.Parameters.AddWithValue("DireccionID", direccionID);
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
