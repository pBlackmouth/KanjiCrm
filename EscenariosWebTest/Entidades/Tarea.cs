using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb._1.Modelos
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("task")]
    public partial class Tarea : EntidadCrm
    {

        #region CONSTRUCTORES

        public Tarea()
            : base()
        {
            Inicializar();
        }

        public Tarea(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Tarea(Entity entidad)
            : base(entidad)
        {

        }

        public Tarea(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("activityid")]
        public Guid? TareaID { get; set; }

        [NombreEsquemaCrm("ownerid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup Propietario { get; set; }

        [NombreEsquemaCrm("subject")]
        public String Asunto { get; set; }

        [NombreEsquemaCrm("activitytypecode")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist TipoActividad { get; set; }

        //TODO: Definir las propiedades todos los tipos de datos deben ser nullables, a excepción de String y Objetos.

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            TareaID = null;
            Propietario = null;
            TipoActividad = null;
            
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
