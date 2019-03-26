using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Logica
{
    public partial class UtileriasMetadata
    {

        /// <summary>
        /// 
        /// </summary>
        IOrganizationService service = null;

        public UtileriasMetadata(IOrganizationService service)
        {
            this.service = service;
        }

        public List<CrmPicklist> ObtenerOptionSetEntidad(string NombreEntidad, string NombreAtributo)
        {
            Metadata meta = new Metadata(this.service);
            return meta.ObtenerOptionSetEntidad(NombreEntidad, NombreAtributo);

        }
    }
}
