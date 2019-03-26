using System;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades.Atributos
{
    //  Gris
    /// <summary>
    ///  create custom attribute to be assigned to class members 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public class TipoDatoCrm : System.Attribute
    {
        ///Gris
        /// <summary>
        /// Campo de la enumeración que indica el tipo de dato de CRM.
        /// </summary>
        public eTipoDatoCRM _nombreTipoDatoCRM;

        /// <summary>
        /// 
        /// </summary>
        public string _nombreReferencia;

        ///Gris
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="TipoDatoCRM">Enumeración del tipo de dato</param>
        /// <param name="Entidad">Nombre de la entidad.</param>
        public TipoDatoCrm(eTipoDatoCRM TipoDatoCRM, string Entidad)
        {
            _nombreTipoDatoCRM = TipoDatoCRM;
            _nombreReferencia = Entidad;

        }

        ///Gris
        /// <summary>
        /// Propiedad que obtiene o establece la enumeración de tipo de dato crm.
        /// </summary>
        public eTipoDatoCRM NombreTipoDatoCRM { get { return _nombreTipoDatoCRM; } set { _nombreTipoDatoCRM = value; } }
        
        ///Gris
        /// <summary>
        /// Propiedad  utilizada pra obtener o establecer la entidad relacionada de CRM.
        /// </summary>
        public string EntidadRelacionadaCRM { get { return _nombreReferencia; } set { _nombreReferencia = value; } }

    }
}
