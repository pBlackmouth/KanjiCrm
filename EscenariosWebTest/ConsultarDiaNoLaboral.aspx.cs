using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace PruebasWeb
{
    public partial class ConsultarDiaNoLaboral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Contexto ctx = new Contexto())
            {
                //DateTime fecha = new DateTime(2014, 9, 16);
                //DiaNoLaboral diaNoLaboral = ctx.Util.EsDiaNoLaboral(fecha);

                //Usuario user = new Usuario()
                //{
                //    NombreDominioUsuario = @"TIMOVIL\franciscoba"
                //};


                //List<Usuario> users = ctx.ObtenerListado<Usuario>(user);

                //if(users != null && users.Count > 0)
                //{
                //    HorarioLaboral lista = ctx.Util.ObtenerHorarioLaboralPorUsuario(users[0], DateTime.Now, true, 15);

                //    DateTime? fechaCalc = lista.CalcularFechaProgramada(DateTime.Now, 1100, eDias.Habiles);
                //}

                

                Usuario user = new Usuario()
                {
                    ID = new Guid("CE618A38-1FA3-E311-940A-00155D01082E")
                };

                HorarioLaboral lista = ctx.Util.ObtenerHorarioLaboralPorUsuario(user, DateTime.Now, true, 7);

                DateTime? fechaCalc = lista.CalcularFechaProgramada(DateTime.Now, 720, eDias.Habiles);
            }
        }
    }
}