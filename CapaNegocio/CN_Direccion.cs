using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Direcciones
    {
        private CD_Direccion objCapaDato = new CD_Direccion();

        public List<Direccion> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Direccion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreDireccion) || string.IsNullOrWhiteSpace(obj.NombreDireccion))
            {
                Mensaje = "El nombre de la dirrecion es obligatoria.";
            }
            else if (string.IsNullOrEmpty(obj.DireccionDetallada) || string.IsNullOrWhiteSpace(obj.DireccionDetallada))
            {
                Mensaje = "La dirrecion detallada es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Canton.Descripcion) || string.IsNullOrWhiteSpace(obj.Canton.Descripcion))
            {
                Mensaje = "El canton es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Provincia.Descripcion) || string.IsNullOrWhiteSpace(obj.Provincia.Descripcion))
            {
                Mensaje = "La provincia es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Distrito.Descripcion) || string.IsNullOrWhiteSpace(obj.Distrito.Descripcion))
            {
                Mensaje = "El distrito es obligatorio";                      
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Direccion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreDireccion) || string.IsNullOrWhiteSpace(obj.NombreDireccion))
            {
                Mensaje = "El nombre de la dirrecion es obligatoria.";
            }
            else if (string.IsNullOrEmpty(obj.DireccionDetallada) || string.IsNullOrWhiteSpace(obj.DireccionDetallada))
            {
                Mensaje = "La dirrecion detallada es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Canton.Descripcion) || string.IsNullOrWhiteSpace(obj.Canton.Descripcion))
            {
                Mensaje = "El canton es obligatorio";
            }
            else if (string.IsNullOrEmpty(obj.Provincia.Descripcion) || string.IsNullOrWhiteSpace(obj.Provincia.Descripcion))
            {
                Mensaje = "La provincia es obligatoria";
            }
            else if (string.IsNullOrEmpty(obj.Distrito.Descripcion) || string.IsNullOrWhiteSpace(obj.Distrito.Descripcion))
            {
                Mensaje = "El distrito es obligatorio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public List<Direccion> ListarDirecciones(int ClienteID)
        {
            return objCapaDato.ListarDirecciones(ClienteID);
        }
    }
}
