﻿using System;
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
    }
}
