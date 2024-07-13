using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Roles
    {
        public List<Roles> Listar()
        {
            List<Roles> lista = new List<Roles>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT RolID, Rol FROM Rol", oconexion);
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Roles
                        {
                            RolID = Convert.ToInt32(rdr["RolID"]),
                            Rol = rdr["Rol"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
