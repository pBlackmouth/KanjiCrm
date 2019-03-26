using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PruebasWeb.Modelos;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    [System.Web.Script.Services.ScriptService]
    public partial class KJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //using(Contexto ctx = new Contexto(Constantes.ConfiguracionActual))
            //{

            //    Cuenta cuenta = new Cuenta
            //    {
            //        ID = new Guid("36194BF8-B9A3-E311-940A-00155D01082E")
            //    };

            //    cuenta = ctx.ObtenerElemento<Cuenta>(cuenta);

            //    string resultado = cuenta.KJSON();
            //}
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public static string ObtenerCuenta(string CuentaID, bool kJSON)
        {
            string resultado = null;

            using (Contexto ctx = new Contexto())
            {
                if (CuentaID.IsGuid())
                {
                    Cuenta cuenta = new Cuenta
                    {
                        ID = new Guid(CuentaID)
                    };

                    cuenta = ctx.ObtenerElemento<Cuenta>(cuenta);

                    if(kJSON)
                    {
                        resultado = cuenta.KJSON();
                    }
                    else
                    {
                        resultado = cuenta.JSON();
                    }

                }

            }

            return resultado;

        }
    }
}