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
    [NombreEsquemaCrm("subject")]
    public class Tema : EntidadCrm
    {
        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public Tema()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidad"></param>
        public Tema(Entity entidad)
            : base(entidad)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public Tema(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("subjectid")]
        public Guid? TemaID { get; set; }

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
        [NombreEsquemaCrm("parentsubject")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "subject")]
        public CrmLookup TemaPrimario { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("featuremask")]
        public int? Mascara { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            TemaID = null;
            Titulo = null;
            TemaPrimario = null;
            Descripcion = null;
            Mascara = 1;
        }

        #endregion
    }
}
