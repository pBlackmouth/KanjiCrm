using System;
using System.Activities;
using System.Collections.ObjectModel;

using Microsoft.Crm.Sdk.Messages;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

namespace Tim.Crm.Base.Entidades.WorkflowActivities
{
    /// <summary>
    /// Workflow Context Wrapper
    /// </summary>
    public class WorkflowActivityContext
    {
        /// <summary>
        /// WorkflowActivityContext Constructor
        /// </summary>
        /// <param name="executionContext"></param>
        public WorkflowActivityContext(CodeActivityContext executionContext)
        {
            if (executionContext == null)
            {
                throw new ArgumentNullException("executionContext");
            }

            // Obtain the execution context service from the service provider.
            this.WorkflowContext = executionContext.GetExtension<IWorkflowContext>();

            // Obtain the tracing service from the service provider.
            this.TracingService = executionContext.GetExtension<ITracingService>();

            // Obtain the Organization Service factory service from the service provider
            var serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();

            // Use the factory to generate the Organization Service.
            this.OrganizationService = serviceFactory.CreateOrganizationService(WorkflowContext.UserId);
        }

        #region Properties

        /// <summary>
        /// Gets the organization service.
        /// </summary>
        /// <value>
        /// The organization service.
        /// </value>
        public IOrganizationService OrganizationService { get; private set; }

        /// <summary>
        /// Gets the plugin execution context.
        /// </summary>
        /// <value>
        /// The plugin execution context.
        /// </value>
        public IWorkflowContext WorkflowContext { get; private set; }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the tracing service.
        /// </summary>
        /// <value>
        /// The tracing service.
        /// </value>
        public ITracingService TracingService { get; private set; }

    
        #endregion


        #region Methods

        /// <summary>
        /// The trace.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Trace(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || this.TracingService == null)
            {
                return;
            }

            this.TracingService.Trace(message);            
        }

        #endregion
    }
}
