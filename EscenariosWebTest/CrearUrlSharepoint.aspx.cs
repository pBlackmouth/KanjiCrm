using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Sistema;

namespace PruebasWeb
{
    public partial class CrearUrlSharepoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using(Contexto ctx = new Contexto())
            {
                UbicacionDocumento ud = new UbicacionDocumento()
                {
                    Nombre = "%Prueba1%"                    
                };

                List<UbicacionDocumento> lista = ctx.ObtenerListado<UbicacionDocumento>(ud);

                UbicacionDocumento ud2 = lista[1];
                ud2.ID = null;

                Contacto con = new Contacto()
                {
                    Nombres = "Francisco",
                    Apellidos = "%Bocanegra%"
                };

                List<Contacto> listaCon = ctx.ObtenerListado<Contacto>(con);

                if(listaCon != null && listaCon.Count > 0)
                {
                    ud2.ReferenteA = new CrmLookup() { ID = listaCon[0].ContactoID };
                    ud2.URLRelativa = listaCon[0].NombreCompleto;
                    ud2.Nombre = "Prueba 4 Contacto";                    
                }

                ctx.Guardar(ud2);
                
                string x = "";
            }
        }
    }
}