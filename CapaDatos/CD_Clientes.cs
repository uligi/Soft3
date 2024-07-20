using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Clientes
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarClientes", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                TipoCliente = new TipoCliente
                                {
                                    Descripcion = dr["TipoCloente"].ToString()
                                },
                                Persona = new Persona
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido1 = dr["Apellido1"].ToString(),
                                    Apellido2 = dr["Apellido2"].ToString(),
                                    Correo = new Correo
                                    {
                                        DireccionCorreo = dr["Correo"].ToString()
                                    }
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Cliente>();
            }
            return lista;
        }


        public int Registrar(Cliente obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Persona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", obj.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", obj.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("@Correo", obj.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("@TipoCorreoID", obj.Persona.Correo.TipoCorreoID);
                    cmd.Parameters.AddWithValue("@TipoClienteID", obj.TipoClienteID);
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



        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("@ClienteID", obj.ClienteID);
                    cmd.Parameters.AddWithValue("@Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Persona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", obj.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", obj.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("@Correo", obj.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("@TipoCorreoID", obj.Persona.Correo.TipoCorreoID);
                    cmd.Parameters.AddWithValue("@TipoClienteID", obj.TipoClienteID);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarClientev", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", id);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool DesactivarCliente(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_DesactivarCliente", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", id);
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

        
    }
}
