using System;
using System.Collections.Generic;
using System.Reflection;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EntidadCrm: Entity, IRegistroError
    {
        #region VARIABLES Y PROPIEDADES

        ///Gris
        /// <summary>
        /// Propiedad que almacena la información de las propiedads de la clase entidad
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        private InfoEntidad infoEntidad = null;

        ///Gris
        /// <summary>
        /// Propiedad utilizada para acceder a la clase de registro de  errores.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        private RegistroError registrar = null;

        ///Gris
        /// <summary>
        /// Propiedad para almacenar el estado de la deserialización.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public InfoDeserializar Deserializacion = null;





        ///Gris
        /// <summary>
        /// Tipo de entidad de la instancia actual
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        private Type TipoEntidadActualTIM;


        ///Gris
        /// <summary>
        /// Campo privado identificador.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        private Guid? _id = null;


        ///Gris
        /// <summary>
        /// Propiedad que obtiene o establece  el identificador en CRM
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public Guid? ID
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


        ///Gris
        /// <summary>
        /// Define el nombre de esquema CRM para esta instancia.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public string NombreEsquema { get; set; }


        /// <summary>
        /// Delegado que define la vista a ser consultada.
        /// </summary> 
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public ColumnasDeMetodo VistaAConsultar;

        /// <summary>
        /// Propiedad que define si la consulta actual es una consulta con operador OR
        /// un valor false representa una operador AND.
        /// Valor por Default: false;
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public bool OperadorOR { get; set; }

        /// <summary>
        /// Indica que las propiedades definidas como requeridas, 
        /// seran verificadas antes de tratar de ser guradadas en CRM.
        /// Valor por Defult: true
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public bool ValidarAtributosRequeridos { get; set; }

        /// <summary>
        /// Define las propiedades por las que será ordenada la consulta.
        /// Ej. "Nombre, Apellido:desc" donde Nombre se indica una ordenación
        /// asc por defecto.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public string OrdenarPor { get; set; }






        //TODO: Definir propiedades para el uso de paginación.

        /// <summary>
        /// Propiedad utilizada para definir los registros por página.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public int RegistrosPorPagina { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        private Dictionary<string, string> NodosXML = null;

        [XmlIgnore]
        [JsonIgnore]
        [Ignorar]
        public List<EntidadRelacionada> EntidadesRelacionadas { get; set; }

        #endregion

        #region CONSTRUCTORES

        ///Gris
        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public EntidadCrm()
        {
            Inicializar();
        }

        public EntidadCrm(Guid ID)
        {
            Inicializar();
            this.ID = ID;

        }

        ///Gris
        /// <summary>
        /// Constructor que recibe un Entity.
        /// </summary>
        /// <param name="entidad"></param>
        public EntidadCrm(Entity entidad)
        {
            Inicializar();
            EntidadTIM(entidad);
        }

        ///Gris
        /// <summary>
        /// Constructor que recibe un objeto serializado y un boleano que indica si los valores se deben de obetener desde el  objeto string serializado.
        /// </summary>
        /// <param name="ObjetoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public EntidadCrm(String ObjetoSerializado, bool ObtenerValoresDesdeString = false)
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

        #endregion

        #region MÉTODOS PÚBLICOS

        ///Gris
        /// <summary>
        /// Método que mapea las propiedadesTIM hacia una Entity.
        /// </summary>
        /// <returns>Entity</returns>
        public virtual Entity EntidadCRM()
        {
            Entity entidadCRM = null;
            ObtenerPropiedadesDeClaseTIM();
            //Se valida que esta instancia no sea nula.
            if (this != null)
            {
                try
                {
                    //Se verifica que se haya obtenido el nombre de esquema definido a la clase.
                    if (!string.IsNullOrEmpty(this.NombreEsquema))
                    {
                        //Se crea la instancia de tipo Entity del tipo especificado en el nombre de esquema de la entidad.
                        entidadCRM = new Entity(this.NombreEsquema);
                        entidadCRM.LogicalName = this.NombreEsquema;

                        //Se valida que el objeto InfoEntidad no sea nula.
                        if (this.infoEntidad != null)
                        {
                            //Se valida que se hayan inicializado las propiedades de la clase.
                            if (this.infoEntidad.PropiedadesTIM != null)
                            {
                                //Se recorren las propiedades de la clase para ser mapeadas.
                                foreach (InfoPropiedad propiedadActualTIM in this.infoEntidad.PropiedadesTIM)
                                {
                                    //Verifica que se haya definido un nombre de esquema para la propiedad y que no se ignore.
                                    if (!string.IsNullOrEmpty(propiedadActualTIM.NombreEsquema) && !string.IsNullOrWhiteSpace(propiedadActualTIM.NombreEsquema) && !propiedadActualTIM.IgnorarPropiedad)
                                    {
                                        //Si el valor de la propiedad es nulo, no se toma en cuenta para el mapeo.
                                        if (propiedadActualTIM.Valor != null)
                                        {
                                            //Se valida que el tipo de dato no sea None
                                            if (propiedadActualTIM.TipoDatoCRM != eTipoDatoCRM.None)
                                            {
                                                if (propiedadActualTIM.TipoDatoCRM == eTipoDatoCRM.EntityReference)
                                                {
                                                    //Crea una instancia de tipo EntityReference
                                                    EntityReference er = new EntityReference();

                                                    if (propiedadActualTIM.TipoDato.FullName.Contains("Guid"))
                                                    {
                                                        // Le asigna los valores de la propiedad actual.                                                            
                                                        er.Id = ((Guid)propiedadActualTIM.Valor);
                                                    }

                                                    if (propiedadActualTIM.TipoDato.FullName.Contains("CrmLookup"))
                                                    {
                                                        //TODO: Validar que no sea nulo el ID.
                                                        //Le asigna los valores de la propiedad actual.                                                            
                                                        er.Id = ((CrmLookup)propiedadActualTIM.Valor).ID.Value;
                                                    }

                                                    er.LogicalName = propiedadActualTIM.NombreEsquemaDeReferencia;

                                                    //Asigna el valor de la instancia a la entidad Entity de CRM.
                                                    entidadCRM[propiedadActualTIM.NombreEsquema] = er;
                                                }

                                                //Valida que el tipo de dato sea un OptionSetValue
                                                if (propiedadActualTIM.TipoDatoCRM == eTipoDatoCRM.OptionSetValue)
                                                {
                                                    //Asigna el valor de la instancia a la entidad Entity de CRM.
                                                    entidadCRM[propiedadActualTIM.NombreEsquema] = new OptionSetValue(((CrmPicklist)propiedadActualTIM.Valor).ID.Value);
                                                }

                                                //Valida que el tipo de dato sea un Money
                                                if (propiedadActualTIM.TipoDatoCRM == eTipoDatoCRM.Money)
                                                {
                                                    //Asigna el valor de la instancia a la entidad Entity de CRM.
                                                    entidadCRM[propiedadActualTIM.NombreEsquema] = new Microsoft.Xrm.Sdk.Money(((Decimal?)propiedadActualTIM.Valor).Value);
                                                }

                                                //Valida que el tipo de dato sea un ActivityParty
                                                if (propiedadActualTIM.TipoDatoCRM == eTipoDatoCRM.ActivityParty)
                                                {
                                                    List<Entity> entidades = null;
                                                    CrmActivityParty item = (CrmActivityParty)propiedadActualTIM.Valor;

                                                    if (item.Referencias != null && item.Referencias.Count > 0)
                                                    {
                                                        entidades = new List<Entity>();

                                                        foreach (CrmLookup referencia in item.Referencias)
                                                        {
                                                            Entity entidad = new Entity("activityparty");
                                                            entidad["partyid"] = new EntityReference(referencia.TipoEntidad, referencia.ID.Value);
                                                            entidades.Add(entidad);
                                                        }
                                                    }

                                                    entidadCRM[propiedadActualTIM.NombreEsquema] = entidades.ToArray();
                                                }
                                            }
                                            else
                                            {
                                                //Asigna el valor de la instancia a la entidad Entity de CRM.
                                                entidadCRM[propiedadActualTIM.NombreEsquema] = propiedadActualTIM.Valor;

                                                if (propiedadActualTIM.EsIdPrincipal)
                                                    entidadCRM.Id = (Guid)propiedadActualTIM.Valor;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MapeoEntidadException nex = new MapeoEntidadException(ex);
                    RegistrarError(ex);
                }
            }

            return entidadCRM;
        }

        /// <summary>
        /// Obtiene una lista de los nombres de esquema definidos en cada propiedad de la clase.
        /// </summary>
        /// <returns>Lista de atributos.</returns>
        public string[] Atributos()
        {
            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.infoEntidad == null ? null : this.infoEntidad.Atributos();
        }

        /// <summary>
        /// Obtiene los nombres de los atributos, de las propiedades de clase que son distintos de Nulo.
        /// </summary>
        /// <returns>Listado de los nombre lógicos de los atributos CRM.</returns>
        public string[] AtributosConValor()
        {
            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.infoEntidad == null ? null : this.infoEntidad.AtributosConValor();
        }

        /// <summary>
        /// Obtiene un arreglo de condiciones, de las propiedades que contengan valor.
        /// </summary>
        /// <returns>Array de ConditionExpression</returns>
        public ConditionExpression[] ObtenerArregloDeCondiciones()
        {
            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.infoEntidad == null ? null : this.infoEntidad.ObtenerArregloDeCondiciones();
        }

        /// <summary>
        /// Obtiene un arreglo de condiciones de rangos, de las propiedades que contengan valor.
        /// </summary>
        /// <returns>Array de ConditionExpression</returns>
        public ConditionExpression[] ObtenerArregloDeCondicionesDeRangos()
        {
            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.infoEntidad == null ? null : this.infoEntidad.ObtenerArregloDeCondicionesDeRangos();
        }

        /// <summary>
        /// Obtiene un arreglo de OrderExpression para especificar como será ordenada la consulta.
        /// Para utilizar este método es necesario definir las columnas de ordenamiento en la propiedad OrdenarPor
        /// de esta clase, de la siguiente manera: "Nombre, Apellidos:desc", donde Nombre se define como una consulta ascendente.
        /// Si la propiedad OrdenarPor no se define, no se agrega ordenamiento a la consulta.
        /// </summary>
        /// <returns></returns>
        public OrderExpression[] ObtenerArregloDeOrdenamientos()
        {
            OrderExpression[] lista = null;
            List<Orden> listaOrden = null;
            Orden item = null;

            if (!string.IsNullOrEmpty(OrdenarPor))
            {
                listaOrden = new List<Orden>();

                if (OrdenarPor.Contains(","))
                {
                    string[] elementosOrdenamiento = OrdenarPor.Split(',');

                    foreach (string cadena in elementosOrdenamiento)
                    {
                        item = ObtenerOrdenamiento(cadena.Trim());
                        if (item != null)
                        {
                            listaOrden.Add(item);
                        }
                    }
                }
                else
                {
                    item = ObtenerOrdenamiento(OrdenarPor);
                    if (item != null)
                    {
                        listaOrden.Add(item);
                    }
                }
            }

            if (listaOrden != null)
            {
                lista = infoEntidad.ObtenerOrdenamientos(listaOrden);
            }

            return lista;
        }

        /// <summary>
        /// Verifica que las propiedades definidas como requeridas contengan valor,
        /// de no ser así se lanza una excepción.
        /// </summary>
        public void VerificarRequeridos()
        {
            this.infoEntidad.VerificarRequeridos();
        }

        /// <summary>
        /// Serializa el objeto en una cadena XML
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el XML formateado.</param>
        /// <returns></returns>
        public String XML(Boolean ConFormato = false)
        {
            return Serializar.XML(this, ConFormato);
        }


        /// <summary>
        /// Serializa el objeto en una cadena JSON
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el JSON formateado.</param>
        /// <returns></returns>
        public String JSON(Boolean ConFormato = false)
        {
            return Serializar.JSON(this, ConFormato);
        }

        public string KJSON(Boolean ConFormato = false)
        {
            string cadena = Serializar.JSON(this, ConFormato);
            cadena = cadena.Substring(0, cadena.Length - 1);
            cadena += infoEntidad.ExtenderJSON(ConFormato);
            cadena += "}";
            return cadena;
        }

        public CrmLookup Lookup()
        {
            CrmLookup item = null;

            if (this.ID != null && !string.IsNullOrEmpty(this.NombreEsquema))
            {
                item = new CrmLookup(this.ID.Value, this.NombreEsquema);
            }

            return item;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="NombrePropiedad"></param>
        /// <returns></returns>
        public String this[string NombrePropiedad]
        {
            get
            {
                string valor = null;

                if (NodosXML != null)
                {
                    if (NodosXML.ContainsKey(NombrePropiedad))
                    {
                        valor = NodosXML[NombrePropiedad];
                    }
                }

                return valor;

            }
        }




        #endregion

        #region MÉTODOS PRIVADOS

        ///Gris
        /// <summary>
        /// Inicializa las propiedades de la clase.
        /// </summary>
        void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            registrar = new RegistroError();

            TipoEntidadActualTIM = this.GetType();
            infoEntidad = new InfoEntidad(this);
            ObtenerNombreLogico();


            OperadorOR = false;
            VistaAConsultar = new ColumnasDeMetodo(VistaPorDefecto);
            ValidarAtributosRequeridos = true;
            OrdenarPor = null;
            RegistrosPorPagina = 5000;

            EntidadesRelacionadas = new List<EntidadRelacionada>();

        }


        ///Gris
        /// <summary>
        /// Obtiene las propiedades de la clase TIM por medio de Reflection 
        /// </summary>
        private void ObtenerPropiedadesDeClaseTIM()
        {
            infoEntidad.AsignarPropiedadesTIM();
        }

        ///Gris
        /// <summary>
        /// Obtiene las propiedades de la clase crm por medio de Reflection 
        /// </summary>
        private void ObtenerPropiedadesDeClaseCRM(Entity entidad)
        {
            infoEntidad.AsignarPropiedadesCRM(entidad);
        }


        ///Gris
        /// <summary>
        /// Obtiene el nombre de esquema de CRM para la instancia Actual.
        /// </summary>
        private void ObtenerNombreLogico()
        {
            NombreEsquema = infoEntidad.NombreEsquema;
        }

        /// <summary>
        /// Vista que obtiene todos los nombres de esquema de todos los atributos de la clase.
        /// </summary>
        /// <returns></returns>
        private string[] VistaPorDefecto()
        {
            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
            {
                ObtenerPropiedadesDeClaseTIM();
            }

            return this.infoEntidad == null ? null : this.infoEntidad.Atributos();
        }

        /// <summary>
        /// Asigna los valores de la instancia actual, a partir de una entity..
        /// </summary>
        /// <param name="entidad">Entity.</param>
        private void EntidadTIM(Entity entidad)
        {
            try
            {
                if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
                {
                    ObtenerPropiedadesDeClaseTIM();
                }

                ObtenerPropiedadesDeClaseCRM(entidad);

                //Validamos que la propiedad PropiedadesCRM no esté vacía.
                if (infoEntidad.PropiedadesCRM != null)
                {
                    //Validamos que la propiedad PropiedadesTIM no esté vacía.
                    if (this.infoEntidad.PropiedadesTIM != null)
                    {
                        //Recorre las propiedades de la intancia de clase actual.
                        foreach (InfoPropiedad propiedadActualTIM in this.infoEntidad.PropiedadesTIM)
                        {
                            if (propiedadActualTIM.NombreEsquema != null)
                            {
                                if (propiedadActualTIM.EsIdPrincipal)
                                    this.ID = (propiedadActualTIM.Valor == null) ? null : (Guid?)propiedadActualTIM.Valor;

                                if (!propiedadActualTIM.IgnorarPropiedad)
                                {
                                    propiedadActualTIM.Propiedad.SetValue(this, propiedadActualTIM.Valor, null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MapeoEntidadException nex = new MapeoEntidadException(ex);
                RegistrarError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CadenaSerializada"></param>
        private void DeserializarObjeto(string CadenaSerializada)
        {

            if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
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
                    if (this.infoEntidad.PropiedadesTIM != null)
                    {
                        //Recorre las propiedades de la intancia de clase actual.
                        foreach (InfoPropiedad propiedadActualObjDes in this.infoEntidad.PropiedadesTIM)
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

        //TODO:Usar este código para aplicaciones onprem.
        //public static T ParseXML<T>(this string @this) where T : class
        //{
        //    var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
        //    return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        //}


        ///Gris
        /// <summary>
        /// Maneja una excepción causada al deserializar.
        /// </summary>
        /// <param name="ex">Excepción.</param>
        /// <param name="CadenaSerializada">String serializado.</param>
        private void CrearErrorDeserializacion(Exception ex, string CadenaSerializada)
        {
            try
            {
                Deserializacion = new InfoDeserializar();
                Deserializacion.Completado = false;

                if (ex.Message.Contains("There is an error in XML document"))
                {
                    string NombreColumna = null;

                    int indiceInicio = ex.Message.IndexOf(",");
                    int indiceFin = ex.Message.IndexOf(")");
                    int longitud = indiceFin - indiceInicio;

                    string txtIndice = ex.Message.Substring(indiceInicio + 1, longitud - 1).Trim();
                    int inicioError = int.Parse(txtIndice);

                    string caracterInicio = CadenaSerializada.Substring(inicioError - 2, 1);

                    if (caracterInicio == "<")
                    {
                        string seccionCadena = CadenaSerializada.Substring(0, inicioError - 2);
                        indiceInicio = seccionCadena.LastIndexOf("<");
                        indiceFin = seccionCadena.LastIndexOf(">");
                        longitud = indiceFin - (indiceInicio + 1);

                        if (longitud > 0)
                        {
                            NombreColumna = seccionCadena.Substring(indiceInicio + 2, longitud - 1);
                        }

                    }
                    else
                    {

                        string textoRestante = CadenaSerializada.Substring(inicioError);
                        indiceInicio = textoRestante.IndexOf("/");
                        indiceFin = textoRestante.IndexOf(">");
                        longitud = indiceFin - indiceInicio;

                        if (longitud > 0)
                        {
                            NombreColumna = textoRestante.Substring(indiceInicio + 1, longitud - 1);
                        }

                    }

                    Deserializacion.PropiedadFallida = NombreColumna;
                }

                Deserializacion.MensajeError = ex.Message;
            }
            catch (Exception ex2)
            {
                DeserializacionException tex = new DeserializacionException(ex.Message, ex2);
                RegistrarError(tex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="XmlObjetoSerializado"></param>
        private void ObtenerNodosXML(string XmlObjetoSerializado)
        {
            if (!string.IsNullOrEmpty(XmlObjetoSerializado))
            {
                NodosXML = new Dictionary<string, string>();

                if (this.infoEntidad != null && this.infoEntidad.PropiedadesTIM == null)
                {
                    ObtenerPropiedadesDeClaseTIM();
                }

                foreach (InfoPropiedad propTIM in this.infoEntidad.PropiedadesTIM)
                {
                    string inicioNodo = string.Format("<{0}>", propTIM.NombrePropiedad);
                    string finNodo = string.Format("</{0}>", propTIM.NombrePropiedad);

                    if (XmlObjetoSerializado.Contains(inicioNodo))
                    {
                        int indiceInicioNodo = XmlObjetoSerializado.IndexOf(inicioNodo);
                        int indiceFinNodo = XmlObjetoSerializado.IndexOf(finNodo);
                        int longitud = indiceFinNodo - indiceInicioNodo;

                        string cadenaNodo = XmlObjetoSerializado.Substring(indiceInicioNodo, longitud);
                        string textoInternoNodo = cadenaNodo.Replace(inicioNodo, "");

                        NodosXML.Add(propTIM.NombrePropiedad, textoInternoNodo);

                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        /// <summary>
        /// Obtiene los criterios de ordenamiento definidos en la propiedad OrdenarPor
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private Orden ObtenerOrdenamiento(String cadena)
        {
            Orden item = null;

            if (!String.IsNullOrEmpty(cadena))
            {
                if (cadena.Contains(":"))
                {
                    string[] elementos = cadena.Split(':');
                    item = new Orden()
                    {
                        Propiedad = elementos[0].Trim(),
                        EsAscendente = elementos[1].Trim().ToLower() == "desc" ? false : true
                    };

                }
                else
                {
                    item = new Orden()
                    {
                        Propiedad = cadena,
                        EsAscendente = true
                    };
                }
            }

            return item;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="CadenaSerializada"></param>
        /// <returns></returns>
        private String ConvertirCaracteresPermitidos(String CadenaSerializada)
        {
            String cadena = CadenaSerializada;

            try
            {
                /*********************************************************************************************************
                //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
                //Usando System.IO
                using (StreamReader sr = new StreamReader(string.Format(@"{0}", ArchivoCaracteresXML), Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string Line = sr.ReadLine();
                        if (Line.Contains("|"))
                        {
                            string[] values = Line.Split('|');
                            if (values.Length > 1)
                            {
                                cadena = cadena.Replace(values[0], values[1]);
                            }
                        }
                    }
                }
                *********************************************************************************************************/
                cadena = cadena.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace(",", "");
            }
            catch (Exception ex)
            {
                //TODO: Dejar esto configurable al desarrollador.
                cadena = cadena.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace(",", "");
            }

            return cadena;
        }


        /******************************************************************************************************************************
        //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
        //Usando System.IO
        /// <summary>
        /// Ruta del archivo de configuraciones
        /// </summary>
        private static readonly string ArchivoCaracteresXML =
            Path.Combine(
                Path.Combine(
                    Path.Combine(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "TI-M"),
                    "CRM", "Apps"),
                "Administrador de Caracteres Especiales", "Archivos"),
            "xml.txt");

        /// <summary>
        /// Ruta del archivo de configuraciones
        /// </summary>
        private static readonly string ArchivoCaracteresJSON =
            Path.Combine(
                Path.Combine(
                    Path.Combine(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "TI-M"),
                    "CRM", "Apps"),
                "Administrador de Caracteres Especiales", "Archivos"),
            "json.txt");


        ******************************************************************************************************************************/




        #endregion

        ///Gris
        /// <summary>
        /// Registro de error con mensaje.
        /// </summary>
        /// <param name="Mensaje">Mensaje del error.</param>
        /// <param name="Ex">Excepción producida.</param>
        /// <param name="EventoID">Identificador del evento.</param>
        /// <param name="NumeroLinea">Línea donde se produjo el error.</param>
        /// <param name="Metodo">Método donde se produjo el error.</param>
        /// <param name="ArchivoOrigen">Archivo origen donde se produjo el error.</param>
        public void RegistrarError(string Mensaje, Exception Ex, int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex);
        }

        /// <summary>
        /// Registro de error.
        /// </summary>
        /// <param name="Ex">Excepción producida.</param>
        /// <param name="EventoID">Identificador del evento.</param>
        /// <param name="NumeroLinea">Línea donde se produjo el error.</param>
        /// <param name="Metodo">Método donde se produjo el error.</param>
        /// <param name="ArchivoOrigen">Archivo origen donde se produjo el error.</param>
        public void RegistrarError(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Ex);
        }
    }
}
