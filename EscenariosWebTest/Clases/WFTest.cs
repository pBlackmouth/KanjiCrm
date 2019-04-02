using Microsoft.Xrm.Sdk;
using PruebasWeb;
using System;
using System.Collections.Generic;
using System.Web;
using Tim.Crm.Base.Entidades.WorkflowActivities;

namespace EscenariosWebTest.Clases
{
    public class WFTest : CodeActivityBase
    {
        public override void Execute()
        {
            try
            {
                Trace("Starting plugin");

                var service = this.xrmWorkflowContext.OrganizationService;    //IOrganizationService                 
                var context = this.xrmWorkflowContext.WorkflowContext; //IPluginExecutionContext

                Contexto ctx = new Contexto(service);


            }
            catch (Exception ex)
            {
                //TODO: Register error here

                //TODO: Change error message.
                throw new InvalidPluginExecutionException("WORKFLOW ACTIVITY ERROR:", ex);
            }
        }
    }
}