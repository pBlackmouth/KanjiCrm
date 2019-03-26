using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebasWeb
{
    public partial class DeserializarObjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string llamada = "<LlamadaTelefonica><Motivo>LlamadaDeSeguimientoDePromocionesEspeciales</Motivo></LlamadaTelefonica>";
            string prospecto = "<Prospecto><Nombres>Diana Domaine </Nombres><ApellidoPaterno>De León </ApellidoPaterno><ApellidoMaterno>Aranda </ApellidoMaterno><MedioContacto>Teléfono</MedioContacto><EMail>domadeleon@outlook.com</EMail><Telefono>8721258430</Telefono><Pais>Mexico</Pais><EstadoProvincia>Coahuila</EstadoProvincia><Ciudad>Saltillo</Ciudad><CampusInteres>G</CampusInteres><Programa>mgn10v-cpop</Programa><IdActividad>MELMX</IdActividad><Proveedor>MKX</Proveedor></Prospecto>";

            //prospecto = "<Prospecto><Nombres>Astrid</Nombres><ApellidoPaterno>Mendoza</ApellidoPaterno><ApellidoMaterno>Esquivel</ApellidoMaterno><MedioContacto>Teléfono</MedioContacto><EMail>mendozpa@gmail.com</EMail><Telefono>525517594227</Telefono><Celular>0445517594227</Celular><Pais>Mexico</Pais><EstadoProvincia>Distrito Federal</EstadoProvincia><CampusInteres>Q</CampusInteres><Programa>ENT11-CPXX</Programa><IdActividad>MAILICON</IdActividad><Proveedor>MKX</Proveedor></Prospecto>";

            Prospecto p = new Prospecto(prospecto);

            LlamadaTelefonica llam = new LlamadaTelefonica(llamada);

         

        }
    }
}