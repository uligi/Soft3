using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Provincia
    {
        private CD_Provincia objCapaDato = new CD_Provincia();

        public List<Provincia> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Provincia obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public int Editar(Provincia obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
