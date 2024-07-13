using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT u.UsuarioID, u.Contrasena, u.RolID, u.Cedula, u.Activo, u.RestablecerContraseña, u.FechaCreacion, " +
                                   "p.Nombre, p.Apellido1, p.Apellido2, c.Correo " +
                                   "FROM Usuarios u " +
                                   "INNER JOIN Persona p ON u.Cedula = p.Cedula " +
                                   "INNER JOIN Correo c ON p.CorreoID = c.CorreoID";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                UsuarioID = Convert.ToInt32(rdr["UsuarioID"]),
                                Contrasena = rdr["Contrasena"].ToString(),
                                RolID = Convert.ToInt32(rdr["RolID"]),
                                Cedula = Convert.ToInt32(rdr["Cedula"]),
                                Activo = Convert.ToBoolean(rdr["Activo"]),
                                RestablecerContraseña = Convert.ToBoolean(rdr["RestablecerContraseña"]),
                                FechaCreacion = Convert.ToDateTime(rdr["FechaCreacion"]),
                                Persona = new Persona()
                                {
                                    Cedula = Convert.ToInt32(rdr["Cedula"]),
                                    Nombre = rdr["Nombre"].ToString(),
                                    Apellido1 = rdr["Apellido1"].ToString(),
                                    Apellido2 = rdr["Apellido2"].ToString(),
                                    Correo = new Correo()
                                    {
                                        DireccionCorreo = rdr["Correo"].ToString()
                                    }
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Usuarios>();
            }

            return lista;
        }

        public int RegistrarUsuario(Usuarios usuario, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("RegistrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Persona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido1", usuario.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("Apellido2", usuario.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("Correo", usuario.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("RolID", usuario.RolID);
                    cmd.Parameters.AddWithValue("Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("RestablecerContraseña", usuario.RestablecerContraseña);
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

        public int ActualizarUsuario(Usuarios usuario, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("ActualizarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("UsuarioID", usuario.UsuarioID);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Persona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido1", usuario.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("Apellido2", usuario.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("Correo", usuario.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("RolID", usuario.RolID);
                    cmd.Parameters.AddWithValue("Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("RestablecerContraseña", usuario.RestablecerContraseña);
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

        public bool EliminarUsuario(int usuarioID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("UsuarioID", usuarioID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
        public bool RestablecerContraseña(int usuarioID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RestablecerContrasena", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UsuarioID", usuarioID);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }

    }
}
