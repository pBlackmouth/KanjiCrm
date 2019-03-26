using System;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.Base
{
    public abstract class PluginBase : IPlugin
    {
        #region Private Properties
        /// <summary>
        /// Gets or sets the name of the child class.
        /// </summary>
        /// <value>The name of the child class.</value>
        protected string ChildClassName
        {
            get;

            private set;
        }




        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the entity reference.
        /// </summary>
        /// <value>
        ///     The entity reference.
        /// </value>    
        public EntityReference ReferenciaEntidad { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public Entity EntidadPrincipal { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public EntidadCrm Entidad { get; set; }

        /// <summary>
        ///     Gets the plugin context.
        /// </summary>
        /// <value>
        ///     The plugin context.
        /// </value>    
        public PluginContext ContextoLocal { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether [ignore plugin].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [ignore plugin]; otherwise, <c>false</c>.
        /// </value>
        /// <created>1/5/2014</created>
        public bool IgnorarEjecucionPlugin
        {
            get; set;
        }


        public bool EnableTrace { get; set; }

        public bool MostrarErrorAUsuario { get; set; }

        #endregion

        #region Methods



        /// <summary>
        /// Escribe en el trace si está habilitada la opción en ContextoLocal
        /// </summary>
        /// <param name="Mensaje"></param>
        public void Trace(string Mensaje)
        {
            if (this.ContextoLocal.EnableTrace)
                this.ContextoLocal.Trace(Mensaje);
        }

        /// <summary>
        /// Registra una excepción en el Trace.
        /// </summary>
        /// <param name="Mensaje">Mensaje personalizado.</param>
        /// <param name="Exception">Los tipos de excepciones soportados son: FaultException&lt;OrganizationServiceFault&gt;, TIMException, Exception </param>
        /// <param name="MostrarExcepcionAUsuario">Permite mostrar el mensaje de error en la interfaz de CRM. (Como un error)</param>
        public void TraceError(string Mensaje, object Exception, bool MostrarExcepcionAUsuario = false)
        {
            

            if (this.ContextoLocal.EnableTrace)
            {
                Type type = Exception.GetType();

                if (Exception == null)
                {
                    Trace("La excepcion es nula.");

                    if (MostrarExcepcionAUsuario)
                        throw new InvalidPluginExecutionException(Mensaje);

                    return;
                }

                if (type == typeof(FaultException<OrganizationServiceFault>))
                {
                    Trace(string.Format("{0}, Error: {1}", Mensaje, ((FaultException<OrganizationServiceFault>)Exception).Detail.Message));

                    if (MostrarExcepcionAUsuario)
                        throw new InvalidPluginExecutionException("Error", Exception as FaultException<OrganizationServiceFault>);
                    
                }
                else
                if (type == typeof(TIMException))
                {
                    Trace(string.Format("{0}, Pila de descripciones: {1}, Pila de métodos: {2}", Mensaje, ((TIMException)Exception).PilaDeDescripciones ?? "", ((TIMException)Exception).PilaDeMetodos ?? ""));

                    if (MostrarExcepcionAUsuario)
                        throw new InvalidPluginExecutionException("Error", Exception as TIMException);
                }
                else
                if (type == typeof(Exception))
                {
                    Trace(string.Format("{0}, Error: {1}", Mensaje, ((Exception)Exception).Message));

                    if (MostrarExcepcionAUsuario)
                        throw new InvalidPluginExecutionException("Error", Exception as Exception);                 
                }
                else
                    Trace(Mensaje);
            }
        }



        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginBase"/> class.
        /// </summary>
        /// <param name="childClassName">The <see cref="PluginBase" cred="Type"/> of the derived class.</param>
        protected PluginBase(Type TipoClasePlugin, bool HabilitarTrace = false, bool MostrarErrorAUsuario = false, bool IgnorarEjecucionPlugin = false)
        {
            this.ChildClassName = TipoClasePlugin.ToString();
            this.IgnorarEjecucionPlugin = IgnorarEjecucionPlugin;
            this.EnableTrace = EnableTrace;
            this.MostrarErrorAUsuario = MostrarErrorAUsuario;
        }

        #endregion


        /// <summary>
        /// Executes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>    
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {

                #region Obtener Entity
                if (serviceProvider == null)
                {
                    throw new ArgumentNullException("serviceProvider");
                }

                if (this.IgnorarEjecucionPlugin)
                {
                    return;
                }



                this.ContextoLocal = new PluginContext(serviceProvider, this.EnableTrace);


                Trace(string.Format("Entered {0}.Execute()", this.ChildClassName));

                if (!this.ContextoLocal.PluginExecutionContext.InputParameters.Contains("Target"))
                {
                    return;
                }

                EntidadPrincipal = this.ContextoLocal.PluginExecutionContext.InputParameters["Target"] as Entity;

                if (EntidadPrincipal != null)
                {
                    this.ReferenciaEntidad = EntidadPrincipal.ToEntityReference();
                }
                else
                {
                    this.ReferenciaEntidad = this.ContextoLocal.PluginExecutionContext.InputParameters["Target"] as EntityReference;
                    if (this.ReferenciaEntidad == null)
                    {
                        return;
                    }
                }

                #endregion



                //Ejecuta e
                this.Execute();
            }

            catch (FaultException<OrganizationServiceFault> e)
            {
                string mensaje = string.Format("Exception: {0}", e.ToString());
                TraceError(mensaje, e, this.MostrarErrorAUsuario);
                // Handle the exception.
                //throw e;

            }
            catch (Exception e)
            {
                string mensaje = string.Format("Exception: {0}", e.Message);
                TraceError(mensaje, e, this.MostrarErrorAUsuario);
                // Handle the exception.
                //throw e;
            }
            finally
            {
                Trace(string.Format("Exiting {0}.Execute()", this.ChildClassName));
            }
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>    
        public abstract void Execute();



    }


}
