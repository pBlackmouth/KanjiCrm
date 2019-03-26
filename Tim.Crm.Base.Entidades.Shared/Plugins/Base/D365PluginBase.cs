using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Tim.Crm.Base.Entidades.Base;
using im.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        internal Entity Target { get; set; }
        internal string EntityLogicalName;

        internal PluginContext xrmPluginContext;

        public PluginBase(string EntityLogicalName)
        {
            this.EntityLogicalName = EntityLogicalName;
        }
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                xrmPluginContext = new PluginContext(serviceProvider, true);

                if (xrmPluginContext != null)
                {
                    // The InputParameters collection contains all the data passed in the message request.
                    if (xrmPluginContext.PluginExecutionContext.InputParameters.Contains("Target") && xrmPluginContext.PluginExecutionContext.InputParameters["Target"] is Entity)
                        Target = (Entity)xrmPluginContext.PluginExecutionContext.InputParameters["Target"];

                    // Verify that the target entity represents an account.
                    // If not, this plug-in was not registered correctly.
                    if (Target.LogicalName != EntityLogicalName)
                        return;


                    this.Execute();
                } else
                {
                    throw new NullPluginContextException("There was an error creating Kanji Plugin Context.");
                }
            }            
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Trace("Kanji plugin execution fail: {0}", ex.Message);
                throw new Exception("Kanji plugin execution fail.", ex);                
            }
            catch (TIMException ex)
            {
                var errorMessage = ex.PilaDeDescripciones;
                Trace("Kanji plugin execution fail: {0}", ex.PilaDeDescripciones);
                throw new InvalidPluginExecutionException("Kanji plugin execution fail: {0}", ex);
            }
            catch (Exception ex)
            {
                Trace("Kanji plugin execution fail: {0}", ex.Message);
                throw new InvalidPluginExecutionException("Kanji plugin execution fail: {0}",ex);
            }
        }

        internal void Trace(string Message, params string[] Parameters)
        {
            if(xrmPluginContext != null)
                xrmPluginContext.Trace(string.Format(Message, Parameters));
        }

        public abstract void Execute();
    }
}
