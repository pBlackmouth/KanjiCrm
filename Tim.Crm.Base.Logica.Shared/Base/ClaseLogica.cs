using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades.Sistema;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Logica.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ClaseLogica : IDisposable, IRegistroError
    {
        /// <summary>
        /// 
        /// </summary>
        protected IOrganizationService servicio;

        /// <summary>
        /// 
        /// </summary>
        private RegistroError registrar = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicio"></param>
        public ClaseLogica(IOrganizationService servicio)
        {
            this.servicio = servicio;

            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);

            registrar = new RegistroError();
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
                servicio = null;
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        /// <summary>
        /// 
        /// </summary>
        ~ClaseLogica()
        {
            // Simply call Dispose(false).
            Dispose (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public virtual void RegistrarError(string Mensaje, Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Mensaje, Ex, EventoID, NumeroLinea, Metodo, ArchivoOrigen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public virtual void RegistrarError(Exception Ex, 
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            registrar.Error(Ex, EventoID, NumeroLinea, Metodo, ArchivoOrigen);
        }
    }
}
