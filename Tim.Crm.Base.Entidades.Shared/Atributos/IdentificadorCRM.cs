using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.Atributos
{
    // create custom attribute to be assigned to class members
    /// <summary>
    /// create custom attribute to be assigned to class members
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = false)]
    public class IdentificadorCRM : System.Attribute
    {
        ///Gris
        /// <summary>
        /// Variable pública que indica si es identificador único.
        /// </summary>
         public bool _esID = true;
         

        ///Gris
        /// <summary>
        /// Constructor principal
        /// </summary>
        public IdentificadorCRM()
        {
            
        }


        ///Gris
        /// <summary>
        /// Propiedad que indica si es identificador único de CRM, esta propiedad es utilizada para poder determinar el tipo de campo a Mapear.
        /// </summary>
         public bool EsIDPrincipal { get { return _esID; } }
    }
}
