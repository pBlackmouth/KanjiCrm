using System;

namespace Tim.Crm.Base.Entidades.Atributos
{
    // Gris
    /// <summary>
    /// create custom attribute to be assigned to class members
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public class Requerido : System.Attribute
    {
        ///Gris
        /// <summary>
        /// Campo que indica si el atributo es requerido.
        /// </summary>
        public bool _esRequerido = true;
        
        ///Gris
        /// <summary>
        /// Constructor de la propiedad.
        /// </summary>
        public Requerido()
        {
            
        }

        ///Gris.
        /// <summary>
        /// Propiedad de la clase, que indica que el atributo es requerido o no.
        /// </summary>
        public bool EsRequerido { get { return _esRequerido; } }
    }
}
