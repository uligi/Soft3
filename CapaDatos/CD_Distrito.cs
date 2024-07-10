using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Distrito
    {
        public List<Distrito> Listar()
        {
            List<Distrito> lista = new List<Distrito>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT DistritoID, Descripcion, CantonID FROM Distrito", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Distrito
                        {
                            DistritoID = Convert.ToInt32(rdr["DistritoID"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                            CantonID = Convert.ToInt32(rdr["CantonID"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}
