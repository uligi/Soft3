using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_TipoCorreo
    {
        public List<TipoCorreo> Listar()
        {
            List<TipoCorreo> lista = new List<TipoCorreo>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT TipoCorreoID, Descripcion FROM TipoCorreo", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new TipoCorreo
                        {
                            TipoCorreoID = Convert.ToInt32(rdr["TipoCorreoID"]),
                            Descripcion = rdr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
