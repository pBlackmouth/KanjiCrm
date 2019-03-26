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
    [NombreEsquemaCrm("pluginassembly")]
    public partial class SdkEnsambladoPlugin : EntidadCrm
    {

        #region CONSTRUCTORES

        public SdkEnsambladoPlugin()
            : base()
        {
            Inicializar();
        }

        public SdkEnsambladoPlugin(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public SdkEnsambladoPlugin(Entity entidad)
            : base(entidad)
        {

        }

        public SdkEnsambladoPlugin(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("pluginassemblyid")]
        public Guid? SdkEnsambladoPluginID { get; set; }

        [NombreEsquemaCrm("name")]
        public String NombreLibreria { get; set; }

        [NombreEsquemaCrm("path")]
        public String NombreCompletoLibreria { get; set; }

        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        [NombreEsquemaCrm("sourcetype")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist RegistradoEn { get; set; }

        [NombreEsquemaCrm("version")]
        public String Version { get; set; }

        [NombreEsquemaCrm("organizationid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "organization")]
        public CrmLookup Organizacion { get; set; }

        [NombreEsquemaCrm("createdby")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup CreadoPor { get; set; }







        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            SdkEnsambladoPluginID = null;
            NombreLibreria = null;
            NombreCompletoLibreria = null;
            Descripcion = null;
            RegistradoEn = null;
            Version = null;
            Organizacion = null;
            CreadoPor = null;
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
            SdkEnsambladoPlugin entidad = new SdkEnsambladoPlugin()
            {
                SdkEnsambladoPluginID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<SdkEnsambladoPluginID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializeSdkEnsambladoPluginID()
        {
            return SdkEnsambladoPluginID.HasValue;
        }

        #endregion

    }
}
