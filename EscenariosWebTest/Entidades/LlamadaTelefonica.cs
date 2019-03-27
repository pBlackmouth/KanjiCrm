using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;
using System.Xml.Serialization;
using Tim.Crm.Base.Entidades.Extension;

namespace PruebasWeb
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("Nombre_de_esquema")]
    public partial class LlamadaTelefonica : EntidadCrmExtended
    {

        #region CONSTRUCTORES

        public LlamadaTelefonica()
            : base()
        {
            Inicializar();
        }

        public LlamadaTelefonica(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public LlamadaTelefonica(Entity entidad)
            : base(entidad)
        {

        }

        public LlamadaTelefonica(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("phonecallid")]
        public Guid? LlamadaTelefonicaID { get; set; }





        [NombreEsquemaCrm("phonenumber")]
        public String NumeroMarcado { get; set; }

        [NombreEsquemaCrm("directioncode")]
        public bool? Direccion { get; set; }

        [NombreEsquemaCrm("description")]
        public String Comentarios { get; set; }

        [NombreEsquemaCrm("scheduledstart")]
        public DateTime? FechaInicio { get; set; }

        [NombreEsquemaCrm("actualend")]
        public DateTime? FechaFin { get; set; }

        [NombreEsquemaCrm("scheduledend")]
        public DateTime? Vencimiento { get; set; }

        [PropiedadBusqueda]
        public string Propietario { get; set; }

        [PropiedadBusqueda]
        public string TipoLlamada { get; set; }

        [PropiedadBusqueda]
        public string Motivo { get; set; }

        [PropiedadBusqueda]
        public string Resultado { get; set; }


        [XmlIgnore]
        [NombreEsquemaCrm("ownerid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup PropietarioLK { get; set; }

        [XmlIgnore]
        [NombreEsquemaCrm("urrea_tipodellamada")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist TipoDeLlamadaPK { get; set; }

        [XmlIgnore]
        [NombreEsquemaCrm("urrea_motivo")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist MotivoPK { get; set; }

        [XmlIgnore]
        [NombreEsquemaCrm("urrea_resultado")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist ResultadoPK { get; set; }
        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            LlamadaTelefonicaID = null;
            PropietarioLK = null;
            NumeroMarcado = null;
            Direccion = null;
            TipoDeLlamadaPK = null;
            MotivoPK = null;
            ResultadoPK = null;
            Comentarios = null;
            FechaInicio = null;
            FechaFin = null;
            Vencimiento = null;

            Propietario = null;
            TipoLlamada = null;
            Motivo = null;
            Resultado = null;
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
            LlamadaTelefonica entidad = new LlamadaTelefonica()
            {
                LlamadaTelefonicaID = Guid.Empty
            };

            return entidad.AtributosConValor();
        }

        //TODO: Definición de otras vistas.

        #endregion

        #region OPCIONES DE SERIALIZACION
        //El método ShouldSerialize se aplica a cada propiedad de la clase que se quiera omitir en el XML resultante,
        //es decir, cada vez que se serializa la propiedad y este tiene un valor null se serializar de la siguiente forma
        //<LlamadaTelefonicaID xsi:nil="true" />, si esto se quiere evitar y que solo se incluya en el XML resultante
        //solo cada propiedad que contenga valor, se debe de crear un método para cada propiedad precedido por ShouldSerialize
        //como el método siguiente:

        public bool ShouldSerializeLlamadaTelefonicaID()
        {
            return LlamadaTelefonicaID.HasValue;
        }

        public bool ShouldSerializeFechaInicio()
        {
            return FechaInicio.HasValue;
        }

        public bool ShouldSerializeFechaFin()
        {
            return FechaFin.HasValue;
        }

        public bool ShouldSerializeVencimiento()
        {
            return Vencimiento.HasValue;
        }

        #endregion


    }
}
