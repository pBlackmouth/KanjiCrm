using System;
using System.Collections.Generic;
using System.Linq;
using Tim.Crm.Base.Logica.Base;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace PruebasWeb
{
    /// <summary>
    /// Clase que provee la lógica necesaria para consultas que el SDK TIM CRM no proveea nativamente.
    /// Esta clase hereda de ClaseLogica que implementa una Interfase IDisposable
    /// </summary>
    public class CLArticuloBC : ClaseLogica
    {

        public CLArticuloBC(IOrganizationService servicio)
            : base(servicio)
        {

        }


        public List<ArticuloBC> ObtenerArticuloBC(String ContenidoCuerpo, Guid AsuntoId)
        {
            List<ArticuloBC> lista = null;


            SearchByBodyKbArticleRequest searchByBodyRequest =
            new SearchByBodyKbArticleRequest()
            {
                SubjectId = AsuntoId,
                UseInflection = true, // allows for a different tense or 
                // inflection to be substituted for the search text
                SearchText = "contains", // will also match on 'contains'
                QueryExpression = new QueryExpression()
                {
                    ColumnSet = new ColumnSet("articlexml"),
                    EntityName = "kbarticle"
                }
            };

            SearchByBodyKbArticleResponse seachByBodyResponse = (SearchByBodyKbArticleResponse)servicio.Execute(searchByBodyRequest);

            var retrievedArticleBodies = seachByBodyResponse.EntityCollection.Entities.Select((entity) => (new ArticuloBC(entity)));

            if (retrievedArticleBodies.Count() > 0)
            {
                lista = new List<ArticuloBC>();
                foreach (EntidadCrm entidad in retrievedArticleBodies)
                {
                    lista.Add((ArticuloBC)entidad);
                }
            }

            return lista;
        }


        #region IMPLEMENTACIÓN IDISPOSABLE
        //Mover a esta implementación solo al estar seguro de ello.


        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.

                disposed = true;
            }
            base.Dispose(disposing);
        }
        // The derived class does not have a Finalize method
        // or a Dispose method without parameters because it inherits
        // them from the base class.

        #endregion

    }
}