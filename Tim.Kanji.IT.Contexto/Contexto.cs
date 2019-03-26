using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Logica;
using Tim.Crm.Base.Entidades;
using Tim.Crm.ProveedorServicio;


namespace $rootnamespace$
{

    public class Contexto : AdaptadorCLN
    {
        /// <summary>
        /// Agrupador de métodos personalizados.
        /// </summary>
        public Extensiones Ext { get; set; }

        #region CONSTRUCTORES
        public Contexto()
        {
            Crm365SimpleConnection conector = new Crm365SimpleConnection(Constants.ActualConnectionString);
            this.service = conector.GetOrganizationService();
            Util = new Utilerias(this.service);
            Ext = new Extensiones(this);
        }

        public Contexto(IOrganizationService service)
                : base(service)
        {
            Ext = new Extensiones(this);
        }

        #endregion

        #region MÉTODOS PÚBLICOS


        #endregion

        public class Extensiones
        {
            private IOrganizationService service = null;
            private Contexto ctx = null;
            public Extensiones(Contexto ctx)
            {
                this.ctx = ctx;
                this.service = ctx.service;
            }

            //TODO: Aquí se definen los métodos personalizados Ej.
            //public Cuenta ObtenerCuenta()
            //{
            //    using (ServicioCuenta servCuenta = new ServicioCuenta(this.service))
            //    {
            //        return servCuenta.ObtenerCuenta();
            //    }
            //}
        }
    }
}