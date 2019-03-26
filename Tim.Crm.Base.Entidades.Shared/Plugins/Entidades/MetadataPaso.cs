using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades.Plugins
{
    public class MetadataPaso
    {
        /// <summary>
        /// Se refiere al nombre de la DLL registrada en CRM.
        /// </summary>
        public string NombreLibreria { get; set; }

        /// <summary>
        /// Se refiere al nombre del Plugin o Workflow Activity en una librería.
        /// </summary>
        public string NombreEnsamblado { get; set; }


        public string NombrePaso { get; set; }


        public string DescripcionPaso { get; set; }


        /// <summary>
        /// Se refiere a las Step Stage: Pre-Validation,Pre-Operationn o Post-Operation.
        /// </summary>
        public eCrmPluginStepStage FaseEjecucion { get; set; }

        /// <summary>
        /// Prioridad de ejecución.
        /// </summary>
        public int OrdenEjecucion { get; set; }


        /// <summary>
        /// Se refiere a Síncrono o Asíncrono.
        /// </summary>
        public eCrmPluginStepMode ModoEjecucion { get; set; }


        /// <summary>
        /// Indica si el trabajo de sistema asincrónico se elimina automaticamente al finalizarse.
        /// </summary>
        public bool BorrarAutomaticamente { get; set; }

        /// <summary>
        /// Se refiere a si el step se ejecutará en el Servidor o en el cliente de Outlook o en ambos.
        /// </summary>
        public eCrmPluginStepDeployment AmbitoEjecucion { get; set; }


        /// <summary>
        /// Se refiere al mensjae donde se registrará el Step.
        /// </summary>
        public eCrmPluginSdkMessageName MensajeSdk { get; set; }




    }
}
