using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    public partial class ConsultaCorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(Contexto ctx = new Contexto())
            {
                CorreoElectronico mail = new CorreoElectronico(new Guid("5AC6B2FD-C3CC-E311-9BE6-5457D1E5EB0E"));
                mail = ctx.ObtenerElemento<CorreoElectronico>(mail);
            }
        }
    }
}