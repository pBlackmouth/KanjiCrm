using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Tim.Crm.Base.Logica.Base;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk.Client;
using PruebasWeb.Modelos;
using Tim.Crm.Base.Entidades.Excepciones;
using Microsoft.Xrm.Sdk;

namespace PruebasWeb.Logica
{
    /// <summary>
    /// Clase que provee la lógica necesaria para consultas que el SDK TIM CRM no proveea nativamente.
    /// Esta clase hereda de ClaseLogica que implementa una Interfase IDisposable
    /// </summary>
    public class CLError : ClaseLogica
    {

        public CLError(IOrganizationService servicio)
            : base(servicio)
        {

        }


        public void ObtenerError()
        {
            try
            {
                throw new Exception("Error provocado a proposito.");
            }
            catch(Exception ex)
            {
                //TestRegistroErrorException Ex = new TestRegistroErrorException("BBB",ex);
                RegistrarError(ex);
            }
        }


        //TODO: Implementar los métodos aquí.


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