using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CotizarCarga
    {

        public List<CotizarCarga> Listar()
        {
            List<CotizarCarga> lista = new List<CotizarCarga>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarCotizacionesCarga", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            int cotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]);
                            int cedula = Convert.ToInt32(dr["Cedula"]);
                            string nombre = dr["Nombre"].ToString();
                            string apellido1 = dr["Apellido1"].ToString();
                            string apellido2 = dr["Apellido2"].ToString();
                            string correo = dr["Correo"].ToString();
                            string tipoCarga = dr["TipoCarga"].ToString();
                            decimal totalPagar = Convert.ToDecimal(dr["TotalPagar"]);
                            DateTime fecha = Convert.ToDateTime(dr["Fecha"]);

                            lista.Add(new CotizarCarga()
                            {
                                CotizarCargaID = cotizarCargaID,
                                Clientes = new Clientes
                                {
                                    Persona = new Persona
                                    {
                                        Cedula = cedula,
                                        Nombre = nombre,
                                        Apellido1 = apellido1,
                                        Apellido2 = apellido2,
                                        Correo = new Correo
                                        {
                                            DireccionCorreo = correo
                                        }
                                    }
                                },
                                TiposDeCarga = new TiposDeCarga
                                {
                                    Nombre = tipoCarga,
                                },
                                TotalPagar = totalPagar,
                                Fecha = fecha
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                lista = new List<CotizarCarga>();
            }
            return lista;
        }

        public int RegistrarCotizacion(CotizarCarga cotizacion, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCotizacionCarga", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", cotizacion.Clientes.Persona.Cedula);
                    cmd.Parameters.AddWithValue("@Peso", cotizacion.Peso);
                    cmd.Parameters.AddWithValue("@TiposDeCargaID", cotizacion.TiposDeCargaID);
                    cmd.Parameters.AddWithValue("@DireccionID", cotizacion.DireccionID);
                    cmd.Parameters.AddWithValue("@Total", cotizacion.Total);
                    cmd.Parameters.AddWithValue("@TotalDescuento", cotizacion.TotalDescuento);
                    cmd.Parameters.AddWithValue("@TotalImpuesto", cotizacion.TotalImpuesto);
                    cmd.Parameters.AddWithValue("@TotalPagar", cotizacion.TotalPagar);

                    
                    int descuentoID = cotizacion.DescuentoID; 
                    if (descuentoID != 0) 
                    {
                        cmd.Parameters.AddWithValue("@DescuentoID", descuentoID);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DescuentoID", DBNull.Value);
                    }

                    oConexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = 0;
            }
            return resultado;
        }



        public bool ActualizarCotizacion(CotizarCarga cotizacion, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarCotizacionCarga", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CotizaCargaID", cotizacion.CotizarCargaID);
                    cmd.Parameters.AddWithValue("@Cedula", cotizacion.Clientes.Persona.Cedula);
                    cmd.Parameters.AddWithValue("@Peso", cotizacion.Peso);
                    cmd.Parameters.AddWithValue("@TiposDeCargaID", cotizacion.TiposDeCargaID);
                    cmd.Parameters.AddWithValue("@DireccionID", cotizacion.DireccionID);
                    cmd.Parameters.AddWithValue("@Total", cotizacion.Total);
                    cmd.Parameters.AddWithValue("@TotalDescuento", cotizacion.TotalDescuento);
                    cmd.Parameters.AddWithValue("@TotalImpuesto", cotizacion.TotalImpuesto);
                    cmd.Parameters.AddWithValue("@TotalPagar", cotizacion.TotalPagar);
                    cmd.Parameters.AddWithValue("@DescuentoID", cotizacion.DescuentoID);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                    mensaje = "Cotización actualizada correctamente.";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return resultado;
        }


        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCotizacionCarga", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CotizaCargaID", id);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                    mensaje = "Cotización eliminada correctamente.";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return resultado;
        }



        public CotizarCarga ObtenerCotizacionPorID(int id)
        {
            CotizarCarga cotizacion = null;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerCotizacionPorID", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CotizaCargaID", id);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cotizacion = new CotizarCarga()
                            {
                                CotizarCargaID = Convert.ToInt32(dr["CotizaCargaID"]),
                                Clientes = new Clientes
                                {
                                    Persona = new Persona
                                    {
                                        Cedula = Convert.ToInt32(dr["Cedula"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido1 = dr["Apellido1"].ToString(),
                                        Apellido2 = dr["Apellido2"].ToString(),
                                        Correo = new Correo
                                        {
                                            DireccionCorreo = dr["Correo"].ToString()
                                        }
                                    }
                                },
                                TiposDeCarga = new TiposDeCarga
                                {
                                    TiposDeCargaID = Convert.ToInt32(dr["TipoCarga"]),
                                },

                                Fecha = Convert.ToDateTime(dr["Fecha"]),

                                Direccion = new Direccion
                                {
                                    NombreDireccion = dr["NombreDireccion"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
            return cotizacion;
        }


    }
}
