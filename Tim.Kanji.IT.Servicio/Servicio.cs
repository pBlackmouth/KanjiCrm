$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;$endif$
using System;
using System.Collections.Generic;
using Tim.Crm.Base.Logica.Base;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Logica;
//using Microsoft.Xrm.Sdk.Client;

namespace $rootnamespace$
{
    /// <summary>
    /// Clase que provee la lógica necesaria para consultas que el SDK TIM CRM no proveea nativamente.
    /// Esta clase hereda de ClaseLogica que implementa una Interfase IDisposable
    /// </summary>
    public class $safeitemname$ : ClaseLogica
    {     
        #region Constructors
            AdaptadorCLN contexto;
            //Descomentar la siguiente linea para utilizar orgContext.CreateQuery en las consultas con LINQ
            //OrganizationServiceContext orgContext = null;

            public $safeitemname$(IOrganizationService servicio)
                :base(servicio)
            {
               //Descomentar la siguiente linea para utilizar orgContext.CreateQuery en las consultas con LINQ
               //this.orgContext = new OrganizationServiceContext(servicio);
            }

            public $safeitemname$(AdaptadorCLN contexto)
                : base(contexto.ObtenerServicio())
            {
                this.contexto = contexto;
                //Descomentar la siguiente linea para utilizar orgContext.CreateQuery en las consultas con LINQ
                //this.orgContext = new OrganizationServiceContext(servicio);
            }
        #endregion

        //TODO: Implementar los métodos aquí.


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    //orgContext.Dispose();
                }
                servicio = null
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~$safeitemname$()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}