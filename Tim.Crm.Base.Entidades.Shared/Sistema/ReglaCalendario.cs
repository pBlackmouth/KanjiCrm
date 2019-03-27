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
    [NombreEsquemaCrm("calendarrule")]
    public partial class ReglaCalendario : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public ReglaCalendario()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public ReglaCalendario(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public ReglaCalendario(Entity entidad)
            : base(entidad)
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public ReglaCalendario(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
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
        [NombreEsquemaCrm("calendarruleid")]
        public Guid? ReglaCalendarioID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("organizationid")]
        public Guid? OrganizacionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("issimple")]
        public Boolean? EsSimple { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("duration")]
        public int? Duracion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("offset")]
        public int? Desfase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("calendarid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "calendar")]
        public CrmLookup Calendario { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("timezonecode")]
        public int? CodigoTiempoZona { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("rank")]
        public int? Grado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("timecode")]
        public int? CodigoTiempo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("businessunitid")]
        public Guid? UnidadDeNegocio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("subcode")]
        public int? SubCodigo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("pattern")]
        public string Patron { get; set; }




        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            ReglaCalendarioID = null;
            OrganizacionID = null;
            EsSimple = null;
            Duracion = null;
            Desfase = null;
            Calendario = null;
            CodigoTiempoZona = null;
            Grado = null;
            CodigoTiempo = null;
            UnidadDeNegocio = null;
            SubCodigo = null;
            Patron = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
