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
    public class Ignorar : System.Attribute
    {
        /// <summary>
        /// Campo boleano ignorar.
        /// </summary>
        public bool _ignorar = true;

        /// <summary>
        /// Contructor.
        /// </summary>
        public Ignorar()
        {
            
        }

        /// <summary>
        /// Propiedad boleanda que indidca si la propiedad debe ser ignorada.
        /// </summary>
        public bool IgnorarPropiedad { get { return _ignorar; } }
    }



    
  
}
