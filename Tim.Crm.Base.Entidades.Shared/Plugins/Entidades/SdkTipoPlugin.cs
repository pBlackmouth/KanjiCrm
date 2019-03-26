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
    [NombreEsquemaCrm("plugintype")]
    public partial class SdkTipoPlugin : EntidadCrm
    {

        #region CONSTRUCTORES

        public SdkTipoPlugin()
            : base()
        {
            Inicializar();
        }

        public SdkTipoPlugin(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public SdkTipoPlugin(Entity entidad)
            : base(entidad)
        {

        }

        public SdkTipoPlugin(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("plugintypeid")]
        public Guid? SdkTipoPluginID { get; set; }

        [NombreEsquemaCrm("name")]
        public String Nombre { get; set; }

        [NombreEsquemaCrm("pluginassemblyid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "pluginassembly")]
        public CrmLookup LibreriaPlugin { get; set; }

        [NombreEsquemaCrm("typename")]
        public String NombrePlugin { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            SdkTipoPluginID = null;
            //TODO: Inicializar todas las propiedades en NULL.
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
            SdkTipoPlugin entidad = new SdkTipoPlugin()
            {
                SdkTipoPluginID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<SdkTipoPluginID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializeSdkTipoPluginID()
        {
            return SdkTipoPluginID.HasValue;
        }

        #endregion

    }
}
