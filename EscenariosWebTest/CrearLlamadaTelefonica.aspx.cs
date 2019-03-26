using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb
{
    public partial class CrearLlamadaTelefonica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Contexto ctx = new Contexto())
            {
                // Usuario: A5362A07-85F5-4399-9B30-0E873B2F27FA - admin@DevCR1.onmicrosoft.com - Francisco Bocanegra
                Usuario user = new Usuario(new Guid("A5362A07-85F5-4399-9B30-0E873B2F27FA"));

                //Contacto: adc555bb-8073-e611-80ee-c4346bac7abc - someone_b@example.com - Ana Trujillo (ejemplo)
                Contacto contact = new Contacto()
                {
                    CorreoElectronico = "someone_b@example.com"
                };

                List<Contacto> lista = ctx.ObtenerListado<Contacto>(contact);

                //Contacto contact = new Contacto(new Guid("A5362A07-85F5-4399-9B30-0E873B2F27FA"));

                PhoneCall call = new PhoneCall();
                call.Asunto = "Llamada de prueba 2 desde Kanji .NET";               
                call.De.Agregar(user.Lookup());
                call.Para.Agregar(lista[0].Lookup());

                Respuesta resp = ctx.Guardar(call);


            }
        }
    }
}