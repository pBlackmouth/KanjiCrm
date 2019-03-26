using PruebasWeb.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebasWeb
{
    public partial class ConsultarEntidadRelacionada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using (Contexto ctx = new Contexto())
            {

                //Cuenta cuenta = new Cuenta()
                //{
                //    CorreoElectronico = "someone9@example.com",
                //    Nombre = "El esquí alpino (ejemplo)",
                //    OperadorOR = true
                //};

                Cuenta cuenta = new Cuenta();

                List<Cuenta> ListaCuentas = ctx.ObtenerListado<Cuenta>();


                Contacto contacto = new Contacto()
                {
                    Nombres = "Luis"
                };

                List<Contacto> ListaContacto = ctx.ObtenerListado<Contacto>(contacto);


                Tim.Crm.Base.Entidades.EntidadRelacionada er = new Tim.Crm.Base.Entidades.EntidadRelacionada();

                er.AliasEntidadRelacion = contacto.NombreEsquema;
                er.AtributoOrigenRelacion = "primarycontactid";
                er.AtributoDestinoRelacion = "contactid";
                er.EntidadRelacion = contacto;
                er.TipoRelacion = Microsoft.Xrm.Sdk.Query.JoinOperator.Inner;

                cuenta.EntidadesRelacionadas.Add(er);

                List<Cuenta> ListaCuentaContacto = ctx.ObtenerListado<Cuenta>(cuenta);

            }

        }
    }
}