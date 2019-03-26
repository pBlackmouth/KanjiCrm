using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("phonecall")]
    public partial class PhoneCall : EntidadCrm
    {

        #region CONSTRUCTORES

        public PhoneCall()
            : base()
        {
            Inicializar();
        }

        public PhoneCall(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public PhoneCall(Entity entidad)
            : base(entidad)
        {

        }

        public PhoneCall(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("phonecallid")]
        public Guid? PhoneCallID { get; set; }

        [NombreEsquemaCrm("subject")]
        public String Asunto { get; set; }

        [NombreEsquemaCrm("from")]
        [TipoDatoCrm(eTipoDatoCRM.ActivityParty,"")]
        public CrmActivityParty De { get; set; }

        [NombreEsquemaCrm("to")]
        [TipoDatoCrm(eTipoDatoCRM.ActivityParty, "")]
        public CrmActivityParty Para { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            PhoneCallID = null;
            Asunto = null;
            De = new CrmActivityParty();
            Para = new CrmActivityParty(); 
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
            PhoneCall entidad = new PhoneCall()
            {
                PhoneCallID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<PhoneCallID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializePhoneCallID()
        {
            return PhoneCallID.HasValue;
        }

        #endregion

    }
}
