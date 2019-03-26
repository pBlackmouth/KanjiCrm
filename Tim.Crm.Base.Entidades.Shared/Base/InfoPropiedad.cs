using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    internal class InfoPropiedad : IRegistroError
    {
        ///Gris
        /// <summary>
        /// Campo privado para almacenar la entidad CRM.
        /// </summary>
        private EntidadCrm Entidad;


        ///Gris
        /// <summary>
        /// Campo privado utilizado para acceder a los métodos de registro de errores.
        /// </summary>
        private RegistroError registrar = null;


        ///Gris
        /// <summary>
        /// Propiedad utilizada para obtener las propiedades de la clase, a través de reflection.
        /// </summary>
        public PropertyInfo Propiedad { get; set; }


        ///Gris
        /// <summary>
        /// Propiedad que almacena el nombre de esquema de la propiedad.
        /// </summary>
        public String NombreEsquema { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NombreEsquemaDeReferencia { get; set; }

        ///Gris
        /// <summary>
        /// Nombre de la propiedad.
        /// </summary>
        public String NombrePropiedad { get; set; }

        /// <summary>
        /// Nombre para mostrar en el cliente.
        /// </summary>
        public String NombreParaMostrar { get; set; }


        public eTipoControlWeb? TipoControlWeb { get; set; }


        ///Gris
        /// <summary>
        /// Propiedad que indica si se debe ignorar  o no la propiedad de la clase.
        /// </summary>
        public Boolean IgnorarPropiedad { get; set; }
        

        ///Gris
        /// <summary>
        /// Determina si la propiedad es de búsqueda.
        /// </summary>
        public Boolean EsPropiedadBusqueda { get; set; }


        ///Gris
        /// <summary>
        /// Indica si la propiedad es el identificador principal.
        /// </summary>
        public Boolean EsIdPrincipal { get; set; }


        ///Gris
        /// <summary>
        /// Propiedad que indica si la propiedad de la clase es requerida.
        /// </summary>
        public Boolean EsRequerido { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Object Valor { get; set; }

        ///Gris
        /// <summary>
        /// Enuemración del tipo de dato CRM.
        /// </summary>
        public eTipoDatoCRM TipoDatoCRM { get; set; }

        ///Gris
        /// <summary>
        /// Tipo de dato de la propiedad.
        /// </summary>
        public Type TipoDato{ get; set; }


        ///Gris
        /// <summary>
        /// Propiedad para almacenar  las condiciones de búsqueda.
        /// </summary>
        public List<ConditionExpression> CondicionesDeBusqueda { get; set; }


        ///Gris
        /// <summary>
        /// 
        /// </summary>
        public List<List<ConditionExpression>> CondicionesDeRangos { get; set; }

        ///Gris
        /// <summary>
        /// Propiedad que almacena el valor que viene desde crm de la propiedad.
        /// </summary>
        public object ValorCRM { get; set; }


        ///Gris
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="propiedad">Propiedad de la clase.</param>
        /// <param name="entidad">Entidad crm a la que pertenece la propiedad.</param>
        public InfoPropiedad(PropertyInfo propiedad, EntidadCrm entidad)
        {
            this.Propiedad = propiedad;
            this.Entidad = entidad;
            this.NombrePropiedad = propiedad.Name;
            Inicializar();
            AsignarAtributos();
            ObtenerValorPropiedad();
            ConstruirConditionExpression();
        }


        ///Gris
        /// <summary>
        /// Asigna el tipo de dato de crm.
        /// </summary>
        /// <param name="entidad"></param>
        public void ObtenerPropiedadesCRM(Entity entidad)
        {
            AttributeCollection coleccion = entidad.Attributes;

            try
            {
                if (!String.IsNullOrEmpty(this.NombreEsquema))
                {
                    if (coleccion.Keys.Contains(this.NombreEsquema))
                    {
                        this.ValorCRM = coleccion[this.NombreEsquema];

                        //- Variables locales utilizadas para guardar los valores de cada tipo de dato.
                        EntityReference valorLookup = null;
                        EntityCollection valorCollection = null;
                        Money valorMoney = null;
                        OptionSetValue valorOption = null;
                        Boolean valorBool = false;
                        Decimal valorDecimal = Decimal.Zero;
                        Int32 valorEntero32 = -1;
                        Int64 valorEntero64 = -1;
                        Guid valorGuid = Guid.Empty;
                        DateTime valorDate = DateTime.MinValue;




                        if (this.ValorCRM != null)
                        {
                            //Se valida que el tipo de dato contenga EntityReference.
                            if (this.ValorCRM.ToString().Contains("Microsoft.Xrm.Sdk.EntityReference"))
                            {
                                valorLookup = (EntityReference)this.ValorCRM;
                            }

                            //Se valida que el tipo de dato contenga Money.
                            if (this.ValorCRM.ToString().Contains("Microsoft.Xrm.Sdk.Money"))
                            {
                                valorMoney = (Money)this.ValorCRM;
                            }

                            //Se valida que el tipo de dato contenga OptionSetValue.
                            if (this.ValorCRM.ToString().Contains("Microsoft.Xrm.Sdk.OptionSetValue"))
                            {
                                valorOption = (OptionSetValue)this.ValorCRM;
                            }

                            //Se valida que el tipo de dato contenga EntityCollection paralos tipos de dato Activiy Party.
                            if (this.ValorCRM.ToString().Contains("Microsoft.Xrm.Sdk.EntityCollection"))
                            {
                                valorCollection = (EntityCollection)this.ValorCRM;
                            }


                            //Conversiones de Entity Reference a Lookup
                            if (this.TipoDato.FullName.Contains("CrmLookup"))
                            {
                                if (valorLookup != null)
                                {
                                    //Se extraen los valores de valorLookup.
                                    CrmLookup cl = new CrmLookup();
                                    cl.ID = valorLookup.Id;
                                    cl.Nombre = valorLookup.Name;
                                    cl.TipoEntidad = valorLookup.LogicalName;

                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = cl;
                                }
                            }


                            //Conversiones de Entity Reference a ActivityParty
                            if (this.TipoDato.FullName.Contains("CrmActivityParty"))
                            {
                                if (valorCollection != null && valorCollection.Entities != null && valorCollection.Entities.Count > 0)
                                {
                                    //TODO: Implementar mapeo Activity Party.
                                    CrmActivityParty cap = null;

                                    foreach (Entity ent in valorCollection.Entities)
                                    {
                                        if (ent.Attributes.Contains("partyid"))
                                        {
                                            if(cap == null)
                                                cap = new CrmActivityParty();

                                            CrmLookup cl = new CrmLookup()
                                            {
                                                ID = ((EntityReference)ent.Attributes["partyid"]).Id,
                                                TipoEntidad = ((EntityReference)ent.Attributes["partyid"]).LogicalName,
                                                Nombre = ((EntityReference)ent.Attributes["partyid"]).Name
                                            };

                                            cap.Agregar(cl);
                                        }
                                    }

                                    if(cap != null)
                                        this.Valor = cap;
                                }
                            }

                            //44.- Conversión de CrmEntityReference a Decimal
                            if (this.TipoDato.FullName.Contains("Decimal"))
                            {
                                try
                                {
                                    //45.- Asignar el valor a la propiedad de la clase.
                                    this.Valor = valorMoney.Value;
                                }
                                catch (Exception ex)
                                {
                                    //46.- En caso de que suceda un error, se intenta de otra forma.
                                    if (decimal.TryParse(this.ValorCRM.ToString(), out valorDecimal))
                                    {
                                        //47.- Asignar el valor a la propiedad de la clase.
                                        this.Valor = valorDecimal;
                                    }
                                }

                            }

                            //Conversiones de Entity Reference a Lookup
                            if (this.TipoDato.FullName.Contains("CrmPicklist"))
                            {
                                if (valorOption != null)
                                {
                                    //Se extraen los valores de valorLookup.
                                    CrmPicklist cl = new CrmPicklist();
                                    cl.ID = valorOption.Value;
                                    if (entidad.FormattedValues.Count>0)
                                        cl.Nombre = entidad.FormattedValues[this.NombreEsquema].ToString();

                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = cl;
                                }
                            }

                            //Conversiones de OperadoresComparacion
                            if (this.TipoDato.BaseType.FullName.Contains("OperadoresComparacion")) 
                            {
                                //Obtengo el tipo de dato del objeto OperadoresComparacion. ej. Fecha, Entero
                                Type type = this.TipoDato;      
                          
                                //creo una instancia de ese tipo de dato, y es el que será devuelto.
                                var obj = Activator.CreateInstance(type);

                                //Obtengo el tipo de dato que la clase ocupa.
                                Type dataType = ((OperadoresComparacion)obj).TipoDato.GetType();

                                //Convierto el valor devuelto por CRM, al tipo de dato que la clase necesita.
                                ((OperadoresComparacion)obj).Valor = Convert.ChangeType(this.ValorCRM.ToString(), dataType);

                                //Asigno el valor a la propiedad.
                                this.Valor = obj;


                                //TODO: Borrar el siguiente código, si lo de arriba funciona para todos los casos.
                                //if (DateTime.TryParse(this.ValorCRM.ToString(), out valorDate))
                                //{
                                //    //Se extraen los valores de valorLookup.
                                //    Fecha fecha = new Fecha(valorDate);
                                //    //fecha.Valor = valorDate;
                                //    //Asignar el valor a la propiedad de la clase.
                                //    this.Valor = fecha;
                                //}
                                
                            }                          


                            //Asignación a tipo String
                            if (this.TipoDato.FullName.Contains("String"))
                            {
                                this.Valor = this.ValorCRM;
                            }

                            //Conversión de String a Boolean
                            if (this.TipoDato.FullName.Contains("Boolean"))
                            {
                                //Se trata de convertir el valor obtenido en valorPropiedadActual a Bool
                                if (Boolean.TryParse(this.ValorCRM.ToString(), out valorBool))
                                {
                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = valorBool;
                                }
                            }

                            //Conversión de String a DateTime
                            if (this.TipoDato.FullName.Contains("DateTime"))
                            {
                                //Se trata de convertir el valor obtenido en valorPropiedadActual a DateTime
                                if (DateTime.TryParse(this.ValorCRM.ToString(), out valorDate))
                                {
                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = valorDate;
                                }
                            }

                            //Conversión de String a Guid
                            if (this.TipoDato.FullName.Contains("Guid"))
                            {
                                //Se trata de convertir el valor obtenido en valorPropiedadActual a Guid
                                if (Guid.TryParse(this.ValorCRM.ToString(), out valorGuid))
                                {
                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = valorGuid;
                                }
                            }

                            //Conversión de String a Entero
                            if (this.TipoDato.FullName.Contains("Int32"))
                            {
                                if (valorOption != null)
                                {
                                    this.Valor = valorOption.Value;
                                }
                                else
                                {
                                    //Se trata de convertir el valor obtenido en valorPropiedadActual a Int32
                                    if (int.TryParse(this.ValorCRM.ToString(), out valorEntero32))
                                    {
                                        //Asignar el valor a la propiedad de la clase.
                                        this.Valor = valorEntero32;
                                    }
                                }
                            }

                            //Conversión de String a Entero 64 bits
                            if (this.TipoDato.FullName.Contains("Int64"))
                            {
                                //e trata de convertir el valor obtenido en valorPropiedadActual a Int64
                                if (Int64.TryParse(this.ValorCRM.ToString(), out valorEntero64))
                                {
                                    //Asignar el valor a la propiedad de la clase.
                                    this.Valor = valorEntero64;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }


        }

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            NombreEsquema = null;
            NombreParaMostrar = null;
            TipoControlWeb = null;
            NombreEsquemaDeReferencia = null;
            IgnorarPropiedad = false;
            EsPropiedadBusqueda = false;
            EsIdPrincipal = false;
            EsRequerido = false;
            Valor = null;
            TipoDatoCRM = eTipoDatoCRM.None;
            TipoDato = null;
            CondicionesDeBusqueda = null;
            CondicionesDeRangos = null;
            registrar = new RegistroError();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AsignarAtributos()
        {
            TipoDato = this.Propiedad.PropertyType;

            //Obtiene los atributos de esta propiedad
            object[] atributosDePropiedad = this.Propiedad.GetCustomAttributes(true);

            //Se verifica que haya atributos personalizados en la propiedad.
            //Si no cuenta con atributos personalizados no se toma en cuenta para el mapeo.
            if (atributosDePropiedad != null && atributosDePropiedad.Length > 0)
            {
                //Recorre los atributos declarados en esta propiedad
                foreach (object attr in atributosDePropiedad)
                {
                    //Verifica que el atributo sea el nombre de esquema.
                    if (attr.GetType() == typeof(NombreEsquemaCrm))
                    {
                        //Asigna el nombre de esquema a la propiedad.
                        this.NombreEsquema = ((NombreEsquemaCrm)attr).NombreEsquema.ToLower();
                    }

                    //Verifica que el atributo sea el nombre para mostrar.
                    if (attr.GetType() == typeof(NombreParaMostrar))
                    {
                        //Asigna el nombre para mostrar a la propiedad.
                        this.NombreParaMostrar = ((NombreParaMostrar)attr).NombreMostrar;
                    }

                    //Verifica que el atributo sea tipo de control web.
                    if (attr.GetType() == typeof(ControlWebFormulario))
                    {
                        //Asigna el tipo de control de la propiedad.
                        this.TipoControlWeb = ((ControlWebFormulario)attr).NombreTipoControlWeb;
                    }

                    //Verifica que el atributo sea el Tipo de Dato CRM
                    if (attr.GetType() == typeof(TipoDatoCrm))
                    {
                        //Se obtiene el tipo de Dato CRM
                        this.TipoDatoCRM = ((TipoDatoCrm)attr).NombreTipoDatoCRM;
                        this.NombreEsquemaDeReferencia = ((TipoDatoCrm)attr).EntidadRelacionadaCRM;
                    }

                    //Verifica que el atributo sea Ignorar.
                    if (attr.GetType() == typeof(Ignorar))
                    {
                        //Asigna el valor a la variable.
                        this.IgnorarPropiedad = ((Ignorar)attr).IgnorarPropiedad;
                    }

                    // Verifica que el atributo sea el IdentificadorCRM.
                    if (attr.GetType() == typeof(IdentificadorCRM))
                    {
                        //Identifica esta propiedad como identificador principal.
                        this.EsIdPrincipal = ((IdentificadorCRM)attr).EsIDPrincipal;
                    }

                    //Verifica que el atributo sea PropiedadBusqueda.
                    //Que Actua como un PlaceHolder hacer la búsqueda de ese valor
                    if (attr.GetType() == typeof(PropiedadBusqueda))
                    {
                        //Asigna el valor a la variable.
                        this.EsPropiedadBusqueda = ((PropiedadBusqueda)attr).EsPropiedadBusqueda;
                    }

                    //TODO: AQUÍ SE IMPLEMENTARAN NUEVOS ATRIBUTOS...
                }
            }
            else
            {
                //Si la propiedad no cuenta con ningun atributo, entonces es ignorada.
                this.IgnorarPropiedad = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ObtenerValorPropiedad()
        {
            if ((   !string.IsNullOrEmpty(this.NombreEsquema) || 
                    !string.IsNullOrWhiteSpace(this.NombreEsquema)) && 
                    !this.IgnorarPropiedad || this.EsPropiedadBusqueda)
                        {
                            //Se obtiene el valor de la propiedad actual de la entidad.
                            this.Valor = this.Propiedad.GetValue(this.Entidad, null);
                        }

        }

        /// <summary>
        /// 
        /// </summary>
        private void ConstruirConditionExpression()
        {
            ConditionExpression CondicionDeBusqueda = null;

            //Valida que se haya definido un valor para la propiedad.
            if(this.Valor != null)
            {
                //Valida que no se ignore la propiedad.
                if(!this.IgnorarPropiedad)
                {
                    //Valida que se haya definido un nombre de esquema para la propiedad.
                    if(!string.IsNullOrEmpty(this.NombreEsquema))
                    {
                        CondicionesDeBusqueda = new List<ConditionExpression>();

                        CondicionDeBusqueda = new ConditionExpression();
                        CondicionDeBusqueda.AttributeName = this.NombreEsquema;

                        if (this.TipoDato.FullName.Contains("String"))
                        {
                            if (this.Valor.ToString().Contains("|"))
                            {
                                string[] values = this.Valor.ToString().Split('|');

                                foreach (string value in values)
                                {
                                    CondicionDeBusqueda = new ConditionExpression();
                                    CondicionDeBusqueda.AttributeName = this.NombreEsquema;
                                    CondicionDeBusqueda.Operator = ConditionOperator.Like;
                                    CondicionDeBusqueda.Values.Add(String.Format("{0}", value));
                                    CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                                }

                            }
                            else
                            {

                                CondicionDeBusqueda.Operator = ConditionOperator.Like;
                                CondicionDeBusqueda.Values.Add(String.Format("{0}", this.Valor));
                                CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                            }
                        }
                        else if(this.TipoDato.BaseType.FullName.Contains("OperadoresComparacion"))  //this.TipoDato.FullName.Contains("Fecha"))
                        {
                            #region CondicionesDeFecha


                            OperadoresComparacion _valor = (OperadoresComparacion)this.Valor;
                            String valorString = null;

                            if (_valor.Valor != null)
                                valorString = _valor.ToString();

                            switch(_valor.TipoComparacion)
                            {
                                case eComparacion.Simple:
                                #region SIMPLE
                                    if (_valor.Operador == eOperador.MayorQue)
                                    {
                                        CondicionDeBusqueda.Operator = ConditionOperator.GreaterThan;
                                        CondicionDeBusqueda.Values.Add(valorString);
                                    }

                                    if (_valor.Operador == eOperador.MayorIgualQue)
                                    {
                                        CondicionDeBusqueda.Operator = ConditionOperator.GreaterEqual;
                                        CondicionDeBusqueda.Values.Add(valorString);
                                    }

                                    if (_valor.Operador == eOperador.MenorQue)
                                    {
                                        CondicionDeBusqueda.Operator = ConditionOperator.LessThan;
                                        CondicionDeBusqueda.Values.Add(valorString);
                                    }

                                    if (_valor.Operador == eOperador.MenorIgualQue)
                                    {
                                        CondicionDeBusqueda.Operator = ConditionOperator.LessEqual;
                                        CondicionDeBusqueda.Values.Add(valorString);
                                    }

                                    CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                                #endregion
                                break;

                                case eComparacion.Rango:
                                #region RANGO
                                    if (_valor.Rango.Count == 2)
                                    {
                                        CondicionesDeRangos = new List<List<ConditionExpression>>();
                                        List<ConditionExpression> listaRangos = new List<ConditionExpression>();

                                        object valorInicio = _valor.Rango[0];
                                        object valorFin = _valor.Rango[1];

                                        String valorStringIquierdo = valorInicio.GetType().FullName.Contains("DateTime") ? ((DateTime)valorInicio).ToString("s") : valorInicio.ToString();
                                        String valorStringDerecho = valorFin.GetType().FullName.Contains("DateTime") ? ((DateTime)valorFin).ToString("s") : valorFin.ToString();

                                        if (_valor.TipoRango == eTipoRango.EntreRango)
                                        {
                                            #region MÉTODOS ENTRE
                                            if (_valor.OperadorRango == eRango.Incluidos)
                                            {
                                                //Incluye los dos valores del rango

                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.GreaterEqual;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);  
                                                
                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.LessEqual));
                                            }

                                            if (_valor.OperadorRango == eRango.IncluidoIzquierda)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.GreaterEqual;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.LessThan));
                                            }

                                            if (_valor.OperadorRango == eRango.IncluidoDerecha)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.GreaterThan;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.LessEqual));
                                            }

                                            if (_valor.OperadorRango == eRango.Excluidos)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.GreaterThan;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.LessThan));
                                            }
                                            #endregion
                                        }

                                        if (_valor.TipoRango == eTipoRango.FueraRango)
                                        {
                                            #region MÉTODOS FUERA

                                            if (_valor.OperadorRango == eRango.Incluidos)
                                            {
                                                //Incluye los dos valores del rango

                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.LessEqual;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.GreaterEqual));
                                            }

                                            if (_valor.OperadorRango == eRango.IncluidoIzquierda)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.LessEqual;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.GreaterThan));
                                            }

                                            if (_valor.OperadorRango == eRango.IncluidoDerecha)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.LessThan;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.GreaterEqual));
                                            }

                                            if (_valor.OperadorRango == eRango.Excluidos)
                                            {
                                                //Asigna el valor izquierdo del rango.
                                                CondicionDeBusqueda.Operator = ConditionOperator.LessThan;
                                                CondicionDeBusqueda.Values.Add(valorStringIquierdo);
                                                listaRangos.Add(CondicionDeBusqueda);

                                                //Asigna el valor derecho del rango
                                                listaRangos.Add(ObtenerValorDerecho(valorStringDerecho, this.NombreEsquema, ConditionOperator.GreaterThan));
                                            }

                                            #endregion
                                        }

                                        CondicionesDeRangos.Add(listaRangos);
                                    }
                                #endregion
                                break;

                                case eComparacion.RangoDiscreto:

                                break;

                                default:
                                    CondicionDeBusqueda.Operator = ConditionOperator.Equal;
                                    CondicionDeBusqueda.Values.Add(_valor.ToString());
                                    CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                                break;
                            }

                            #endregion
                        }
                        else if(this.TipoDato.FullName.Contains("CrmPicklist"))
                        {
                            if(((CrmPicklist)this.Valor).ID != null) {
                                CondicionDeBusqueda.Operator = ConditionOperator.Equal;
                                CondicionDeBusqueda.Values.Add(((CrmPicklist)this.Valor).ID.Value);
                                CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                            }
                        }
                        else if (this.TipoDato.FullName.Contains("CrmLookup"))
                        {
                            CondicionDeBusqueda.Operator = ConditionOperator.Equal;
                            CondicionDeBusqueda.Values.Add(((CrmLookup)this.Valor).ID.Value.ToString());
                            CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                        }
                        else
                        {
                            CondicionDeBusqueda.Operator = ConditionOperator.Equal;
                            CondicionDeBusqueda.Values.Add(this.Valor);
                            CondicionesDeBusqueda.Add(CondicionDeBusqueda);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valorStringDerecho"></param>
        /// <param name="NombreEsquema"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        private ConditionExpression ObtenerValorDerecho(String valorStringDerecho, string NombreEsquema, ConditionOperator operador)
        {
            ConditionExpression CondicionDeBusqueda = new ConditionExpression();

            CondicionDeBusqueda = new ConditionExpression();
            CondicionDeBusqueda.AttributeName = NombreEsquema;
            CondicionDeBusqueda.Operator = operador;
            CondicionDeBusqueda.Values.Add(valorStringDerecho);

            return CondicionDeBusqueda;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public void RegistrarError(string Mensaje, Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex, EventoID, NumeroLinea, Metodo, ArchivoOrigen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public void RegistrarError(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Ex, EventoID, NumeroLinea, Metodo, ArchivoOrigen);
        }


    }
}
