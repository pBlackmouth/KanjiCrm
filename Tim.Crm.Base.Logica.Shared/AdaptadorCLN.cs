using System;
using System.Collections.Generic;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk;



namespace Tim.Crm.Base.Logica
{
    /// <summary>
    /// 
    /// </summary>
    public class AdaptadorCLN : Acciones, IDisposable
    {
        //TODO: Validar con el uso, si es necesario conservar esta clase.

        /// <summary>
        /// 
        /// </summary>
        public Utilerias Util { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AdaptadorCLN(IOrganizationService service)
            :base(service)
        {
            Util = new Utilerias(service);
            ////AppDomain currentDomain = AppDomain.CurrentDomain;
            ////currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        public AdaptadorCLN()
        {
            Util = null;
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOrganizationService ObtenerServicio()
        {
            return service;
        }

        

        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        //Implement IDisposable.
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                service = null;

                // Set large fields to null.
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        /// <summary>
        /// 
        /// </summary>
        ~AdaptadorCLN()
        {
            // Simply call Dispose(false).
            Dispose (false);
        }
    }
}
