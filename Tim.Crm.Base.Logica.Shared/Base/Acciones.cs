using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services.Protocols;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;

namespace Tim.Crm.Base.Logica
{
    /// <summary>
    /// 
    /// </summary>
    public class Acciones : AccionesExt,  IRegistroError
    {
        /// <summary>
        /// 
        /// </summary>
        protected IOrganizationService service = null;
        
        /// <summary>
        /// 
        /// </summary>
        private RegistroError registrar = null;        
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public Acciones(IOrganizationService service)
        {
            this.service = service;
            registrar = new RegistroError();
        }

        /// <summary>
        /// 
        /// </summary>
        public Acciones()
        {
            registrar = new RegistroError();
        }

        /// <summary>
        /// Obtiene un registro de CRM identificado por ID.
        /// </summary>
        /// <param name="entidad">EntidadCrm necesaria para la búsqueda. Requiere que la propiedad ID contenga valor.</param>
        /// <param name="ConsultarVista">(Opcional) Delegado que hace referencia a un método de clase, que sirve como vista.</param>
        /// <returns>Retorna un objeto de tipo Entity de CRM.</returns>
        public virtual T ObtenerElemento<T>(EntidadCrm entidad) where T : EntidadCrm
        {
            Entity item = null;
            T itemT = null;
            try
            {


                    //Verifica que la entidad no sea nula.
                if (entidad != null)
                {
                    //Verifica que el ID no sea nulo.
                    if (entidad.ID != null)
                    {
                        //Variable que contiene las columnas a ser consultadas.
                        ColumnSet cols = new ColumnSet();

                        //Ejecuta el delegado especificado para obtener las columnas reuqeridas de consulta.
                        cols.AddColumns(entidad.VistaAConsultar());


                        if (service != null)
                        {
                            //Hace la consulta a CRM para obtener el elemento.
                            item = service.Retrieve(entidad.NombreEsquema, entidad.ID.Value, cols);
                        }                        
                        else
                        {
                            //TODO: Crear Clase de error para este tipo de excepción.
                            throw new Exception("No existe un objeto de conexión a CRM.");
                        }
                        

                        if (item != null)
                        {
                            itemT = item.TransformarEn<T>();
                        }
                    }
                }
                


            }

            catch(SoapException ex)
            {
                ObtenerElementoException tex = new ObtenerElementoException(ex);
                RegistrarError(tex);
            }
            catch(Exception ex)
            {
                ObtenerElementoException tex = new ObtenerElementoException(ex);
                RegistrarError(tex);
            }

            return itemT;
        }

        /// <summary>
        /// Obtiene un listado de todos los registros de la entidad especificada.
        /// </summary>
        /// <typeparam name="T"> Tipo de entidad a consultar.</typeparam>
        /// <returns>Regresa un listado de registros eliminando el tope de 5000 registros.</returns>
        public List<T> ObtenerListado<T>() where T : EntidadCrm
        {
            T item = (T)Activator.CreateInstance(typeof(T));
            return ObtenerListado<T>(item);
        }

