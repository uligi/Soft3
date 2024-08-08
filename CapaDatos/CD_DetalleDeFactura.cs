using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;

namespace CapaDatos
{
    public class CD_DetalleDeFactura
    {
        public List<DetalleDeFactura> ListarFacturas()
        {
            List<DetalleDeFactura> lista = new List<DetalleDeFactura>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarFacturas", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var detalleFacturaID = Convert.ToInt32(dr["DetalleFacturaID"]);
                            var cotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]);
                            var subTotalGravado = Convert.ToDecimal(dr["SubTotalGravado"]);
                            var fechaEmision = Convert.ToDateTime(dr["FechaEmision"]);
                            var totalSinDescuento = Convert.ToDecimal(dr["TotalSinDescuento"]);
                            var totalConDescuento = Convert.ToDecimal(dr["TotalConDescuento"]);
                            var totalImpuesto = Convert.ToDecimal(dr["TotalImpuesto"]);
                            var totalComprobante = Convert.ToDecimal(dr["TotalComprobante"]);
                            var precioPorPeso = Convert.ToDecimal(dr["PrecioPorPeso"]);
                            var cantidad = Convert.ToInt32(dr["Cantidad"]);
                            var usuarioID = Convert.ToInt32(dr["UsuarioID"]);
                            var tiposDeCargaID = Convert.ToInt32(dr["TiposDeCargaID"]);
                            var nombreCarga = dr["NombreCarga"].ToString();
                            var activo = Convert.ToBoolean(dr["Activo"]);
                            var cedulaCliente = dr["Cedula"].ToString();
                            var nombreCliente = dr["NombreCliente"].ToString();
                            var apellido1Cliente = dr["Apellido1Cliente"].ToString();
                            var apellido2Cliente = dr["Apellido2Cliente"].ToString();
                            var descuentoID = Convert.ToInt32(dr["DescuentoID"]);
                            var porcentajeDescuento = Convert.ToDecimal(dr["PorcentajeDescuento"]);
                            var tipoDescuento = dr["TipoDescuento"].ToString();
                            var representante = dr["Representante"].ToString();

