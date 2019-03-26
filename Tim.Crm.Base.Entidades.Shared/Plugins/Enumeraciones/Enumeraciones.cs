using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.Enumeraciones
{
    public enum eCrmPluginStepMode
    {
        Asynchronous = 1,
        Synchronous = 0
    }


    /// <summary>
    /// 
    /// </summary>
    public enum eCrmPluginStepStage
    {
        PreValidation = 10,
        PreOperation = 20,
        PostOperation = 40,
        PostOperationDeprecated = 50
    }

    public enum eCrmPluginStepDeployment
    {
        ServerOnly = 0,
        OfflineOnly = 1,
        Both = 2
    }

    public enum eCrmPluginStepInvocationSource
    {
        Parent = 0,
        Child = 1
    }

    public enum eCrmPluginSdkMessageName
    {
        Create,
        Update,
        Delete,
        Retrieve,
        Assign,
        GrantAccess,
        ModifyAccess,
        RetrieveMultiple,
        RetrievePrincipalAccess,
        RetrieveSharedPrincipalsAndAccess,
        RevokeAccess,
        SetState,
        SetStateDynamicEntity,
    }
}