        /// <summary>
        /// Obtiene un listado de registros de CRM con determinados filtros de búsqueda.
        /// </summary>
        /// <param name="entidad">Entidad que actúa como filtros de la búsqueda. Instancia de búsqueda</param>
        /// <param name="ConsultarVista">(Opcional) Delegado que hace referencia a un método de clase, que sirve como vista. Si es null toma la vista por defecto.</param>
        /// <param name="operador">(Opcional) Parámetro que especifíca el tipo de operador de la consulta.</param>
        /// <returns>Retorna un objeto de tipo EntityCollection de CRM.</returns>
        public List<T> ObtenerListado<T>(EntidadCrm entidad) where T : EntidadCrm
        {
            EntityCollection lista = null;
            List<T> listaT = null;
                        
            // Inicializa el número de la página a consultar.
            int noPagina = 1;


            try
            {


                    //Verifica que la entidad no sea nula.
                if (entidad != null)
                {
                    //Objeto que se utiliza para hacer la consulta.
                    QueryExpression query = new QueryExpression();

                    //Se define el tipo de entidad, para el query.
                    query.EntityName = entidad.NombreEsquema;

                    #region COLUMNAS A CONSULTAR

                    //Variable que contiene las columnas a ser consultadas.
                    ColumnSet cols = new ColumnSet();

                    //Ejecuta el delegado especificado para obtener las columnas reuqeridas de consulta.
                    cols.AddColumns(entidad.VistaAConsultar());

                    //Asigno las columnas requeridas.
                    query.ColumnSet = cols;

                    #endregion

                    #region CONDICIONES

                    //Obtengo las condiciones de la entidad.
                    ConditionExpression[] condiciones = entidad.ObtenerArregloDeCondiciones();

                    if (condiciones != null)
                    {
                        //Crea el objeto y se definen las condiciones por medio de las propiedades con valor.
                        FilterExpression filter = new FilterExpression();

                        if (entidad.OperadorOR)
                        {
                            filter.FilterOperator = LogicalOperator.Or;
                            query.Criteria.FilterOperator = LogicalOperator.Or;
                        }
                        else
                        {
                            filter.FilterOperator = LogicalOperator.And;
                            query.Criteria.FilterOperator = LogicalOperator.And;
                        }

                        //Agrego las condiciones al filtro.
                        filter.Conditions.AddRange(condiciones);
                        
                        //Se asigna el filtro a la consulta.
                        query.Criteria.Filters.Add(filter);


                        //Esta sección es para agregar las condiciones de los rangos
                        ConditionExpression[] condicionesDeRangos = entidad.ObtenerArregloDeCondicionesDeRangos();

                        if (condicionesDeRangos != null && condicionesDeRangos.Length > 0)
                        {
                            filter = new FilterExpression();
                            filter.FilterOperator = LogicalOperator.And;

                            //Agrego las condiciones de rango al filtro.
                            filter.Conditions.AddRange(condicionesDeRangos);
                            query.Criteria.Filters.Add(filter);
                        }

                    }

                    #endregion

                    #region ORDENAMIENTO

                    OrderExpression[] ordenamientos = entidad.ObtenerArregloDeOrdenamientos();

                    if (ordenamientos != null)
                    {
                        query.Orders.AddRange(ordenamientos);
                    }

                    #endregion

                    #region CONSULTA PAGINADA

                    //Implementación de consultas páginadas de más de 5000 registros.
                    query.PageInfo = new PagingInfo();

                    //Se asigna la cantidad de registros a ser consultados por página.
                    query.PageInfo.Count = entidad.RegistrosPorPagina;

                    //Se inicializa la página que será consultada.
                    query.PageInfo.PageNumber = noPagina;

                    //para obtener la primera página, pagingCookie debe de ser nulo.
                    query.PageInfo.PagingCookie = null;

                    #endregion


                    #region ENTIDADES RELACIONADAS


                    if (entidad.EntidadesRelacionadas != null && entidad.EntidadesRelacionadas.Count > 0)
                    {

                        foreach (EntidadRelacionada entidadrelacionada in entidad.EntidadesRelacionadas)
                        {

                            LinkEntity linkentity = new LinkEntity();

                            EntidadCrm entidadRelacion = (EntidadCrm)entidadrelacionada.EntidadRelacion;

                            #region PROPIEDADES RELACION
                            linkentity.LinkFromEntityName = entidad.NombreEsquema;
                            linkentity.LinkToEntityName = entidadRelacion.NombreEsquema;
                            linkentity.LinkFromAttributeName = entidadrelacionada.AtributoOrigenRelacion;
                            linkentity.LinkToAttributeName = entidadrelacionada.AtributoDestinoRelacion;
                            linkentity.JoinOperator = entidadrelacionada.TipoRelacion;
                            linkentity.EntityAlias = entidadrelacionada.AliasEntidadRelacion;
                            #endregion

                            #region COLUMNAS A CONSULTAR RELACION

                            //Variable que contiene las columnas a ser consultadas.
                            ColumnSet colsRelacion = new ColumnSet();

                            //Ejecuta el delegado especificado para obtener las columnas reuqeridas de consulta.
                            colsRelacion.AddColumns(entidadRelacion.VistaAConsultar());

                            //Asigno las columnas requeridas.
                            linkentity.Columns = colsRelacion;

                            #endregion


                            #region CONDICIONES RELACION

                            //Obtengo las condiciones de la entidad.
                            ConditionExpression[] condicionesRelacion = entidadRelacion.ObtenerArregloDeCondiciones();

                            if (condicionesRelacion != null)
                            {
                                //Crea el objeto y se definen las condiciones por medio de las propiedades con valor.
                                FilterExpression filterRelacion = new FilterExpression();

                                if (entidadRelacion.OperadorOR)
                                {
                                    filterRelacion.FilterOperator = LogicalOperator.Or;
                                    query.Criteria.FilterOperator = LogicalOperator.Or;
                                }
                                else
                                {
                                    filterRelacion.FilterOperator = LogicalOperator.And;
                                    query.Criteria.FilterOperator = LogicalOperator.And;
                                }

                                //Agrego las condiciones al filtro.
                                filterRelacion.Conditions.AddRange(condicionesRelacion);

                                //Se asigna el filtro a la consulta.
                                linkentity.LinkCriteria.Filters.Add(filterRelacion);


                                //Esta sección es para agregar las condiciones de los rangos
                                ConditionExpression[] condicionesDeRangosRelacion = entidadRelacion.ObtenerArregloDeCondicionesDeRangos();

                                if (condicionesDeRangosRelacion != null && condicionesDeRangosRelacion.Length > 0)
                                {
                                    filterRelacion = new FilterExpression();
                                    filterRelacion.FilterOperator = LogicalOperator.And;

                                    //Agrego las condiciones de rango al filtro.
                                    filterRelacion.Conditions.AddRange(condicionesDeRangosRelacion);
                                    linkentity.LinkCriteria.Filters.Add(filterRelacion);
                                }

                            }

                            #endregion


                            query.LinkEntities.Add(linkentity);

                        }

                    }


                    #endregion


                    while (true)
                    {

                        if (service != null)
                        {
                            // Se hace el retrieve.
                            lista = service.RetrieveMultiple(query);
                        }
                        else                        
                        {
                            //TODO: Crear Clase de error para este tipo de excepción.
                            throw new Exception("No existe un objeto de conexión a CRM.");
                        }
                        

                        // Valido que la lista haya obtenido información.
                        if (lista != null && lista.Entities != null && lista.Entities.Count > 0)
                        {
                            // Hace el mapeo de las entidades Entity de CRM a las entidades de TIM.
                            List<T> listaTempT = lista.TransformarEnListadoDe<T>();

                            // Verifica que se hayan convertido las entidades.
                            if (listaTempT != null && listaTempT.Count > 0)
                            {
                                if (listaT == null)
                                    listaT = new List<T>();

                                // Agrega las entidades convertidas a la lista resultante.
                                listaT.AddRange(listaTempT);
                            }

                            // Verifica que sea necesario otra consulta para obtener más resultados.
                            if (lista.MoreRecords)
                            {
                                // Incrementa el número de página para obtener la página siguiente.
                                query.PageInfo.PageNumber++;

                                // Asigna a la consulta la Cookie de paginación, devuelta por el resultado actual.
                                query.PageInfo.PagingCookie = lista.PagingCookie;
                            }
                            else
                            {
                                // Si ya no hay más registros que obtener
                                // detiene el ciclo del while.
                                break;
                            }

                        }
                        else
                        {
                            break;
                        }
                    }



                }

                

            }

            catch (SoapException ex)
            {
                ObtenerListadoException tex = new ObtenerListadoException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                ObtenerListadoException tex = new ObtenerListadoException(ex);
                RegistrarError(tex);
            }

            return listaT;
        }

