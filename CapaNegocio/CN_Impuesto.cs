using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Impuesto
    {
        private CD_Impuesto objCapaDato = new CD_Impuesto();

        public List<Impuesto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Impuesto impuesto, out string mensaje)
        {
            mensaje = Validar(impuesto);

            if (!string.IsNullOrEmpty(mensaje))
            {
                return 0; // Indicar que hubo un error en la validación
            }

            return objCapaDato.Registrar(impuesto, out mensaje);
        }

        public bool Editar(Impuesto impuesto, out string mensaje)
        {
            mensaje = Validar(impuesto);

            if (!string.IsNullOrEmpty(mensaje))
            {
                return false; // Indicar que hubo un error en la validación
            }

            return objCapaDato.Editar(impuesto, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }

        private string Validar(Impuesto impuesto)
        {
            if (impuesto.Porcentaje <= 0 || impuesto.Porcentaje > 100)
            {
                return "El porcentaje debe ser un valor entre 0 y 100.";
            }

            if (impuesto.TipoImpuestoID <= 0)
            {
                return "Debe seleccionar un tipo de impuesto válido.";
            }

            return string.Empty; // No hay errores de validación
        }
    }
}
