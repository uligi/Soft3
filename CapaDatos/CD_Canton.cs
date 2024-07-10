using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Canton
    {
        public List<Canton> Listar()
        {
            List<Canton> lista = new List<Canton>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT CantonID, Descripcion, ProvinciaID FROM Canton", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Canton
                        {
                            CantonID = Convert.ToInt32(rdr["CantonID"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                            ProvinciaID = Convert.ToInt32(rdr["ProvinciaID"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}
