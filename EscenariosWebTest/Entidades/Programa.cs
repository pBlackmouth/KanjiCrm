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
    [NombreEsquemaCrm("new_programa")]
    public partial class Programa : EntidadCrm
    {

        #region CONSTRUCTORES

        public Programa()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Programa(Entity entidad)
            : base(entidad)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("new_programaid")]
        public Guid? ProgramaID { get; set; }

        [NombreEsquemaCrm("new_name")]
        public String Nombre { get; set; }

        [NombreEsquemaCrm("new_claveprograma")]
        public String ClavePrograma { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            ProgramaID = null;
            Nombre = null;
            ClavePrograma = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
