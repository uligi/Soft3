using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_TipoTelefono
    {
        public List<TipoTelefono> Listar()
        {
            List<TipoTelefono> lista = new List<TipoTelefono>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT TipoTelefonoID, Descripcion FROM TipoTelefono", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new TipoTelefono
                        {
                            TipoTelefonoID = Convert.ToInt32(rdr["TipoTelefonoID"]),
                            Descripcion = rdr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
