using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public  class InfoEntidad : IRegistroError
    {
        ///Gris
        /// <summary>
        /// Campo que hace referencia a entidadCRM
        /// </summary>
        private EntidadCrm Entidad;

        ///Gris
        /// <summary>
        /// Propiedad que se utiliza para poder hacer uso del registro de errores.
        /// </summary>
        private RegistroError registrar = null;

        ///Gris
        /// <summary>
        /// Lista de propiedades TIM de la clase obtenidas a través de reflection.
        /// </summary>
        public List<InfoPropiedad> PropiedadesTIM { get; set; }

        
        /// <summary>
        /// Variable que contiene los atibutos personalizados declarados en la clase de la entidad.
        /// </summary>
        public AttributeCollection PropiedadesCRM { get; set; }

        ///Gris
        /// <summary>
        /// Propiedad utilizada para almacenar el nombre de esquema de la entidad
        /// </summary>
        public String NombreEsquema { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public Type TipoEntidadTIM { get; set; }

        
        public string JsonExtendido { get; set; }



        
        ///Gris
        /// <summary>
        /// Constructor que recibe como parametro una entidadCRM
        /// </summary>
        /// <param name="entidad"></param>
        public InfoEntidad(EntidadCrm entidad)
        {
            Inicializar();
            this.Entidad = entidad;
            this.TipoEntidadTIM = entidad.GetType();
            ObtenerNombreEsquemaClaseTIM(this.TipoEntidadTIM);            

        }


        ///Gris
        /// <summary>
        /// Inicializa las propiedades de la clase en null, excepto el registrador que es instanciado.
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            PropiedadesTIM = null;
            PropiedadesCRM = null;
            NombreEsquema = null;
            TipoEntidadTIM = null;
            registrar = new RegistroError();
        }


        ///Gris
        /// <summary>
        /// Método utilizado para obtener el nombre de esquema de la de la clase TIM
        /// </summary>
        /// <param name="tipoEntidad"></param>
        private void ObtenerNombreEsquemaClaseTIM(Type tipoEntidad)
        {
            //Se obtienen los atributos personalizados definidos para la clase entidad.
            object[] atributosEntidadTIM = tipoEntidad.GetCustomAttributes(true);

            //Se verifica que se hayan encontrado atributos personalizados en la entidad
            if (atributosEntidadTIM != null && atributosEntidadTIM.Length > 0)
            {
                //Se recorren los atributos definidos en la entidad.
                foreach (object attr in atributosEntidadTIM)
                {
                    //Se comparan los atributos y se trata de obtener el nombre de esquema.
                    if (attr.GetType() == typeof(NombreEsquemaCrm))
                    {
                        //Se obtiene el nombre de esquema de la entidad.
                        this.NombreEsquema = ((NombreEsquemaCrm)attr).NombreEsquema;
                    }
                }
            }
        }


        ///Gris
        /// <summary>
        /// Método utilizado para asignar las propiedades TIM a  través de reflection  por medio del tipo de entidad CRM.
        /// </summary>
        public void AsignarPropiedadesTIM()
        {
            PropiedadesTIM = new List<InfoPropiedad>();

            foreach(PropertyInfo prop in TipoEntidadTIM.GetProperties())
            {
                PropiedadesTIM.Add(new InfoPropiedad(prop, this.Entidad));
            }
        }

        ///Gris
        /// <summary>
        /// Método  utilizado para asignar las propiedades CRM a partir de la Entity recibida
        /// </summary>
        /// <param name="entidad"></param>
        public void AsignarPropiedadesCRM(Entity entidad)
        {
            PropiedadesCRM = entidad.Attributes;

            foreach(InfoPropiedad propiedadActual in this.PropiedadesTIM)
            {
                propiedadActual.ObtenerPropiedadesCRM(entidad);
            }
        }

        /// <summary>
        /// Obtiene una lista de los nombres de esquema definidos en cada propiedad de la clase.
        /// </summary>
        /// <returns>Lista de atributos.</returns>
        public String[] Atributos()
        {
            List<string> lista = null;

            //Valida que se hayan definido las propiedades de la entidad.
            if(PropiedadesTIM != null)
            {
                lista = new List<string>();

                //Se recorren las propiedades de la clase para obtener los nombres de esquema.
                foreach(InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    //Verifica que esta propiedad no sea ignorada.
                    if(!propiedadActualTIM.IgnorarPropiedad)
                    {
                        //13.- Valida que se haya obtenido un nombre de esquema.
                        if (!string.IsNullOrEmpty(propiedadActualTIM.NombreEsquema))
                        {
                            //Se agrega el nombre de esquema la lista de atributos.
                            lista.Add(propiedadActualTIM.NombreEsquema);
                        }
                    }
                }

            }

            return lista == null ? null : lista.ToArray();
        }

        /// <summary>
        /// Obtiene los nombres de los atributos, de las propiedades de clase que son distintos de Nulo.
        /// </summary>
        /// <returns>Listado de los nombre lógicos de los atributos CRM.</returns>
        public String[] AtributosConValor()
        {
            List<string> lista = null;

            //Valida que se hayan definido las propiedades de la entidad.
            if (PropiedadesTIM != null)
            {
                lista = new List<string>();

                //Se recorren las propiedades de la clase para obtener los nombres de esquema.
                foreach (InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    if (propiedadActualTIM.Valor != null)
                    {
                        //Verifica que esta propiedad no sea ignorada.
                        if (!propiedadActualTIM.IgnorarPropiedad)
                        {
                            //13.- Valida que se haya obtenido un nombre de esquema.
                            if (!string.IsNullOrEmpty(propiedadActualTIM.NombreEsquema))
                            {
                                //Se agrega el nombre de esquema la lista de atributos.
                                lista.Add(propiedadActualTIM.NombreEsquema);
                            }
                        }
                    }
                }

            }

            return lista == null ? null : lista.ToArray();
        }

         /// <summary>
        /// Verifica que las propiedades definidas como requeridas contengan valor,
        /// de no ser así se lanza una excepción.
        /// </summary>
        public void VerificarRequeridos()
        {
            //Valida que se hayan definido las propiedades de la entidad.
            if (PropiedadesTIM != null)
            {
                //Se recorren las propiedades de la clase para obtener los nombres de esquema.
                foreach (InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    //Verifica que esta propiedad no sea ignorada.
                    if (!propiedadActualTIM.IgnorarPropiedad)
                    {
                        //Valida que se haya obtenido un nombre de esquema.
                        if (!string.IsNullOrEmpty(propiedadActualTIM.NombreEsquema))
                        {
                            //Valida que sea una ppropiedad requerida y que sea nula
                            if(propiedadActualTIM.EsRequerido && propiedadActualTIM.Valor == null)
                            {
                                throw new MapeoEntidadException(String.Format("El atributo {0} tiene un valor nulo y está definido como requerido.", propiedadActualTIM.NombreEsquema));
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Obtiene un arreglo de condiciones, de las propiedades que contengan valor.
        /// </summary>
        /// <returns>Array de ConditionExpression</returns>
        public ConditionExpression[] ObtenerArregloDeCondiciones()
        {
            List<ConditionExpression> condiciones = null;

            if(PropiedadesTIM != null)
            {
                condiciones = new List<ConditionExpression>();

                foreach(InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    if(propiedadActualTIM.CondicionesDeBusqueda != null)
                    {
                        condiciones.AddRange(propiedadActualTIM.CondicionesDeBusqueda);
                    }
                }
            }

            return condiciones== null ? null : condiciones.ToArray();
        }

        /// <summary>
        /// Obtiene un arreglo de condiciones de randos, de las propiedades que contengan valor.
        /// </summary>
        /// <returns>Array de ConditionExpression</returns>
        public ConditionExpression[] ObtenerArregloDeCondicionesDeRangos()
        {
            List<ConditionExpression> condiciones = null;

            if (PropiedadesTIM != null)
            {
                condiciones = new List<ConditionExpression>();

                foreach (InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    if (propiedadActualTIM.CondicionesDeRangos != null)
                    {
                        foreach(List<ConditionExpression> lista in propiedadActualTIM.CondicionesDeRangos)
                        {
                            if(lista != null && lista.Count > 0)
                            {
                                condiciones.AddRange(lista);
                            }
                        }
                    }
                }
            }

            return condiciones == null ? null : condiciones.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordenamientos"></param>
        /// <returns></returns>
        public OrderExpression[] ObtenerOrdenamientos(List<Orden> ordenamientos)
        {
            List<OrderExpression> expresionesDeOrdenamiento = null;
            OrderExpression item = null;

            if (PropiedadesTIM != null)
            {
                expresionesDeOrdenamiento = new List<OrderExpression>();

                foreach (Orden elem in ordenamientos)
                {
                    InfoPropiedad info = PropiedadesTIM.Find(p => p.Propiedad.Name.ToLower() == elem.Propiedad.ToLower());

                    if(info != null)
                    {
                        item = new OrderExpression()
                        {
                            AttributeName = info.NombreEsquema,
                            OrderType =  elem.EsAscendente ? OrderType.Ascending : OrderType.Descending
                        };

                        expresionesDeOrdenamiento.Add(item);
                    }
                    else
                    {
                        throw new ExpresionDeOrdenamientoException(string.Format("No existe una propiedad llamada: {0}", elem.Propiedad));
                    }
                }
            }

            return expresionesDeOrdenamiento == null ? null : expresionesDeOrdenamiento.ToArray();
        }

        public string ExtenderJSON(bool ConFormato = false )
        {
            string NombresEsquema = "\"NombreEsquemaCrm\": {";
            string NombresMostrar = "\"NombreParaMostrar\": {";
            string ControlWeb = "\"TipoControlWeb\": {";

            if (PropiedadesTIM != null)
            {
                int index = 0;
                foreach (InfoPropiedad propiedadActualTIM in PropiedadesTIM)
                {
                    
                    if (ConFormato)
                    {
                        //TODO: Implementar JSON formateado.
                    }
                    else
                    {
                        if(index == 0)
                        {
                            if (propiedadActualTIM.NombreEsquema != null)
                                NombresEsquema = string.Format("\"NombreEsquemaCrm\": {{ \"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.NombreEsquema);

                            if (propiedadActualTIM.NombreParaMostrar != null)
                                NombresMostrar = string.Format("\"NombreParaMostrar\": {{ \"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.NombreParaMostrar);

                            if (propiedadActualTIM.TipoControlWeb != null)
                                ControlWeb = string.Format("\"TipoControlWeb\": {{ \"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.TipoControlWeb);
                        }
                        else
                        {
                            if (propiedadActualTIM.NombreEsquema != null)
                                NombresEsquema += string.Format(",\"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.NombreEsquema);

                            if (propiedadActualTIM.NombreParaMostrar != null)
                                NombresMostrar += string.Format(",\"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.NombreParaMostrar);

                            if (propiedadActualTIM.TipoControlWeb != null)
                                ControlWeb += string.Format(",\"{0}\":\"{1}\"", propiedadActualTIM.NombrePropiedad, propiedadActualTIM.TipoControlWeb);
                        }
                    }
                    
                    index++;

                }
                
            }

            NombresEsquema += " }";
            NombresMostrar += " }";
            ControlWeb += " }";


            return string.Format(",{0},{1},{2},\"Listo\":\"true\"", NombresEsquema, NombresMostrar, ControlWeb);
        }


        ///Gris
        /// <summary>
        /// Método que ayuda a registrar si se presenta un error, incluyendo un mensaje
        /// </summary>
        /// <param name="Mensaje">Mensaje de error.</param>
        /// <param name="Ex"></param>
        /// <param name="EventoID">Identificador del evento.</param>
        /// <param name="NumeroLinea">Línea donde ocurrio el error.</param>
        /// <param name="Metodo">Método donde ocurrio el error.</param>
        /// <param name="ArchivoOrigen">Archivo origen donde ocurrio el error.</param>
        public void RegistrarError(string Mensaje, Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex);
        }

        /// <summary>
        /// Método que registra un error o excepción presentada.
        /// </summary>
        /// <param name="Ex">Excepción o error generado.</param>
        /// <param name="EventoID">Identificador del evento.</param>
        /// <param name="NumeroLinea">Número de línea donde ocurrio el error.</param>
        /// <param name="Metodo">Método donde ocurrio el error.</param>
        /// <param name="ArchivoOrigen">Archivo de origen donde ocurrio el error.</param>
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
