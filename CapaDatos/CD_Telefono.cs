using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Telefono
    {
        public List<Telefono> Listar()
        {
            List<Telefono> lista = new List<Telefono>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarTelefono", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Telefono()
                            {
                                TelefonoID = Convert.ToInt32(dr["TelefonoID"]),
                                NumeroTelefono = dr["NumeroTelefono"].ToString(),
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                TipoTelefonoID = Convert.ToInt32(dr["TipoTelefonoID"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Telefono>();
            }
            return lista;
        }
        public List<Telefono> ListarPorCliente(int Cedula)
        {
            List<Telefono> lista = new List<Telefono>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("[sp_ListarTelefonosPorCedula]", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", Cedula);
                    oConexion.Open();
                    System.Diagnostics.Debug.WriteLine("Conexión abierta.");

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            var telefono = new Telefono()
                            {
                                TelefonoID = Convert.ToInt32(dr["TelefonoID"]),
                                NumeroTelefono = dr["NumeroTelefono"].ToString(),
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                TipoTelefonoID = Convert.ToInt32(dr["TipoTelefonoID"]),
                                TipoTelefono = new TipoTelefono()
                                {
                                    Descripcion = dr["TipoTelefono"].ToString()
                                }
                            };
                            System.Diagnostics.Debug.WriteLine($"TeléfonoID: {telefono.TelefonoID}, Número: {telefono.NumeroTelefono}, Tipo: {telefono.TipoTelefono.Descripcion}");
                            lista.Add(telefono);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                lista = new List<Telefono>();
            }
            return lista;
        }


        public int Registrar(Telefono obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTelefono", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NumeroTelefono", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("TipoTelefonoID", obj.TipoTelefonoID);
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


        public int Editar(Telefono obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarTelefono", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("TelefonoID", obj.TelefonoID);
                    cmd.Parameters.AddWithValue("NumeroTelefono", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("TipoTelefonoID", obj.TipoTelefonoID);
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


        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarTelefono", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("TelefonoID", id);
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

        public List<Telefono> ListarPorCedula(int Cedula)
        {
            List<Telefono> lista = new List<Telefono>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarTelefonoPorCedula", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", Cedula);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Telefono()
                            {
                                TelefonoID = Convert.ToInt32(dr["TelefonoID"]),
                                NumeroTelefono = dr["NumeroTelefono"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Telefono>();
            }
            return lista;
        }
    }
}
