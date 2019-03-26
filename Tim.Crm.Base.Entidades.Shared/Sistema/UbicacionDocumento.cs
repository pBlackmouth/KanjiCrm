using System;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("sharepointdocumentlocation")]
    public partial class UbicacionDocumento : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public UbicacionDocumento()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public UbicacionDocumento(Entity entidad)
            : base(entidad)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public UbicacionDocumento(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("sharepointdocumentlocationid")]
        public Guid? UbicacionDocumentoID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("name")]
        public String Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("parentsiteorlocation")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "sharepointdocumentlocation")]
        public CrmLookup SitioPadreLocacion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("absoluteurl")]
        public String URLAbsoluta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("relativeurl")]
        public String URLRelativa { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("regardingobjectid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "contact")]
        public CrmLookup ReferenteA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("ownerid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup Propietario { get; set; }


        


        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            UbicacionDocumentoID = null;
            Nombre = null;
            Descripcion = null;
            SitioPadreLocacion = null;
            URLAbsoluta = null;
            URLRelativa = null;
            ReferenteA = null;
            Propietario = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
