using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebasWeb
{
    public partial class ConsultarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Contexto ctx = new Contexto())
            {

                Usuario user = new Usuario()
                {
                    CorreoElectronico = "administrator2@ti-m.com.mx",
                    OperadorOR = false

                };

                List<Usuario> users = ctx.ObtenerListado<Usuario>(user);

            }
        }
    }
}