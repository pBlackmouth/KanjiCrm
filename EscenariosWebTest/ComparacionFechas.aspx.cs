using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PruebasWeb.Modelos;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace PruebasWeb
{

    public partial class ComparacionFechas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se inicializa el Contexto de Trabajo.
            Contexto ctx = new Contexto();

            if (ctx != null)
            {

                Contacto contacto = new Contacto()
                {       
                    NoHijos = Entero.MayorIgualQue(2)
                    //Cumpleanos = Fecha.MayorIgualQue(new DateTime(1969,1,3))
                    //,Estatus = new CrmPicklist(1)
                };

                //Cumpleanos = Fecha.Entre(new DateTime(1969,1,3), new DateTime(1983,4,8), eRango.Incluidos),

                List<Contacto> lista = ctx.ObtenerListado<Contacto>(contacto);


                if (lista != null)
                {
                    foreach (Contacto con in lista)
                    {
                        DateTime fechaC = con.Cumpleanos.Valor;
                    }
                }

            }
        }
    }
}