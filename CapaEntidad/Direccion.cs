using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Direccion
    {
   
        public int DireccionID { get; set; }

       
        public string Descripcion { get; set; }

      
        public int DistritoID { get; set; }

      
        public int CantonID { get; set; }

    
        public int ProvinciaID { get; set; }

        
        public Distrito Distrito { get; set; }

      
        public Canton Canton { get; set; }

   
        public Provincia Provincia { get; set; }

        public ICollection<Persona> Personas { get; set; }

        public Direccion()
        {
            Descripcion = string.Empty;
            Distrito = new Distrito();
            Canton = new Canton();
            Provincia = new Provincia();
            Personas = new HashSet<Persona>();
        }
    }
}
