using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    public partial class ConsultaArticulosBC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se inicializa el Contexto de Trabajo.
            using (Contexto ctx = new Contexto())
            {       
                List<ArticuloBC> elems = ctx.ObtenerListado<ArticuloBC>();
                List<PlantillaArticuloBC> plantillas = null;

                PlantillaArticuloBC plantilla = null;

                plantillas = ctx.ObtenerListado<PlantillaArticuloBC>();

                plantilla = plantillas.Find(p => p.Titulo.Contains("TIMP00"));

                if (elems != null && elems.Count > 0)
                {
                    foreach (ArticuloBC elem in elems)
                    {
                        plantilla = new PlantillaArticuloBC()
                        {
                            ID = elem.PlantillaArticulo.ID.Value
                        };

                        plantilla = ctx.ObtenerElemento<PlantillaArticuloBC>(plantilla);

                        if(plantilla != null)
                        {
                            if (plantillas == null)
                                plantillas = new List<PlantillaArticuloBC>();

                            plantillas.Add(plantilla);
                        }
                    }
                }

            }

        }
    }
}