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
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarDirecciones", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Direccion()
                            {
                                DireccionID = Convert.ToInt32(dr["DireccionID"]),
                                NombreDireccion = dr["Direccion"].ToString(),
                                DireccionDetallada = dr["DireccionDetallada"].ToString(),
                                ProvinciaID = Convert.ToInt32(dr["ProvinciaID"]),
                                CantonID = Convert.ToInt32(dr["CantonID"]),
                                DistritoID = Convert.ToInt32(dr["DistritoID"]),
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Provincia = new Provincia() { Descripcion = dr["ProvinciaDescripcion"].ToString() },
                                Canton = new Canton() { Descripcion = dr["CantonDescripcion"].ToString() },
                                Distrito = new Distrito() { Descripcion = dr["DistritoDescripcion"].ToString() }
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

        public int Registrar(Direccion obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDireccion", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Direccion", obj.NombreDireccion); // Change this line to "NombreDireccion"
                    cmd.Parameters.AddWithValue("DireccionDetallada", obj.DireccionDetallada);
                    cmd.Parameters.AddWithValue("ProvinciaID", obj.ProvinciaID);
                    cmd.Parameters.AddWithValue("CantonID", obj.CantonID);
                    cmd.Parameters.AddWithValue("DistritoID", obj.DistritoID);
                    cmd.Parameters.AddWithValue("ClienteID", obj.ClienteID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Editar(Direccion obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarDireccion", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("DireccionID", obj.DireccionID);
                    cmd.Parameters.AddWithValue("Direccion", obj.NombreDireccion);
                    cmd.Parameters.AddWithValue("DireccionDetallada", obj.DireccionDetallada);
                    cmd.Parameters.AddWithValue("ProvinciaID", obj.ProvinciaID);
                    cmd.Parameters.AddWithValue("CantonID", obj.CantonID);
                    cmd.Parameters.AddWithValue("DistritoID", obj.DistritoID);
                    cmd.Parameters.AddWithValue("ClienteID", obj.ClienteID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarDireccion", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("DireccionID", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public List<Direccion> ListarDirecciones(int ClienteID)
        {
            List<Direccion> lista = new List<Direccion>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarDireccionesPorCliente", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClienteID", ClienteID);
                    oConexion.Open();
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
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Provincia = new Provincia() { Descripcion = dr["ProvinciaDescripcion"].ToString() },
                                Canton = new Canton() { Descripcion = dr["CantonDescripcion"].ToString() },
                                Distrito = new Distrito() { Descripcion = dr["DistritoDescripcion"].ToString() }
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
    }
}
