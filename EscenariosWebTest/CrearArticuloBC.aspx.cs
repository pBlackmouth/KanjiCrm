using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades.Excepciones;
using Tim.Crm.AdministradorErrores;

namespace PruebasWeb
{
    public partial class CrearArticuloBC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using(Contexto ctx = new Contexto())
                {

                    try
                    {
                        throw new TIMException("Prueba de asociación de Articulo de KB.");
                    }
                    catch(TIMException tex)
                    {
                        ctx.GuardarError(tex);
                    }

                }
            }
            catch(TIMException tex)
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}