using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Logica;
using Tim.Crm.Base.Entidades;
using Tim.Crm.ProveedorServicio;
using PruebasWeb.Modelos;
using PruebasWeb.Logica;

namespace PruebasWeb
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
            Crm365SimpleConnection conector = new Crm365SimpleConnection(Constantes.ConfiguracionActual);
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
            public Cuenta ObtenerCuenta()
            {
                using (CLCuenta lCuenta = new CLCuenta(this.service))
                {
                    return lCuenta.ObtenerCuenta();
                }
            }

            public List<Cuenta> ObtenerCuentas()
            {
                using (CLCuenta lCuenta = new CLCuenta(this.service))
                {
                    return lCuenta.ObtenerCuentas();
                }
            }

            public void ObtenerError()
            {
                using (CLError lCuenta = new CLError(this.service))
                {
                    lCuenta.ObtenerError();
                }
            }

            public List<ArticuloBC> ObtenerArticulos(String ContenidoArticulo, Guid AsuntoID)
            {
                using (CLArticuloBC lArticulo = new CLArticuloBC(this.service))
                {
                    return lArticulo.ObtenerArticuloBC(ContenidoArticulo, AsuntoID);
                }
            }
        }
    }
}