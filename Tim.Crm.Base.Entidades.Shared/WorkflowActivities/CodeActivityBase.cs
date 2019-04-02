using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.WorkflowActivities
{
    public abstract class CodeActivityBase : CodeActivity
    {

        protected WorkflowActivityContext xrmWorkflowContext;
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                xrmWorkflowContext = new WorkflowActivityContext(context);

                if (xrmWorkflowContext != null)
                {
                    this.Execute();
                }
                else
                {
                    throw new NullWorkflowActivityContextException("There was an error creating Kanji Workflow Activity Context.");
                }

            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Trace("Kanji Workflow Activity execution fail: {0}", ex.Message);
                throw new Exception("Kanji workflow activity execution fail.", ex);
            }
            catch (TIMException ex)
            {
                var errorMessage = ex.PilaDeDescripciones;
                Trace("Kanji plugin execution fail: {0}", ex.PilaDeDescripciones);
                throw new InvalidPluginExecutionException("Kanji workflow activity execution fail: {0}", ex);
            }
            catch (Exception ex)
            {
                Trace("Kanji plugin execution fail: {0}", ex.Message);
                throw new InvalidPluginExecutionException("Kanji workflow activity execution fail: {0}", ex);
            }
        }

        protected void Trace(string Message, params string[] Parameters)
        {
            if (xrmWorkflowContext != null)
                xrmWorkflowContext.Trace(string.Format(Message, Parameters));
        }

        public abstract void Execute();
    }
}
