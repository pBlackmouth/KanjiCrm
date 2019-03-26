using PruebasWeb._1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebasWeb
{
    public partial class ConsultarTareasa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using(Contexto ctx  = new  Contexto()){

                List<Tarea> lista = ctx.ObtenerListado<Tarea>();

                var i = 1;
            
            }
            
            


        }
    }
}