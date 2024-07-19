using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Descuento
    {
        private CD_Descuento objCapaDato = new CD_Descuento();

        public List<Descuento> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Descuento descuento, out string mensaje)
        {
            return objCapaDato.Registrar(descuento, out mensaje);
        }

        public int Actualizar(Descuento descuento, out string mensaje)
        {
            return objCapaDato.Actualizar(descuento, out mensaje);
        }

        public bool Eliminar(int descuentoID, out string mensaje)
        {
            return objCapaDato.Eliminar(descuentoID, out mensaje);
        }
    }
}
