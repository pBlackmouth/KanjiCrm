using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades.Atributos
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ControlWebFormulario : System.Attribute
    {
        
        /// <summary>
        /// Campo de la enumeración que indica el tipo de dato de CRM.
        /// </summary>
        public eTipoControlWeb _nombreTipoControlWeb;

       
      
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="Tipo">Enumeración del tipo de control web</param>
        public ControlWebFormulario(eTipoControlWeb Tipo)
        {
            _nombreTipoControlWeb = Tipo;
           

        }

       
        /// <summary>
        /// Propiedad que obtiene o establece la enumeración de tipo de control web.
        /// </summary>
        public eTipoControlWeb NombreTipoControlWeb { get { return _nombreTipoControlWeb; } set { _nombreTipoControlWeb = value; } }
        
        
    }
}