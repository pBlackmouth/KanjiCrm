using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.Atributos
{

    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Field |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public class NombreParaMostrar : System.Attribute
    {

        
        /// <summary>
        /// Campo Nombre para mostrar.
        /// </summary>
        public string _nombreMostrar;

        
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="NombreCrm">Cadena que representa el nombre de esquema de CRM.</param>
        public NombreParaMostrar(string Nombre)
        {
            _nombreMostrar = Nombre;
        }

        ///Gris
        /// <summary>
        /// Propiedad para obtener o establecer el nombre de esquema CRM. 
        /// </summary>
        public string NombreMostrar { get { return _nombreMostrar; } set { _nombreMostrar = value; } }
    }
}
