using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Entidades.Plugins
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("sdkmessageprocessingstep")]
    public partial class SdkMensajeRegistroPaso : EntidadCrm
    {

        #region CONSTRUCTORES

        public SdkMensajeRegistroPaso()
            : base()
        {
            Inicializar();
        }

        public SdkMensajeRegistroPaso(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public SdkMensajeRegistroPaso(Entity entidad)
            : base(entidad)
        {

        }

        //public SdkMensajeRegistroPaso(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
        //    : base(ObjectoSerializado, ObtenerValoresDesdeString)
        //{

        //}

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("sdkmessageprocessingstepid")]
        public Guid? SdkMensajeRegistroPasoID { get; set; }

        [NombreEsquemaCrm("name")]
        public String Nombre { get; set; }

        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        [NombreEsquemaCrm("asyncautodelete")]
        public bool? BorrarAutomaticamente { get; set; }

        [NombreEsquemaCrm("mode")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist ModoEjecucion { get; set; }

        [NombreEsquemaCrm("rank")]
        public int? OrdenEjecucion { get; set; }

        [NombreEsquemaCrm("eventhandler")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "plugintype")]
        public CrmLookup ControladorEventoAsociado { get; set; }

        [NombreEsquemaCrm("sdkmessageid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "sdkmessage")]
        public CrmLookup MensajeSdk { get; set; }

        [NombreEsquemaCrm("stage")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist FaseEjecucion { get; set; }

        [NombreEsquemaCrm("supporteddeployment")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist EjecutarEn { get; set; }

        [NombreEsquemaCrm("sdkmessagefilterid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "sdkmessagefilter")]
        public CrmLookup FiltroMensajeSdk { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            SdkMensajeRegistroPasoID = null;
            Nombre = null;
            Descripcion = null;
            BorrarAutomaticamente = null;
            ModoEjecucion = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion


        #region VISTAS
        //Para definir nuevas vistas, es necesario agregar nuevos métodos que hagan una instancia de la entidad en cuestion.
        //En cada instancia es necesario inicializar cada propiedad con un valor distito de null para que solo dichas propiedades sean devueltas 
        //y después retornar el método AtributosConValor() de la instancia.

        public static string[] SoloID()
        {
            SdkMensajeRegistroPaso entidad = new SdkMensajeRegistroPaso()
            {
                SdkMensajeRegistroPasoID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<SdkMensajeRegistroPasoID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializeSdkMensajeRegistroPasoID()
        {
            return SdkMensajeRegistroPasoID.HasValue;
        }

        #endregion

    }
}
