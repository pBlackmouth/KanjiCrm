using System;
using System.Collections.Generic;
using System.Linq;
using Tim.Crm.Base.Logica.Base;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Sistema;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using PruebasWeb.Modelos;
using Microsoft.Xrm.Sdk;

namespace PruebasWeb.Logica
{
    public class CLCuenta : ClaseLogica
    {

        //TODO: Francisco, este no se de donde sale. Lo defini aqui para que dejara de marcar error
        OrganizationServiceContext orgContext = null;

        public CLCuenta(IOrganizationService servicio)
            : base(servicio)
        {
            this.orgContext = new OrganizationServiceContext(servicio);
        }

        public Cuenta ObtenerCuenta()
        {
            Cuenta item = null;

            try
            {
                item = (from a in orgContext.CreateQuery("account")
                        where ((String)a["address1_city"]).Contains("Mad")
                        select new Cuenta(a)).ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {
                RegistrarError(ex);
            }

            return item;
        }

        public List<Cuenta> ObtenerCuentas()
        {
            List<Cuenta> lista = null;

            try
            {
                lista = (from a in orgContext.CreateQuery("account")
                         where ((String)a["address1_city"]).Contains("Ovie")
                         select new Cuenta(a)).ToList();

            }
            catch (Exception ex)
            {
                RegistrarError(ex);
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