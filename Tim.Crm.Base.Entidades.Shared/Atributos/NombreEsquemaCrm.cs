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
    public class NombreEsquemaCrm : System.Attribute
    {
        ///Gris
        /// <summary>
        /// Campo Nombre de esquema.
        /// </summary>
        public string _nombreEsquema;

        ///Gris
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="NombreCrm">Cadena que representa el nombre de esquema de CRM.</param>
        public NombreEsquemaCrm(string NombreCrm)
        {
            _nombreEsquema = NombreCrm;
        }

        ///Gris
        /// <summary>
        /// Propiedad para obtener o establecer el nombre de esquema CRM. 
        /// </summary>
        public string NombreEsquema { get { return _nombreEsquema; } set { _nombreEsquema = value; } }


    }
}