        /// <summary>
        /// Crea o actualiza una entidad de CRM
        /// </summary>
        /// <param name="entidad">Entidad TIM.</param>
        /// <returns>Retorno de estado de la acción.</returns>
        public Respuesta Guardar(EntidadCrm entidad)
        {
            Respuesta resp = new Respuesta();

            try
            {


                if (entidad.ID == null)
                {
                    if (entidad.ValidarAtributosRequeridos)
                        entidad.VerificarRequeridos();

                    if (service != null)
                    {
                        //Sección para Crear.
                        resp.ID = service.Create(entidad.EntidadCRM());
                    }                    
                    else
                    {
                        //TODO: Crear Clase de error para este tipo de excepción.
                        throw new Exception("No existe un objeto de conexión a CRM.");
                    }
                    
                }
                else
                {

                    if (service != null)
                    {
                        //Seccion para Actualizar.
                        service.Update(entidad.EntidadCRM());
                    }
                    else
                    {
                        //TODO: Crear Clase de error para este tipo de excepción.
                        throw new Exception("No existe un objeto de conexión a CRM.");
                    }
                    
                }
            }
            catch (TIMException ex)
            {
                GuardarException tex = new GuardarException(ex);
                RegistrarError(tex);
            }
            catch (SoapException ex)
            {
                GuardarException tex = new GuardarException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                GuardarException tex = new GuardarException(ex);
                RegistrarError(tex);
            }

            resp.Completado = true;

            return resp;
        }

