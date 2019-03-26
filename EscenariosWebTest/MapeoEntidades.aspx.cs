using Microsoft.Xrm.Sdk;
using PruebasWeb.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebasWeb
{
    public partial class MapeoEntidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Mapeo de Entity a EntidadCrm
            Entity ent = new Entity("account");
            ent["ownerid"] = new EntityReference("systemuser", Guid.NewGuid());
            ent["creditlimit"] = new Money(100.0M);
            ent["customertypecode"] = new OptionSetValue(1);

            Cuenta cuenta = new Cuenta(ent);

            cuenta.LimiteCredito = 5000.0M;
            cuenta.Nombre = "Mi cuenta";

            ent = cuenta.EntidadCRM();






        }
    }
}