using PruebasWeb.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    public partial class ConsultarElementos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Contexto ctx = new Contexto()) {

                Cuenta cuenta = new Cuenta()
                {
                    CorreoElectronico = "someone9@example.com",
                    Nombre = "El esquí alpino (ejemplo)",
                    OperadorOR = true

                };

               

                Respuesta resp = ctx.Guardar(cuenta);
                

                List<Cuenta> ListaCuentas = ctx.ObtenerListado<Cuenta>(cuenta);

            }
        }


        [WebMethod]
        public static List<Cuenta> ObtenerListadoCuentas()
        {
            List<Cuenta> ListaCuentas = null;
            using (Contexto ctx = new Contexto())
            {
                ListaCuentas = ctx.ObtenerListado<Cuenta>();                
            }
            return ListaCuentas;
        }
    }
}