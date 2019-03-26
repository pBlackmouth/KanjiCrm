using System;
using System.Collections.Generic;
using Tim.Crm.Base.Plugins;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using Microsoft.Xrm.Sdk;

namespace $rootnamespace$
{
	public class $safeitemrootname$: PluginBase
	{
        public $safeitemrootname$() : base("LOGICAL_ENTITY_NAME") { }

        public override void Execute()
        {
            try
            {
                Trace("Starting plugin");

                var service = this.xrmPluginContext.OrganizationService;    //IOrganizationService                 
                var context = this.xrmPluginContext.PluginExecutionContext; //IPluginExecutionContext
                Contexto ctx = new Contexto(service);

                //TODO: >>>> Implement code here.

            }
            catch (Exception ex)
            {
                //TODO: Register error here

                //TODO: Change error message.
                throw new InvalidPluginExecutionException("PLUGIN ERROR:", ex);
            }
        }
    }
}
