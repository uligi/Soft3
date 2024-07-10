using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuarios> Listar() {

            List<Usuarios> lista= new List<Usuarios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {

                    string query = "SELECT UsuarioID,Nombre,Correo,Contrasena,RolID,Cedula FROM Usuarios";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(
                                new Usuarios()
                                {
                                    UsuarioID = Convert.ToInt32(rdr["UsuarioID"]),
                                    Nombre = rdr["Nombre"].ToString(),
                                    Correo = rdr["Correo"].ToString(),
                                    Contrasena = rdr["Contrasena"].ToString(),
                                    RolID = Convert.ToInt32(rdr["RolID"]),
                                    Cedula = Convert.ToInt32(rdr["Cedula"])
                                }
                        );
                        }

                    }
                }
            }

            catch { 
                lista = new List<Usuarios>();
            
            
            
            }

            return lista;
        }


        public int RegistrarUsuario(Usuarios usuario, out string Mensaje)
        {
            int idAutogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("CrearUsuario", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    cmd.Parameters.AddWithValue("Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Persona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido1", usuario.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("Apellido2", usuario.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("Direccion", usuario.Persona.Direccion.Descripcion);
                    cmd.Parameters.AddWithValue("DistritoID", usuario.Persona.Direccion.DistritoID);
                    cmd.Parameters.AddWithValue("CantonID", usuario.Persona.Direccion.CantonID);
                    cmd.Parameters.AddWithValue("ProvinciaID", usuario.Persona.Direccion.ProvinciaID);
                    cmd.Parameters.AddWithValue("Telefono", usuario.Persona.Telefono.Numero);
                    cmd.Parameters.AddWithValue("TipoTelefonoID", usuario.Persona.Telefono.TipoTelefonoID);
                    cmd.Parameters.AddWithValue("Correo", usuario.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("TipoCorreoID", usuario.Persona.Correo.TipoCorreoID);
                    cmd.Parameters.AddWithValue("RolID", usuario.RolID);
                    cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);

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






        public int ActualizarUsuario(Usuarios usuario, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("ActualizarUsuario", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios aquí
                    cmd.Parameters.AddWithValue("@Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Persona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", usuario.Persona.Apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", usuario.Persona.Apellido2);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Persona.Direccion.Descripcion);
                    cmd.Parameters.AddWithValue("@DistritoID", usuario.Persona.Direccion.DistritoID);
                    cmd.Parameters.AddWithValue("@CantonID", usuario.Persona.Direccion.CantonID);
                    cmd.Parameters.AddWithValue("@ProvinciaID", usuario.Persona.Direccion.ProvinciaID);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Persona.Telefono.Numero);
                    cmd.Parameters.AddWithValue("@TipoTelefonoID", usuario.Persona.Telefono.TipoTelefonoID);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Persona.Correo.DireccionCorreo);
                    cmd.Parameters.AddWithValue("@TipoCorreoID", usuario.Persona.Correo.TipoCorreoID);
                    cmd.Parameters.AddWithValue("@RolID", usuario.RolID);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

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


        public bool EliminarUsuario(int cedula, out string Mensaje)
        {
            bool exito = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarUsuario", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Cedula", cedula);

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
/*        public bool CambiarClave(int usuarioid,string nuevaClave, out string Mensaje)
        {
            bool exito = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarUsuario", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Cedula", usuarioid);

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
*/
        public bool EsCorreoDuplicado(string correo)
        {
            // Implementación para verificar correos duplicados
            // Ejemplo:
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo", oconexion);
                cmd.Parameters.AddWithValue("@Correo", correo);
                oconexion.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool EsTelefonoDuplicado(string telefono)
        {
            // Implementación para verificar teléfonos duplicados
            // Ejemplo:
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Persona WHERE Telefono = @Telefono", oconexion);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                oconexion.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}



