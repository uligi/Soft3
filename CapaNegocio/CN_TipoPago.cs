using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_TipoPago
    {
        private CD_TipoPago objCapaDato = new CD_TipoPago();

        public List<TipoPago> Listar()
        {
            return objCapaDato.Listar();
        }

        public bool Registrar(TipoPago obj, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, out Mensaje);
        }

        public bool Editar(TipoPago obj, out string Mensaje)
        {
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
