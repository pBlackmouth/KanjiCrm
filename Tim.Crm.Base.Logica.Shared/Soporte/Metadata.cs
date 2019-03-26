using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Logica
{
    public class Metadata
    {
        private IOrganizationService service;

        public Metadata(IOrganizationService service)
        {
            this.service = service;
        }

        public List<CrmPicklist> ObtenerOptionSetGlobal(string optionsetName)
        {
            List<CrmPicklist> lista = null;
            

            try
            {


                RetrieveOptionSetRequest retrieveOptionSetRequest =
                    new RetrieveOptionSetRequest
                    {
                        Name = optionsetName
                    };

                // Execute the request.
                RetrieveOptionSetResponse retrieveOptionSetResponse =
                    (RetrieveOptionSetResponse)service.Execute(retrieveOptionSetRequest);

                // Access the retrieved OptionSetMetadata.
                OptionSetMetadata retrievedOptionSetMetadata = (OptionSetMetadata)retrieveOptionSetResponse.OptionSetMetadata;

                // Get the current options list for the retrieved attribute.
                OptionMetadata[] optionList = retrievedOptionSetMetadata.Options.ToArray();

                if (optionList != null)
                {
                    lista = new List<CrmPicklist>();
                    foreach (OptionMetadata optionMetadata in optionList)
                    {

                        //optionsetSelectedText = optionMetadata.Label.UserLocalizedLabel.Label.ToString();

                        CrmPicklist item = new CrmPicklist();

                        item.ID = optionMetadata.Value;
                        item.Nombre = optionMetadata.Label.UserLocalizedLabel.Label;
                        lista.Add(item);
                    }
                }
            }
            catch (Exception)
            {
            }
            return lista;

        }


        public List<CrmPicklist> ObtenerOptionSetEntidad(string nombreEntidad, string nombreAtributo)
        {            

            List<CrmPicklist> lista = null;

            RetrieveEntityRequest retrieveDetails = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.All,
                LogicalName = nombreEntidad
            };
            RetrieveEntityResponse retrieveEntityResponseObj = (RetrieveEntityResponse)service.Execute(retrieveDetails);

            Microsoft.Xrm.Sdk.Metadata.EntityMetadata metadata = retrieveEntityResponseObj.EntityMetadata;

            Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata picklistMetadata = 
                metadata.Attributes.FirstOrDefault(
                    attribute => String.Equals(attribute.LogicalName, nombreAtributo, StringComparison.OrdinalIgnoreCase)
                ) as Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata;

            Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata options = picklistMetadata.OptionSet;

            IList<OptionMetadata> OptionsList = (from o in options.Options
                                                 select o).ToList();


            if (OptionsList != null)
            {
                lista = new List<CrmPicklist>();
                foreach (OptionMetadata option in OptionsList)
                {
                    CrmPicklist item = new CrmPicklist();

                    item.ID = option.Value;
                    item.Nombre = option.Label.UserLocalizedLabel.Label;
                    lista.Add(item);
                }
            }


            return lista;
        }

    }
}