                            lista.Add(new DetalleDeFactura()
                            {
                                DetalleFacturalID = detalleFacturaID,
                                CotizarCargaID = cotizarCargaID,
                                SubTotalGravado = subTotalGravado,
                                FechaEmision = fechaEmision,
                                TotalSinDescuento = totalSinDescuento,
                                TotalConDescuento = totalConDescuento,
                                TotalImpuesto = totalImpuesto,
                                TotalComprobante = totalComprobante,
                                PrecioPorPeso = precioPorPeso,
                                Cantidad = cantidad,
                                UsuarioID = usuarioID,
                                TiposDeCargaID = tiposDeCargaID,
                                TipoCarga = nombreCarga,
                                Activo = activo,
                                CedulaCliente = cedulaCliente,
                                NombreCliente = nombreCliente,
                                Apellido1Cliente = apellido1Cliente,
                                Apellido2Cliente = apellido2Cliente,
                                DescuentoID = descuentoID,
                                PorcentajeDescuento = porcentajeDescuento,
                                TipoDescuento = tipoDescuento,
                                Representante = representante
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.Message);
                lista = new List<DetalleDeFactura>();
            }
            return lista;
        }



        public DetalleDeFactura ListarCargasSeleccionadas(int cotizarCargaID, int usuarioID)
        {
            DetalleDeFactura factura = null;

            using (SqlConnection conn = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDatosFactura", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CotizarCargaID", cotizarCargaID);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        factura = new DetalleDeFactura
                        {
                            CedulaCliente = dr["CedulaCliente"].ToString(),
                            ClienteID = Convert.ToInt32(dr["ClienteID"]),
                            NombreCliente = dr["NombreCliente"].ToString(),
                            Apellido1Cliente = dr["Apellido1Cliente"].ToString(),
                            Apellido2Cliente = dr["Apellido2Cliente"].ToString(),
                            CorreoCliente = dr["CorreoCliente"].ToString(),
                            CotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]),
                            SubTotalGravado = Convert.ToDecimal(dr["SubTotalGravado"]),
                            TotalConDescuento = Convert.ToDecimal(dr["TotalDescuento"]),
                            TotalComprobante = Convert.ToDecimal(dr["TotalComprobante"]),
                            TotalImpuesto = Convert.ToDecimal(dr["TotalImpuesto"]),
                            TipoCarga = dr["TipoCarga"].ToString(),
                            PrecioPorPeso = Convert.ToDecimal(dr["PrecioPorPeso"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                            Representante = dr["Representante"].ToString()
                        };
                    }
                }
            }

            return factura;
        }

        
            public int RegistrarFactura(DetalleDeFactura factura, out string mensaje)
            {
                int resultado = 0;
                mensaje = string.Empty;
                try
                {
                    using (SqlConnection oConexion = new SqlConnection(Conexion.conexion))
                    {
                        SqlCommand cmd = new SqlCommand("sp_RegistrarFactura", oConexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CotizarCargaID", factura.CotizarCargaID);
                        cmd.Parameters.AddWithValue("@UsuarioID", factura.UsuarioID);
                        cmd.Parameters.AddWithValue("@PrecioPorPeso", factura.PrecioPorPeso);
                        cmd.Parameters.AddWithValue("@Cantidad", factura.Cantidad);
                        cmd.Parameters.AddWithValue("@SubTotalGravado", factura.SubTotalGravado);
                        cmd.Parameters.AddWithValue("@TotalSinDescuento", factura.TotalSinDescuento);
                        cmd.Parameters.AddWithValue("@TotalConDescuento", factura.TotalConDescuento);
                        cmd.Parameters.AddWithValue("@TotalImpuesto", factura.TotalImpuesto);
                        cmd.Parameters.AddWithValue("@TotalComprobante", factura.TotalComprobante);
                        // No es necesario agregar el parámetro 'Activo', ya que siempre será 1 en el procedimiento almacenado.

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

        public DetalleDeFactura ObtenerFacturaPorID(int id)
        {
            DetalleDeFactura factura = null;

            using (SqlConnection conn = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerFacturaPorID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DetalleFacturaID", id);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        factura = new DetalleDeFactura
                        {
                            DetalleFacturalID = Convert.ToInt32(dr["DetalleFacturaID"]),
                            CotizarCargaID = Convert.ToInt32(dr["CotizarCargaID"]),
                            SubTotalGravado = Convert.ToDecimal(dr["SubTotalGravado"]),
                            FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                            TotalSinDescuento = Convert.ToDecimal(dr["TotalSinDescuento"]),
                            TotalConDescuento = Convert.ToDecimal(dr["TotalConDescuento"]),
                            TotalImpuesto = Convert.ToDecimal(dr["TotalImpuesto"]),
                            TotalComprobante = Convert.ToDecimal(dr["TotalComprobante"]),
                            PrecioPorPeso = Convert.ToDecimal(dr["PrecioPorPeso"]),
                            tipoDePago = dr["TipoPago"].ToString(),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                            TiposDeCargaID = Convert.ToInt32(dr["TiposDeCargaID"]),
                            DescripcionCarga = dr["DescripcionCarga"].ToString(),
                            TipoCarga = dr["NombreCarga"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"]),
                            CedulaCliente = dr["Cedula"].ToString(),
                            NombreCliente = dr["NombreCliente"].ToString(),
                            Apellido1Cliente = dr["Apellido1Cliente"].ToString(),
                            Apellido2Cliente = dr["Apellido2Cliente"].ToString(),
                            CorreoCliente = dr["Correo"].ToString(),
                            DireccionDetallada = dr["DireccionDetallada"].ToString(),
                            Provincia = dr["Provincia"].ToString(),
                            Canton = dr["Canton"].ToString(),
                            Distrito = dr["Distrito"].ToString(),
                            NumeroTelefono = Convert.ToInt32(dr["NumeroTelefono"]),
                            DescuentoID = Convert.ToInt32(dr["DescuentoID"]),
                            PorcentajeDescuento = Convert.ToDecimal(dr["PorcentajeDescuento"]),
                            TipoDescuento = dr["TipoDescuento"].ToString(),
                            Representante = dr["Representante"].ToString()
                        };
                    }
                }
            }

            return factura;
        }



    }
}
