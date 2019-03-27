using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// Clase que ayuda a registrar los errores de las aplicaciones en CRM.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("new_eventosistema")]
    public partial class EventoSistema : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public EventoSistema()
            :base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public EventoSistema(Entity entidad)
            :base(entidad)
        { 
        
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public EventoSistema(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
        //    : base(ObjectoSerializado, ObtenerValoresDesdeString)
        //{

        //}

       

        #endregion

        #region PROPIEDADES
        
        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("new_eventosistemaid")]
        public Guid? EventoSistemaID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("new_name")]
        public String ReferenteA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("new_fecha")]
        public DateTime? Fecha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_articulo")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "kbarticle")]
        public CrmLookup ArticuloBC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_mensajeerror")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_mensajeerror")]
        public CrmLookup MensajeError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_metodo")]
        public String Metodo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_archivo")]
        public String Archivo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_descripcion")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_linkreferente")]
        public String LinkReferente { get; set; }


        

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_timstack")]
        public String TIMStack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_stack")]
        public String Stack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_tipoeventosistema")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist TipoEventoSistema { get; set; }





        

        
        

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            EventoSistemaID = null;
            ReferenteA = null;
            Fecha = null;
            ArticuloBC = null;
            MensajeError = null;
            Metodo = null;
            Archivo = null;
            Descripcion = null;
            LinkReferente = null;
            TIMStack = null;
            Stack = null;
            TipoEventoSistema = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

       

        #endregion

    }
}
