using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDato = new CD_Reporte();

        public List<ReporteMontoPorCarga> ObtenerMontosPorTipoDeCarga(int tipoCargaID)
        {
            return objCapaDato.ObtenerMontosPorTipoDeCarga(tipoCargaID);
        }
        public List<ReporteMontoPorDescuento> ObtenerMontosPorDescuento(int descuentoID)
        {
            return objCapaDato.ObtenerMontosPorDescuento(descuentoID);
        }


        public List<ReporteMontoPorPeriodo> ObtenerMontosPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCapaDato.ObtenerMontosPorPeriodo(fechaInicio, fechaFin);
        }
    }
}
