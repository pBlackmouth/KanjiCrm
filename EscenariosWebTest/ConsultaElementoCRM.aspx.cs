using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tim.Crm.Base.Logica;
using Tim.Crm.Base.Entidades;
using Microsoft.Xrm.Sdk;
using PruebasWeb;
using Microsoft.Xrm.Sdk.Query;
using System.Diagnostics;
using PruebasWeb.Modelos;


namespace PruebasWeb
{
    public partial class ConsultaElementoCRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //Mapeo de Entity a EntidadCrm
            Entity ent = new Entity("account");
            ent["ownerid"] = new EntityReference("systemuser", Guid.NewGuid());
            ent["creditlimit"] = new Money(107.5M);
            ent["customertypecode"] = new OptionSetValue(1);

            Cuenta cuenta = new Cuenta(ent);


            Contexto ctx = new Contexto();

            //Métodos personalizados.
            Cuenta c = ctx.Ext.ObtenerCuenta();
            List<Cuenta> cts = ctx.Ext.ObtenerCuentas();



            cts = ctx.ObtenerListado<Cuenta>();

            //Instancia de búsqueda.
            Usuario usuario = new Usuario()
            {
                NombreDominioUsuario = "Nombre"
                //Link = Link<RolSeguridad> ( new Parametros("","") )
               
            };

            List<Usuario> usuarios = ctx.ObtenerListado<Usuario>(usuario);

            

            //Cuenta cuenta = new Cuenta()
            //{
            //    Nombre = "TI-M",
            //    DireccionCiudad = "Monterrey"
            //};

            //Respuesta resp = ctx.Guardar(cuenta);

            //if (resp.Completado.Value)
            //{
            //    cuenta.ID = resp.ID;
            //    cuenta = ctx.ObtenerElemento<Cuenta>(cuenta);
            //}

            //resp = ctx.Eliminar(cuenta);

            //ctx.Asignar(new Usuario(), cuenta);


            Cuenta cuenta2 = new Cuenta()
            {
                DireccionCiudad = "Oviedo",
                Nombre = "Fabri%",
                OperadorOR = true,
                VistaAConsultar = new ColumnasDeMetodo(Cuenta.NombreyDescripcion)
            };

            List<Cuenta> cuentas = ctx.ObtenerListado<Cuenta>(cuenta2);

        }


       
    }
}