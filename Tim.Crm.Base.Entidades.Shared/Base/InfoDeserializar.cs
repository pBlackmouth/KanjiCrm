using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class InfoDeserializar
    {
        ///Gris
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public InfoDeserializar()
        {
            Completado = true;
            PropiedadFallida = null;
            MensajeError = null;
        }

        ///Gris
        /// <summary>
        /// PRopiedad que indica que la deserialización fue completada o no.
        /// </summary>
        public bool Completado { get; set; }

        ///Gris
        /// <summary>
        /// Propiedad que almacena la propiedad fallada en el proceso de deserialización.
        /// </summary>
        public String PropiedadFallida { get; set; }

        ///Gris
        /// <summary>
        /// Propiedad que almacena el mensaje de error producido en el proceso de deserialización.
        /// </summary>
        public String MensajeError { get; set; }

    }
}
