using System;
using Tim.Crm.Base.Logica;
using Microsoft.Xrm.Sdk;

namespace $rootnamespace$
{
	public partial class Contexto : AdaptadorCLN
{

    public Extensiones Ext { get; set; }


    public Contexto(IOrganizationService service)
        : base(service)
    {
        Ext = new Extensiones(this);
    }

    public class Extensiones
    {
        private IOrganizationService service = null;
        private Contexto ctx = null;
        OrganizationServiceContext osc = null;

        public Extensiones(Contexto ctx)
        {
            this.ctx = ctx;
            this.service = ctx.service;
        }
    }
}
}