        /// <summary>
        /// Eliminar un registro de CRM.
        /// </summary>
        /// <param name="entidad">Entidad TIM que debe de llevar el ID para identificar el registro a eliminar</param>
        /// <returns>Retorno de la petición, indica si se completó la acción</returns>
        public Respuesta Eliminar(EntidadCrm entidad)
        {
            Respuesta resp = new Respuesta();
            try
            {


                if (entidad.ID != null)
                {
                    if (service != null)
                    {
                        service.Delete(entidad.NombreEsquema, entidad.ID.Value);
                    }
                    else
                    {
                        //TODO: Crear Clase de error para este tipo de excepción.
                        throw new Exception("No existe un objeto de conexión a CRM.");
                    }
                    
                }
            }
            catch (SoapException ex)
            {
                EliminarException tex = new EliminarException(ex);
                RegistrarError(tex);
            }
            catch (Exception ex)
            {
                EliminarException tex = new EliminarException(ex);
                RegistrarError(tex);
            }

            resp.Completado = true;

            return resp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asignatario"></param>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Respuesta Asignar(IAsignable asignatario, EntidadCrm entidad)
        {
            Respuesta resp = new Respuesta();
            try
            {

                if (asignatario != null && entidad != null)
                {
                    if (asignatario.Id() != null && entidad.ID != null)
                    {
                        if (!string.IsNullOrEmpty(asignatario.NombreLogico()) && !string.IsNullOrEmpty(entidad.NombreEsquema))
                        {
                            // Create the Request Object and Set the Request Object's Properties
                            AssignRequest assign = new AssignRequest
                            {
                                Assignee = new EntityReference(asignatario.NombreLogico(), asignatario.Id().Value),
                                Target = new EntityReference(entidad.NombreEsquema, entidad.ID.Value)
                            };

                            // Execute the Request
                            service.Execute(assign);

                            resp.Completado = true;
                        }
                    }
                }

                
            }
            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (SoapException ex)
            {
                AsignarException tex = new AsignarException(ex);
                RegistrarError(tex);
            }
            catch(Exception ex)
            {
                AsignarException tex = new AsignarException(ex);
                RegistrarError(tex);
            }

            return resp;
        }

        /*
        public String ObtenerURLBase()
        {
            //TODO: Obtener dinámicamente la URL base de cada sitio.
            return conector.ObtenerUrlBase();//"https://sdktest.nikima.com.mx";
        }
        */
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CrmLookup ObtenerOrganizacion()
        {

            


            CrmLookup item = null;

            //RetrieveOrganizationRequest req = new RetrieveOrganizationRequest();
            //req.UniqueName = "sdktest";
            //RetrieveOrganizationResponse resp = (RetrieveOrganizationResponse)dService.Execute(req);
            
            

            return item;

        }



        //TODO: Implementar método para usar LinkEntity.


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public virtual void RegistrarError(string Mensaje, Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex);
        }

        public virtual void RegistrarError(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Ex);
        }


    }
}
