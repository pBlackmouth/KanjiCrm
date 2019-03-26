using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades.Excepciones;
using PruebasWeb.Modelos;
using Tim.Crm.ProveedorServicio;
using System.Configuration;
using Microsoft.Xrm.Sdk.Client;
using Tim.Crm.Base.Entidades.Sistema;
using Tim.Crm.Base.Entidades.Interfases;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Logica;
using Tim.Crm.AdministradorErrores;

namespace PruebasWeb
{

    public partial class ConsultaOrdenada : System.Web.UI.Page, IObservador
    {
        RegistroError registrar = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            registrar = new RegistroError();
            Observador.RegistrarObservador(this);

            //Se inicializa el Contexto de Trabajo.
            using (Contexto ctx = new Contexto())
            {
                if (ctx != null)
                {
                    try
                    {
                        Cuenta cuenta = new Cuenta()
                        {
                            DireccionCiudad = "%Ovie%",
                            Nombre = "%fa%",
                            OperadorOR = true,
                            OrdenarPor = "Nombre,DireccionCiudad"
                        };

                        List<Cuenta> cuentas = ctx.ObtenerListado<Cuenta>(cuenta);

                        String jsonCTA = cuentas.FormatoJSON<Cuenta>();                        

                        GridView1.DataSource = cuentas;
                        GridView1.DataBind();
                    }
                    catch (TIMException ex)
                    {
                        ctx.GuardarError(ex);
                    }

                }
            }
          




        }


        public void ReportarError(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}