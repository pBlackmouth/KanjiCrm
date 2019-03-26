using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("new_mensajeerror")]
    public partial class MensajeError : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public MensajeError()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public MensajeError(Entity entidad)
            : base(entidad)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public MensajeError(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
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
        [NombreEsquemaCrm("new_mensajeerrorid")]
        public Guid? MensajeErrorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("new_name")]
        public String CodigoError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("new_asunto")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "subject")]
        public CrmLookup Asunto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_descripcion")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_enviarnotificacion")]
        public Boolean? EnviarNotificacion { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            MensajeErrorID = null;
            CodigoError = null;
            Asunto = null;
            Descripcion = null;
            EnviarNotificacion = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        #endregion

    }
}
