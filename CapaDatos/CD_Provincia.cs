using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Provincia
    {
        public List<Provincia> Listar()
        {
            List<Provincia> lista = new List<Provincia>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProvinciaID, Descripcion FROM Provincia", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Provincia
                        {
                            ProvinciaID = Convert.ToInt32(rdr["ProvinciaID"]),
                            Descripcion = rdr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
