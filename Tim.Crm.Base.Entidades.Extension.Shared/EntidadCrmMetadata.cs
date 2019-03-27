using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Atributos;

namespace Tim.Crm.Base.Entidades.Extension
{
    [Serializable]
    [MetadataType(typeof(EntidadCrmExtended))]
    public class EntidadCrmExtended : EntidadCrm
    {
          
        /// <summary>
        /// Propiedad para almacenar el estado de la deserialización.
        /// </summary>
        [JsonIgnore]
        public override InfoDeserializar Deserializacion { get; set; }
                
        /// <summary>
        /// Propiedad que obtiene o establece  el identificador en CRM
        /// </summary>
        [JsonIgnore]
        public override Guid? ID
        {
            get { return _id; }
            set
            {
                PropertyInfo[] lista = this.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(IdentificadorCRM), false))
                .ToArray();

                if (lista.Length == 1)
                    lista[0].SetValue(this, value);

                _id = value;
            }
        }

        /// <summary>
        /// Define el nombre de esquema CRM para esta instancia.
        /// </summary>
        [JsonIgnore]
        public override string NombreEsquema { get; set; }


        /// <summary>
        /// Delegado que define la vista a ser consultada.
        /// </summary> 
        [JsonIgnore]
        public override ColumnasDeMetodo VistaAConsultar { get; set; }

        /// <summary>
        /// Propiedad que define si la consulta actual es una consulta con operador OR
        /// un valor false representa una operador AND.
        /// Valor por Default: false;
        /// </summary>
        [JsonIgnore]
        public override bool OperadorOR { get; set; }

        /// <summary>
        /// Indica que las propiedades definidas como requeridas, 
        /// seran verificadas antes de tratar de ser guradadas en CRM.
        /// Valor por Defult: true
        /// </summary>
        [JsonIgnore]
        public override bool ValidarAtributosRequeridos { get; set; }

        /// <summary>
        /// Define las propiedades por las que será ordenada la consulta.
        /// Ej. "Nombre, Apellido:desc" donde Nombre se indica una ordenación
        /// asc por defecto.
        /// </summary>
        [JsonIgnore]
        public override string OrdenarPor { get; set; }

        //TODO: Definir propiedades para el uso de paginación.

        /// <summary>
        /// Propiedad utilizada para definir los registros por página.
        /// </summary>
        [JsonIgnore]
        public override int RegistrosPorPagina { get; set; }


        [JsonIgnore]
        public override List<EntidadRelacionada> EntidadesRelacionadas { get; set; }


      
        ///Gris
        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public EntidadCrmExtended()
        {
            Inicializar();
        }

        public EntidadCrmExtended(Guid ID)
        {
            Inicializar();
            this.ID = ID;

        }

        ///Gris
        /// <summary>
        /// Constructor que recibe un Entity.
        /// </summary>
        /// <param name="entidad"></param>
        public EntidadCrmExtended(Entity entidad)
        {
            Inicializar();
            EntidadTIM(entidad);
        }

        /// <summary>
        /// Constructor que recibe un objeto serializado y un boleano que indica si los valores se deben de obetener desde el  objeto string serializado.
        /// </summary>
        /// <param name="ObjetoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public EntidadCrmExtended(String ObjetoSerializado, bool ObtenerValoresDesdeString = false)
        {
            Inicializar();

            if (ObtenerValoresDesdeString)
            {
                ObtenerNodosXML(ObjetoSerializado);
            }
            else
            {
                DeserializarObjeto(ObjetoSerializado);
            }
        }

        /// <summary>
        /// Vista que obtiene todos los nombres de esquema de todos los atributos de la clase.
        /// </summary>
        /// <returns></returns>
        protected new string[] VistaPorDefecto()
        {
            if (this.informacionPropiedad != null && this.informacionPropiedad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.informacionPropiedad == null ? null : this.informacionPropiedad.Atributos();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CadenaSerializada"></param>
        private void DeserializarObjeto(string CadenaSerializada)
        {

            if (this.informacionPropiedad != null && this.informacionPropiedad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            object objetoDeserializado = null;

            try
            {

                if (IsJson(CadenaSerializada))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    objetoDeserializado = js.Deserialize(CadenaSerializada, TipoEntidadActualTIM);
                }
                else
                {
                    /****************************************************************************************
                    //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
                    //Usando System.IO
                    XmlSerializer deserializer = new XmlSerializer(TipoEntidadActualTIM);
                    CadenaSerializada = ConvertirCaracteresPermitidos(CadenaSerializada); 

                    using (TextReader textReader = new StringReader(CadenaSerializada))
                    {
                        objetoDeserializado = deserializer.Deserialize(textReader);
                    }
                    ****************************************************************************************/

                    XmlSerializer deserializer = new XmlSerializer(TipoEntidadActualTIM);
                    CadenaSerializada = ConvertirCaracteresPermitidos(CadenaSerializada);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(CadenaSerializada);

                    CadenaSerializada = JsonConvert.SerializeObject(xmlDoc);
                    CadenaSerializada = CadenaSerializada.Substring(CadenaSerializada.IndexOf(':') + 1);
                    CadenaSerializada = CadenaSerializada.Substring(0, CadenaSerializada.LastIndexOf('}'));

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    objetoDeserializado = js.Deserialize(CadenaSerializada, TipoEntidadActualTIM);
                }

                if (objetoDeserializado != null)
                {
                    if (this.informacionPropiedad.PropiedadesTIM != null)
                    {
                        //Recorre las propiedades de la intancia de clase actual.
                        foreach (InfoPropiedad propiedadActualObjDes in this.informacionPropiedad.PropiedadesTIM)
                        {
                            if (!propiedadActualObjDes.IgnorarPropiedad || propiedadActualObjDes.EsPropiedadBusqueda)
                            {
                                propiedadActualObjDes.Propiedad.SetValue(this, propiedadActualObjDes.Propiedad.GetValue(objetoDeserializado), null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CrearErrorDeserializacion(ex, CadenaSerializada);
            }


        }
    }
}
