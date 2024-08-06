using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Clientes
    {
        public List<Clientes> Listar()
        {
            List<Clientes> lista = new List<Clientes>();
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
                            lista.Add(new Clientes()
                            {
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                TipoCliente = new TipoCliente { Descripcion = dr["TipoCliente"].ToString() },
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
                lista = new List<Clientes>();
            }
            return lista;
        }



        public int Registrar(Clientes obj, out string Mensaje)
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




        public bool Editar(Clientes obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarCliente", oConexion);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarCliente", oConexion);
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

        public bool ActivarDesactivarCliente(int id, bool activo, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActivarDesactivarCliente", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", id);
                    cmd.Parameters.AddWithValue("@Activo", activo);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
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

        public Clientes ObtenerClientePorCedula(int cedula)
        {
            Clientes cliente = null;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarClientesPorCedula", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cliente = new Clientes()
                            {
                                ClienteID = Convert.ToInt32(dr["ClienteID"]),
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                TipoCliente = new TipoCliente { Descripcion = dr["TipoCliente"].ToString() },
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
                            };
                        }

                        if (dr.NextResult())
                        {
                            cliente.Persona.Telefonos = new List<Telefono>();
                            while (dr.Read())
                            {
                                cliente.Persona.Telefonos.Add(new Telefono()
                                {
                                    TelefonoID = Convert.ToInt32(dr["TelefonoID"]),
                                    NumeroTelefono = dr["NumeroTelefono"].ToString(),
                                    TipoTelefono = new TipoTelefono
                                    {
                                        Descripcion = dr["TipoTelefono"].ToString()
                                    }
                                });
                            }
                        }

                        if (dr.NextResult())
                        {
                            cliente.Persona.Direcciones = new List<Direccion>();
                            while (dr.Read())
                            {
                                cliente.Persona.Direcciones.Add(new Direccion()
                                {
                                    DireccionID = Convert.ToInt32(dr["DireccionID"]),
                                    NombreDireccion = dr["NombreDireccion"].ToString(),
                                    DireccionDetallada = dr["DireccionDetallada"].ToString(),
                                    Provincia = new Provincia { Descripcion = dr["Provincia"].ToString() },
                                    Canton = new Canton { Descripcion = dr["Canton"].ToString() },
                                    Distrito = new Distrito { Descripcion = dr["Distrito"].ToString() },
                                    ClienteID = Convert.ToInt32(dr["ClienteID"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cliente = null;
            }
            return cliente;
        }
    }
}
