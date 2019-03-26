using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("new_campussede")]
    public partial class Campus : EntidadCrm
    {

        #region CONSTRUCTORES

        public Campus()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Campus(Entity entidad)
            : base(entidad)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("new_campussedeid")]
        public Guid? CampusID { get; set; }

        [NombreEsquemaCrm("new_name")]
        public String Nombre { get; set; }

        [NombreEsquemaCrm("new_clavecampus")]
        public String Clave { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            CampusID = null;
            Nombre = null;
            Clave = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
