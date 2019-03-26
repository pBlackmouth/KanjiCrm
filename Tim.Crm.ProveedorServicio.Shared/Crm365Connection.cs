using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.ProveedorServicio
{
    public class Crm365SimpleConnection
    {
        private CrmServiceClient _crmClient;
        private IOrganizationService _orgService;
        public Crm365SimpleConnection(string connectionString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _crmClient = new CrmServiceClient(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);

            // Cast the proxy client to the IOrganizationService interface.
            _orgService = (IOrganizationService)_crmClient.OrganizationWebProxyClient != null ? (IOrganizationService)_crmClient.OrganizationWebProxyClient : (IOrganizationService)_crmClient.OrganizationServiceProxy;
        }

        public IOrganizationService GetOrganizationService()
        {
            return _orgService;
        }
    }
}
