using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("new_alertaerror")]
    public partial class AlertaError : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public AlertaError()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public AlertaError(Entity entidad)
            : base(entidad)
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public AlertaError(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
        //    : base(ObjectoSerializado, ObtenerValoresDesdeString)
        //{

        //}



        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("new_alertaerrorid")]
        public Guid? AlertaErrorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_name")]
        public String Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_usuario")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup Usuario { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_mensajeerror")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_mensajeerror")]
        public CrmLookup MensajeError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_plantilladecorreo")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_plantillacorreoeventosistema")]
        public CrmLookup PlantillaDeCorreo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_activo")]
        public Boolean? Activo { get; set; }

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

            AlertaErrorID = null;
            Nombre = null;
            Usuario = null;
            MensajeError = null;
            PlantillaDeCorreo = null;
            Activo = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
