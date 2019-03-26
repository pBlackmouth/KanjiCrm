#region Hitorial de cambios
///////////////////////////////////////////////////
//      Autor: Francisco Bocanegra Alcocer
//      Creada el: 20 de Enero de 2014
///////////////////////////////////////////////////
//      Modificada el: 20 de Enero de 2014
//      Por: Francisco Bocanegra Alcocer
//
//      Descripción del cambio: 
//      Se agregó el método CRMEntity() y el constructor  EntidadCrm
//      para hacer el mapeo local y conversion de tipo EntidadCrm -> Entity.
///////////////////////////////////////////////////
//      Modificada el: 21 de Enero de 2014
//      Por: Francisco Bocanegra Alcocer
//
//      Descripción del cambio: 
//      1.- Se agregó el método TIMEntity() y al constructor  EntidadCrm
//      para hacer el mapeo local y conversion de tipo Entity -> EntidadCrm.
//      2.- Se agregó el método Atributos().
//////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;

using System.Diagnostics;
using Tim.Crm.Base.Entidades.Excepciones;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// Clase base para las entidades del Framework 2.0.0
    /// </summary>
    [Obsolete("Utilice la clase base EntidadCrm, esta clase será eliminada al momento de estar seguros que EntidadCrm funciona completamente.", false)]
    public sealed class EntidadCrmObsoleta
    {
        #region VARIABLES Y PROPIEDADES

        ///Gris
        /// <summary>
        /// Campo privado para almacenar el identificador de la clase.
        /// </summary>
        private Guid? _id = null;

        
        /// <summary>
        /// Propiedad que guarda el identificador en CRM
        /// </summary>
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
        /// Propiedad para almacenar el tipo de entidad.
        /// </summary>
        public string TipoEntidad { get; set; }

       
        /// <summary>
        /// Tipo de entidad de la instancia actual
        /// </summary>
        private Type entidadTIMClassType;

        
        /// <summary>
        /// Variable que contiene las propiedades de la instancia actual.
        /// </summary>
        private PropertyInfo[] entidadTIMClassObjectProperties;

        /// <summary>
        /// 
        /// </summary>
        public ColumnasDeMetodo VistaAConsultar;

        ///Gris
        /// <summary>
        /// Propiedad operador O lógico.
        /// </summary>
        public bool OperadorOR { get; set; }


        ///Gris
        /// <summary>
        /// Propiedad boleana que indica si se deben validar los atributos requeridos.
        /// </summary>
        public bool ValidarAtributosRequeridos { get; set; }

        #endregion

        #region CONSTRUCTORES

        ///Gris
        /// <summary>
        /// Constructor vació.
        /// </summary>
        public EntidadCrmObsoleta()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            ObtenerPropiedadesDeClase();
            ObtenerNombreLogico();

            OperadorOR = false;
            VistaAConsultar = new ColumnasDeMetodo(VistaPorDefecto);
            ValidarAtributosRequeridos = true;
        }

        /// <summary>
        /// Constructor que permite mapear un objeto de tipo Entity a una clase de tipo EntidadCrm
        /// </summary>
        /// <param name="entity">Objeto Entity.</param>
        public EntidadCrmObsoleta(Entity entity)
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            ObtenerPropiedadesDeClase();
            ObtenerNombreLogico();
            TIMEntity(entity);

            OperadorOR = false;
            VistaAConsultar = new ColumnasDeMetodo(VistaPorDefecto);
            ValidarAtributosRequeridos = true;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        /// <summary>
        /// Convierte la instancia actual de la clase a un objeto Entity de CRM.
        /// </summary>
        /// <returns>Objeto Entity de CRM</returns>
        public Entity CRMEntity()
        {
            Entity entidadCRM = null;

            if (this != null)
            {
                try
                {
                    //Variable que contiene el nombre de esquema de la entidad o de cada propiedad en cuestion.
                    string nombreesquema = null;

                    //Variable que contiene el tipo de datos especificado en cada propiedad de la clase.
                    eTipoDatoCRM tipodatocrm = eTipoDatoCRM.None;

                    //Variable que contiene el nombre de la entidad relacionada de un EntityReference.
                    string referenciaLogicalName = null;

                    //variable que contiene la indicación para tomar en cuenta la propiedad actual
                    Boolean IgnorarPropiedad = false;

                    //Variable que es usada para identificar el atributo ID de CRM
                    Boolean EsIDPrincipal = false;


                    //8.- Se verifica que se haya obtenido el nombre de esquema definido a la clase.
                    if (!string.IsNullOrEmpty(this.TipoEntidad))
                    {
                        //9.- Se crea la instancia de tipo Entity del tipo especificado en el nombre de esquema de la entidad.
                        entidadCRM = new Entity(this.TipoEntidad);
                        entidadCRM.LogicalName = this.TipoEntidad;


                        //10.- Se vacia la variable nombre de esquema para uso posterior.
                        nombreesquema = null;

                        //11.- Se recorren las propiedades de la clase para ser mapeadas.
                        foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
                        {
                            //12 .- Reinicia el valor de la variable para que la propiedad sea tomada en cuenta.
                            IgnorarPropiedad = false;

                            //Inicializa el nombre de esquema para evitar valores anteriores.
                            nombreesquema = null;

                            //13.- Obtiene los atributos de cada propiedad
                            object[] atributosDePropiedad = propiedadActual.GetCustomAttributes(true);

                            //14.- Se verifica que haya atributos personalizados en la propiedad.
                            //Si no cuenta con atributos personalizados no se toma en cuenta para el mapeo.
                            if (atributosDePropiedad != null && atributosDePropiedad.Length > 0)
                            {
                                //15.- Recorre los atributos declarados en cada propiedad
                                foreach (object attr in atributosDePropiedad)
                                {
                                    //16.- Verifica que el atributo sea el nombre de esquema.
                                    if (attr.GetType() == typeof(NombreEsquemaCrm))
                                    {
                                        //17.- Asigna el nombre de esquema a la variable.
                                        nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                                    }

                                    //18.- Verifica que el atributo sea el Tipo de Dato CRM
                                    if (attr.GetType() == typeof(TipoDatoCrm))
                                    {
                                        //19.- Se obtiene el tipo de Dato CRM
                                        tipodatocrm = ((TipoDatoCrm)attr).NombreTipoDatoCRM;
                                        referenciaLogicalName = ((TipoDatoCrm)attr).EntidadRelacionadaCRM;
                                    }

                                    //20.- Verifica que el atributo sea el nombre de esquema.
                                    if (attr.GetType() == typeof(Ignorar))
                                    {
                                        //21.- Asigna el nombre de esquema a la variable.
                                        IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                                    }

                                    // Verifica que el atributo sea el IdentificadorCRM.
                                    if (attr.GetType() == typeof(IdentificadorCRM))
                                    {
                                        //Asigna el nombre de esquema a la variable.
                                        EsIDPrincipal = ((IdentificadorCRM)attr).EsIDPrincipal;
                                    }

                                }

                                //22.- Verifica que se haya definido un nombre de esquema para la propiedad y que no se ignore.
                                if ((!string.IsNullOrEmpty(nombreesquema) || !string.IsNullOrWhiteSpace(nombreesquema)) && !IgnorarPropiedad)
                                {
                                    //23.- Se obtiene el valor de la propiedad actual de la entidad.
                                    object valorPropiedadActual = propiedadActual.GetValue(this, null);

                                    //24.- Si la propiedad es nula, no se toma en cuenta para el mapeo.
                                    if (valorPropiedadActual != null)
                                    {
                                        //25.- Se valida que el tipo de dato no sea None
                                        if (tipodatocrm != eTipoDatoCRM.None)
                                        {
                                            //26.- Valida que el tipo de dato sea un EntityReference
                                            if (tipodatocrm == eTipoDatoCRM.EntityReference)
                                            {
                                                //27.- Valida que la variable referenciaLogicalName no sea nula o vacía.
                                                if (!string.IsNullOrEmpty(referenciaLogicalName))
                                                {
                                                    //28.- Crea una instancia de tipo EntityReference y le asigna los valores
                                                    //de la instancia.
                                                    EntityReference er = new EntityReference();
                                                    er.Id = ((Guid)valorPropiedadActual);
                                                    er.LogicalName = referenciaLogicalName;


                                                    //29.- Asigna el valor de la instancia a la entidad Entity de CRM.
                                                    entidadCRM[nombreesquema] = er;
                                                }
                                            }

                                            //30.- Valida que el tipo de dato sea un OptionSetValue
                                            if (tipodatocrm == eTipoDatoCRM.OptionSetValue)
                                            {
                                                //31.- Asigna el valor de la instancia a la entidad Entity de CRM.
                                                entidadCRM[nombreesquema] = new OptionSetValue((int)valorPropiedadActual);
                                            }

                                            //32.- Valida que el tipo de dato sea un Money
                                            if (tipodatocrm == eTipoDatoCRM.Money)
                                            {
                                                //33.- Asigna el valor de la instancia a la entidad Entity de CRM.
                                                entidadCRM[nombreesquema] = new Microsoft.Xrm.Sdk.Money((Decimal)valorPropiedadActual);
                                            }
                                        }
                                        else
                                        {
                                            //34.- Verifica que el valor de propiedad no sea nula.
                                            if (valorPropiedadActual != null)
                                            {
                                                //35.- Asigna el valor de la instancia a la entidad Entity de CRM.
                                                entidadCRM[nombreesquema] = valorPropiedadActual;

                                                if (EsIDPrincipal)
                                                    entidadCRM.Id = (Guid)valorPropiedadActual;
                                            }
                                        }
                                    }



                                }
                            }

                            EsIDPrincipal = false;
                        }
                    }


                }
                catch (Exception ex)
                {

                }

            }

            return entidadCRM;
        }

        /// <summary>
        /// Obtiene una lista de atributos definidos en cada propiedad de la clase actual.
        /// </summary>
        /// <returns>Lista de atributos.</returns>
        public string[] Atributos()
        {
            List<string> lista = new List<string>();

            //Variable que contiene el nombde de esquema de la propiedad.
            string nombreesquema = null;

            //Variable que contiene la indicación para tomar en cuenta la propiedad actual
            Boolean IgnorarPropiedad = false;

            //3.- Recorre las propiedades de la clase.
            foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
            {
                //4 .- Reinicia el valor de la variable para que la propiedad sea tomada en cuenta.
                IgnorarPropiedad = false;

                //5.- Obtiene los atributos personalizados de cada propiedad.
                object[] attributes = propiedadActual.GetCustomAttributes(true);

                //Inicializa el nombre de esquema para evitar valores anteriores.
                nombreesquema = null;

                //6.- Valida que se hayan obtenido atributos para la propiedad actual.
                if (attributes != null && attributes.Length > 0)
                {
                    //7.- Recorre los atributos actuales para obtener la información necesaria.
                    foreach (object attr in attributes)
                    {
                        //8.- Compara para hasta que sea un atributo de tipo NombreEsquemaCrm.
                        if (attr.GetType() == typeof(NombreEsquemaCrm))
                        {
                            //9.- Obtiene el nombre de esquema.
                            nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                        }

                        //10.- Compara para hasta que sea un atributo de tipo Ignorar.
                        if (attr.GetType() == typeof(Ignorar))
                        {
                            //11.- Se determina que esta propiedad será ignorada.
                            IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                        }
                    }
                }

                //12.- Verifica que esta propiedad no sea ignorada.
                if (!IgnorarPropiedad)
                {
                    //13.- Valida que se haya obtenido un nombre de esquema.
                    if (!string.IsNullOrEmpty(nombreesquema))
                    {
                        //14.- Se agrega el nombre de esquema la lista de atributos.
                        lista.Add(nombreesquema);
                    }
                }

            }


            return lista.ToArray();
        }

        /// <summary>
        /// Obtiene los nombres de los atributos, de las propiedades de clase que son distintos de Nulo.
        /// </summary>
        /// <returns>Listado de los nombre lógicos de los atributos CRM.</returns>
        public string[] AtributosConValor()
        {
            List<string> lista = new List<string>();

            //Variable que contiene el nombde de esquema de la propiedad.
            string nombreesquema = null;

            //Variable que contiene la indicación para tomar en cuenta la propiedad actual
            Boolean IgnorarPropiedad = false;

            //Recorre las propiedades de la clase.
            foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
            {
                //Reinicia el valor de la variable para que la propiedad sea tomada en cuenta.
                IgnorarPropiedad = false;

                nombreesquema = null;

                //Se obtiene el valor de la propiedad actual de la entidad.
                object valorPropiedadActual = propiedadActual.GetValue(this, null);


                if (valorPropiedadActual != null)
                {
                    //Obtiene los atributos personalizados de cada propiedad.
                    object[] attributes = propiedadActual.GetCustomAttributes(true);

                    //Valida que se hayan obtenido atributos para la propiedad actual.
                    if (attributes != null && attributes.Length > 0)
                    {
                        //Recorre los atributos actuales para obtener la información necesaria.
                        foreach (object attr in attributes)
                        {
                            //Compara para hasta que sea un atributo de tipo NombreEsquemaCrm.
                            if (attr.GetType() == typeof(NombreEsquemaCrm))
                            {
                                //Obtiene el nombre de esquema.
                                nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                            }

                            //Compara para hasta que sea un atributo de tipo Ignorar.
                            if (attr.GetType() == typeof(Ignorar))
                            {
                                //Se determina que esta propiedad será ignorada.
                                IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                            }
                        }
                    }

                    //Verifica que esta propiedad no sea ignorada.
                    if (!IgnorarPropiedad)
                    {
                        //Valida que se haya obtenido un nombre de esquema.
                        if (!string.IsNullOrEmpty(nombreesquema))
                        {
                            //Se agrega el nombre de esquema la lista de atributos.
                            lista.Add(nombreesquema);
                        }
                    }
                }

            }


            return lista.ToArray();
        }

        /// <summary>
        /// Obtiene un arreglo de condiciones, de las propiedades que contengan valor.
        /// </summary>
        /// <returns>Array de ConditionExpression</returns>
        public ConditionExpression[] ObtenerArregloDeCondiciones()
        {
            List<ConditionExpression> condiciones = null;
            ConditionExpression item = null;

            //Variable que contiene el nombde de esquema de la propiedad.
            string nombreesquema = null;

            //Variable que contiene la indicación para tomar en cuenta la propiedad actual
            Boolean IgnorarPropiedad = false;

            if (this != null)
            {
                condiciones = new List<ConditionExpression>();

                //Recorre las propiedades de la clase.
                foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
                {
                    object valorPropiedadActual = propiedadActual.GetValue(this, null);

                    //Inicializa el nombre de esquema para evitar valores anteriores.
                    nombreesquema = null;

                    if (valorPropiedadActual != null)
                    {

                        //Reinicia el valor de la variable para que la propiedad sea tomada en cuenta.
                        IgnorarPropiedad = false;

                        //Obtiene los atributos personalizados de cada propiedad.
                        object[] attributes = propiedadActual.GetCustomAttributes(true);

                        //Valida que se hayan obtenido atributos para la propiedad actual.
                        if (attributes != null && attributes.Length > 0)
                        {
                            //Recorre los atributos actuales para obtener la información necesaria.
                            foreach (object attr in attributes)
                            {
                                //Compara para hasta que sea un atributo de tipo NombreEsquemaCrm.
                                if (attr.GetType() == typeof(NombreEsquemaCrm))
                                {
                                    //Obtiene el nombre de esquema.
                                    nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                                }

                                //Compara para hasta que sea un atributo de tipo Ignorar.
                                if (attr.GetType() == typeof(Ignorar))
                                {
                                    //Se determina que esta propiedad será ignorada.
                                    IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                                }
                            }
                        }


                        //Verifica que esta propiedad no sea ignorada.
                        if (!IgnorarPropiedad)
                        {
                            //Valida que se haya obtenido un nombre de esquema.
                            if (!string.IsNullOrEmpty(nombreesquema))
                            {

                                //Se obtiene el tipo de dato destino al que será convertido la propiedad de CRM
                                Type tipoDestino = propiedadActual.PropertyType;

                                item = new ConditionExpression();
                                item.AttributeName = nombreesquema;

                                if (tipoDestino.FullName.Contains("String"))
                                {
                                    item.Operator = ConditionOperator.Like;
                                    item.Values.Add(String.Format("%{0}%", valorPropiedadActual));
                                }
                                else
                                {
                                    item.Operator = ConditionOperator.Equal;
                                    item.Values.Add(valorPropiedadActual);
                                }

                                condiciones.Add(item);
                            }
                        }




                    }
                }
            }

            return condiciones.ToArray();
        }

        /// <summary>
        /// Verifica que las propiedades definidas como requeridas contengan valor,
        /// de no ser así se lanza una excepción.
        /// </summary>
        public void VerificarRequeridos()
        {

            //Variable que contiene el nombre de esquema de la entidad o de cada propiedad en cuestion.
            string nombreesquema = null;

            bool EsRequerido = false;

            //Se recorren las propiedades de la clase para ser mapeadas.
            foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
            {
                nombreesquema = null;
                EsRequerido = false;

                //Obtiene los atributos de cada propiedad
                object[] atributosDePropiedad = propiedadActual.GetCustomAttributes(true);

                //Se verifica que haya atributos personalizados en la propiedad.
                //Si no cuenta con atributos personalizados no se toma en cuenta para el mapeo.
                if (atributosDePropiedad != null && atributosDePropiedad.Length > 0)
                {
                    //Recorre los atributos declarados en cada propiedad
                    foreach (object attr in atributosDePropiedad)
                    {
                        //Verifica que el atributo sea el nombre de esquema.
                        if (attr.GetType() == typeof(NombreEsquemaCrm))
                        {
                            //Asigna el nombre de esquema a la variable.
                            nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                        }

                        //Verifica que el atributo sea el nombre de esquema.
                        if (attr.GetType() == typeof(Requerido))
                        {
                            //Asigna el nombre de esquema a la variable.
                            EsRequerido = ((Requerido)attr).EsRequerido;
                        }

                    }
                }

                object valorPropiedad = propiedadActual.GetValue(this, null);

                if (EsRequerido && valorPropiedad == null)
                {
                    throw new MapeoEntidadException(String.Format("El atributo {0} tiene un valor nulo y está definido como requerido.", nombreesquema));
                }

            }
        }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// Vista que obtiene todos los nombres de esquema de todos los atributos de la clase.
        /// </summary>
        /// <returns></returns>
        private string[] VistaPorDefecto()
        {
            return this.Atributos();
        }


        /// <summary>
        /// Convierte una instancia de tipo Entity de CRM a una instancia del objeto actual.
        /// </summary>
        private void TIMEntity(Entity entity)
        {

            if (entity != null)
            {

                try
                {
                    //- Variables locales utilizadas para guardar los valores de cada tipo de dato.
                    EntityReference valorLookup = null;
                    Money valorMoney = null;
                    OptionSetValue valorOption = null;
                    Boolean valorBool = false;
                    Decimal valorDecimal = Decimal.Zero;
                    Int32 valorEntero32 = -1;
                    Int64 valorEntero64 = -1;
                    Guid valorGuid = Guid.Empty;
                    DateTime valorDate = DateTime.MinValue;

                    //Variable que contiene el nombre de esquema de cada propiedad.
                    string nombreesquema = String.Empty;

                    //Variable que contiene los atibutos personalizados declarados en la clase de la entidad.
                    AttributeCollection entidadCRMProperties;

                    //Variable que contiene la indicación para tomar en cuenta la propiedad actual
                    Boolean IgnorarPropiedad = false;

                    //Variable que es usada para identificar el atributo ID de CRM
                    Boolean EsIDPrincipal = false;


                    //3.- Obtiene los atributos personalizados de la instancia actual.
                    entidadCRMProperties = entity.Attributes;

                    //4.- Validamos que la variable entidadTIMAttributes no esté vacía.
                    if (entidadCRMProperties != null)
                    {
                        //5.- Recorre las propiedades de la intancia de clase actual.
                        foreach (PropertyInfo propiedadActual in entidadTIMClassObjectProperties)
                        {
                            //6 .- Reinicia el valor de la variable para que la propiedad sea tomada en cuenta.
                            IgnorarPropiedad = false;

                            //7.- Obtiene los atributos de la propiedad actual.
                            object[] atributosPropiedadActual = propiedadActual.GetCustomAttributes(true);

                            //8.- Valida que la propiedad actual tenga atributos personalizados definidos.
                            if (atributosPropiedadActual != null && atributosPropiedadActual.Length > 0)
                            {
                                //9.- Recorre los atributos personalizados
                                foreach (object attr in atributosPropiedadActual)
                                {
                                    //10.- Busca hasta encontrar el atributo personalizado de tipo NombreEsquemaCrm
                                    if (attr.GetType() == typeof(NombreEsquemaCrm))
                                    {
                                        //11.- Obtiene el nombre de esquema de la propiedad
                                        nombreesquema = ((NombreEsquemaCrm)attr).NombreEsquema.ToLower();
                                    }

                                    //12.- Verifica que el atributo sea el nombre de esquema.
                                    if (attr.GetType() == typeof(Ignorar))
                                    {
                                        //13.- Asigna el nombre de esquema a la variable.
                                        IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                                    }

                                    // Verifica que el atributo sea el IdentificadorCRM.
                                    if (attr.GetType() == typeof(IdentificadorCRM))
                                    {
                                        //Asigna el nombre de esquema a la variable.
                                        EsIDPrincipal = ((IdentificadorCRM)attr).EsIDPrincipal;
                                    }
                                }

                                //14.- Se valida que nombreesquema no se encuentre nulo o vacio y que no se ignore.
                                if ((!string.IsNullOrEmpty(nombreesquema) || !string.IsNullOrWhiteSpace(nombreesquema)) && !IgnorarPropiedad)
                                {
                                    //15.- Se verifica que las propiedades de Entity contenga el nombre de esquema definido.
                                    if (entidadCRMProperties.Keys.Contains(nombreesquema.ToLower()))
                                    {

                                        //16.- Se utiliza para obtener el valor de la propiedad actual y se convierte a String, 
                                        //pero tambien contiene el tipo de dato para estos 3 casos: EntityReference, Money y OptionSetValue.
                                        string valorPropiedadActualCRM = entidadCRMProperties[nombreesquema].ToString();

                                        //17.- Se valida que el tipo de dato contenga EntityReference.
                                        if (valorPropiedadActualCRM.Contains("Microsoft.Xrm.Sdk.EntityReference"))
                                        {
                                            //18.- Se asigna el valor del Guid a la variable.
                                            valorPropiedadActualCRM = ((EntityReference)(entidadCRMProperties[nombreesquema])).Id.ToString();

                                            //19.- Se obtiene el valor de la propiedad EntityReference.
                                            valorLookup = new EntityReference();
                                            valorLookup = ((EntityReference)(entidadCRMProperties[nombreesquema]));
                                        }

                                        //20.- Se valida que el tipo de dato contenga Money.
                                        if (valorPropiedadActualCRM.Contains("Microsoft.Xrm.Sdk.Money"))
                                        {
                                            //23.- Se asigna el valor del Int a la variable.
                                            valorPropiedadActualCRM = ((Money)(entidadCRMProperties[nombreesquema])).Value.ToString();

                                            //21.- Se obtiene el valor de la propiedad Money.
                                            valorMoney = new Money();
                                            valorMoney = ((Money)(entidadCRMProperties[nombreesquema]));
                                        }

                                        //22.- Se valida que el tipo de dato contenga OptionSetValue.
                                        if (valorPropiedadActualCRM.Contains("Microsoft.Xrm.Sdk.OptionSetValue"))
                                        {
                                            //23.- Se asigna el valor del Int a la variable.
                                            valorPropiedadActualCRM = ((OptionSetValue)(entidadCRMProperties[nombreesquema])).Value.ToString();

                                            //24.- Se obtiene el valor de la propiedad OptionSetValue.
                                            valorOption = new OptionSetValue();
                                            valorOption = ((OptionSetValue)(entidadCRMProperties[nombreesquema]));
                                        }

                                        //25.- Se obtiene le valor de la propiedad actual de la entidad, que debería ser nulo.
                                        object valorPropiedadClase = propiedadActual.GetValue(this, null);

                                        //26.- Se obtiene el tipo de dato destino al que será convertido la propiedad de CRM
                                        Type tipoDestino = propiedadActual.PropertyType;

                                        #region Conversiones de String a otro tipo de dato

                                        //27.- Asignación a tipo String
                                        if (tipoDestino.FullName.Contains("String"))
                                        {
                                            //28.- Asignacion directa a la propiedad de la clase.
                                            valorPropiedadClase = valorPropiedadActualCRM;
                                        }


                                        //29.- Conversiones de String a Lookup
                                        if (tipoDestino.FullName.Contains("CrmLookup"))
                                        {
                                            //30.- Se extraen los valores de valorLookup.
                                            CrmLookup cl = new CrmLookup();
                                            cl.ID = valorLookup.Id;
                                            cl.Nombre = valorLookup.Name;

                                            //31.- Asignar el valor a la propiedad de la clase.
                                            valorPropiedadClase = cl;
                                        }



                                        //35.- Conversión de String a DateTime
                                        if (tipoDestino.FullName.Contains("DateTime"))
                                        {
                                            //36.- Se trata de convertir el valor obtenido en valorPropiedadActual a DateTime
                                            if (DateTime.TryParse(valorPropiedadActualCRM, out valorDate))
                                            {
                                                //37.- Asignar el valor a la propiedad de la clase.
                                                valorPropiedadClase = valorDate;
                                            }
                                        }

                                        //38.- Conversión de String a Guid
                                        if (tipoDestino.FullName.Contains("Guid"))
                                        {
                                            //39.- Se trata de convertir el valor obtenido en valorPropiedadActual a Guid
                                            if (Guid.TryParse(valorPropiedadActualCRM, out valorGuid))
                                            {
                                                //40.- Asignar el valor a la propiedad de la clase.
                                                valorPropiedadClase = valorGuid;

                                                if (EsIDPrincipal)
                                                    this.ID = valorGuid;
                                            }
                                        }

                                        //Conversión de String a Entero
                                        if (tipoDestino.FullName.Contains("Int32"))
                                        {
                                            //e trata de convertir el valor obtenido en valorPropiedadActual a Int32
                                            if (int.TryParse(valorPropiedadActualCRM, out valorEntero32))
                                            {
                                                //Asignar el valor a la propiedad de la clase.
                                                valorPropiedadClase = valorEntero32;
                                            }
                                        }

                                        if (tipoDestino.FullName.Contains("Int64"))
                                        {
                                            //42.- Se trata de convertir el valor obtenido en valorPropiedadActual a Int32
                                            if (Int64.TryParse(valorPropiedadActualCRM, out valorEntero64))
                                            {
                                                //43.- Asignar el valor a la propiedad de la clase.
                                                valorPropiedadClase = valorEntero64;
                                            }
                                        }


                                        //44.- Conversión de CrmEntityReference a Decimal
                                        if (tipoDestino.FullName.Contains("Decimal"))
                                        {
                                            try
                                            {
                                                //45.- Asignar el valor a la propiedad de la clase.
                                                valorPropiedadClase = valorMoney.Value;
                                            }
                                            catch (Exception ex)
                                            {
                                                //46.- En caso de que suceda un error, se intenta de otra forma.
                                                if (decimal.TryParse(valorPropiedadActualCRM, out valorDecimal))
                                                {
                                                    //47.- Asignar el valor a la propiedad de la clase.
                                                    valorPropiedadClase = valorDecimal;
                                                }
                                            }

                                        }

                                        #endregion

                                        //48.- Se asigna el valor de la propiedad directamente a la instancia local de la clase.
                                        propiedadActual.SetValue(this, valorPropiedadClase, null);
                                    }
                                }
                            }

                            EsIDPrincipal = false;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// Obtiene el LogicalName de la Entidad definido en el atributo NombreEsquema.
        /// </summary>
        private void ObtenerNombreLogico()
        {

            //Se obtienen los atributos personalizados definidos para la clase actual.
            object[] entidadTIMAttributes = entidadTIMClassType.GetCustomAttributes(true);

            //Se verifica que se hayan encontrado atributos personalizados en la entidad
            if (entidadTIMAttributes != null && entidadTIMAttributes.Length > 0)
            {
                //Se recorren los atributos definidos en la entidad.
                foreach (object attr in entidadTIMAttributes)
                {
                    //6Se comparan los atributos y se trata de obtener el nombre de esquema.
                    if (attr.GetType() == typeof(NombreEsquemaCrm))
                    {
                        //Se obtiene el nombre de esquema de la entidad.
                        this.TipoEntidad = ((NombreEsquemaCrm)attr).NombreEsquema;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene las propiedades de la clase por medio de Reflection 
        /// y las asigna a la variable entidadTIMClassObjectProperties de la clase.
        /// </summary>
        private void ObtenerPropiedadesDeClase()
        {

            entidadTIMClassType = this.GetType();

            //2.- Obtiene las propiedades de la instancia actual.
            entidadTIMClassObjectProperties = entidadTIMClassType.GetProperties();
        }


        #endregion
    }
}

