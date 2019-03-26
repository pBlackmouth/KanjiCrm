using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Entidades.Plugins
{
    /// <summary>
    /// El nombre de esquema de esta entidad en CRM es SdkMessage y contiene metadat relacionada con información del mensaje de CRM.
    /// Ej. Create, Update, Delete, Etc...
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("sdkmessage")]
    public partial class SdkMensaje : EntidadCrm
    {

        #region CONSTRUCTORES

        public SdkMensaje()
            : base()
        {
            Inicializar();
        }

        public SdkMensaje(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public SdkMensaje(Entity entidad)
            : base(entidad)
        {

        }

        public SdkMensaje(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("sdkmessageid")]
        public Guid? MensajeSdkID { get; set; }

        [NombreEsquemaCrm("name")]
        public String Nombre { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            MensajeSdkID = null;
            Nombre = null;
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
            SdkMensaje entidad = new SdkMensaje()
            {
                MensajeSdkID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<MensajeSdkID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializeMensajeSdkID()
        {
            return MensajeSdkID.HasValue;
        }

        #endregion

    }
}
