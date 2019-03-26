using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;



namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("kbarticle")]
    public class ArticuloBC : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public ArticuloBC()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidad"></param>
        public ArticuloBC(Entity entidad)
            : base(entidad)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public ArticuloBC(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("kbarticleid")]
        public Guid? ArticuloKBID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("title")]
        public String Titulo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("subjectid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "subject")]
        public CrmLookup Asunto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_mensajeerror")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_mensajeerror")]
        public CrmLookup MensajeError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("keywords")]
        public String PalabrasClave { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("languagecode")]
        public int? Idioma { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("articlexml")]
        public String XMLArticulo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("kbarticletemplateid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "kbarticletemplate")]
        public CrmLookup PlantillaArticulo { get; set; }

        

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            ArticuloKBID = null;
            Titulo = null;
            Asunto = null;
            PalabrasClave = null;
        }

        #endregion


    }
}
