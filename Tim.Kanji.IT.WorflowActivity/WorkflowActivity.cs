using System;
using System.Web;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using Tim.Crm.Base.Entidades.WorkflowActivities;
using System.Text;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace $rootnamespace$
{
    public class $safeitemrootname$ : CodeActivityBase
    {
        public override void Execute()
        {
            try
            {                
                InitializeAppInsights(AssemblySharedData.AppInsightsInstrumentationKey);
                Trace("Starting Workflow Activity");

                var service = this.xrmWorkflowContext.OrganizationService;    //IOrganizationService                 
                var context = this.xrmWorkflowContext.WorkflowContext; //IPluginExecutionContext

                Contexto ctx = new Contexto(service);

                Trace("Workflow Activity finished");
    }
            catch (Exception ex)
            {

                // Write error on AppInsight
                WriteError(ex.Message);

                //TODO: Change error message.
                throw new InvalidPluginExecutionException("WORKFLOW ACTIVITY ERROR:", ex);
            }
        }
    }
}
