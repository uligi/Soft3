﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TipoTelefono
    {
      
        public int TipoTelefonoID { get; set; }
    
        public string Descripcion { get; set; }
        public ICollection<Telefono> Telefonos { get; set; }
    }
}
