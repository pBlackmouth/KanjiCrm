using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tim.Crm.Base.Entidades.Extension;

namespace PruebasWeb
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("contact")]
    public partial class Prospecto : EntidadCrmExtended
    {

        #region CONSTRUCTORES

        public Prospecto()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Prospecto(Entity entidad)
            : base(entidad)
        {

        }

        public Prospecto(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("contactid")]
        public Guid? ProspectoID { get; set; }




        [NombreEsquemaCrm("firstname")]
        public String Nombres { get; set; }

        [NombreEsquemaCrm("new_apellidopaterno")]
        public String ApellidoPaterno { get; set; }

        [NombreEsquemaCrm("new_apellidomaterno")]
        public String ApellidoMaterno { get; set; }

        [NombreEsquemaCrm("birthdate")]
        public DateTime? FechaNacimiento { get; set; }

        [NombreEsquemaCrm("emailaddress1")]
        public String EMail { get; set; }

        [NombreEsquemaCrm("telephone1")]
        public String Telefono { get; set; }

        [NombreEsquemaCrm("telephone2")]
        public String Celular { get; set; }

        [NombreEsquemaCrm("new_paisnacimiento")]
        public String Nacionalidad { get; set; }



        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_mediodecontacto")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist MedioDeContacto { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_areainteres")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist AreaDeInteres { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_campusindicador")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_campussede")]
        public CrmLookup CampusDeInteres { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_programa")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_programa")]
        public CrmLookup ProgramaDeInteres { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("gendercode")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist Genero { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_actividaddepromocion")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_actividaddepromocion")]
        public CrmLookup ActividadPromocion { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_url")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_referencia")]
        public CrmLookup URL { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_proveedor")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_proveedor")]
        public CrmLookup Proveedor { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("lastname")]
        public String ApPaterno { get; set; }


        [NombreEsquemaCrm("address1_country")]
        public String Pais { get; set; }


        [NombreEsquemaCrm("address1_stateorprovince")]
        public String EstadoProvincia { get; set; }


        [NombreEsquemaCrm("address1_city")]
        public String Ciudad { get; set; }


        [NombreEsquemaCrm("description")]
        public String Comentarios { get; set; }


        [NombreEsquemaCrm("new_custom2")]
        public String Modalidad { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_modalidad")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist ModalidadPL { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_origen")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist Origen { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_plataformaorigen")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist PlataformaOrigen { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_duplicadoconid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "contact")]
        public CrmLookup DuplicadoCon { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("statuscode")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist Estatus { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("createdon")]
        public DateTime? FechaCreacion { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_iddeactividad")]
        public String IdDeActividad { get; set; }

        [NombreEsquemaCrm("new_periodo")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "new_periodo")]
        public CrmLookup Periodo { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [NombreEsquemaCrm("new_mediostradicionales")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist MedioTradicional { get; set; }







        [PropiedadBusqueda]
        public String CampusInteres { get; set; }

        [PropiedadBusqueda]
        public String Programa { get; set; }

        [PropiedadBusqueda]
        public String Sexo { get; set; }

        [PropiedadBusqueda]
        public String IdActividad { get; set; }

        [PropiedadBusqueda]
        public String IdPeriodo { get; set; }


        [PropiedadBusqueda]
        public String MedioContacto { get; set; }

        [PropiedadBusqueda]
        public String AreaInteres { get; set; }

        [PropiedadBusqueda]
        public string MediosTradicionales { get; set; }




        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            ProspectoID = null;
            Nombres = null;
            ApellidoPaterno = null;
            ApellidoMaterno = null;
            FechaNacimiento = null;
            Genero = null;
            ApPaterno = null;
            EMail = null;
            Telefono = null;
            Celular = null;
            Nacionalidad = null;
            CampusDeInteres = null;
            ProgramaDeInteres = null;
            Pais = null;
            EstadoProvincia = null;
            Ciudad = null;
            CampusInteres = null;
            Programa = null;
            Sexo = null;
            Comentarios = null;
            IdActividad = null;
            FechaCreacion = null;
            PlataformaOrigen = null;
            Origen = null;
            Modalidad = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        public static string[] NombreID()
        {
            Prospecto entidad = new Prospecto()
            {
                ProspectoID = Guid.Empty,
                Nombres = ""
            };

            return entidad.AtributosConValor();
        }

        public static string[] SoloFechaCreacion()
        {
            Prospecto entidad = new Prospecto()
            {
                FechaCreacion = DateTime.MinValue
            };

            return entidad.AtributosConValor();
        }

        public bool ShouldSerializeProspectoID()
        {
            return ProspectoID.HasValue;
        }



        #endregion

    }
}
