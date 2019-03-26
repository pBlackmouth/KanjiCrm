using System;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Atributos;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace PruebasWeb.Modelos
{
    [Serializable]
    [NombreEsquemaCrm("account")]
    public partial class Cuenta : EntidadCrm
    {
               
        public Cuenta()
        {
            Inicializar();
        }

        public Cuenta(Entity entity) 
            : base(entity)
        {
        } 

        public Cuenta(string JSON)
            :base(JSON,true)
        {

        }

        private void Inicializar()
        {
            CuentaID = null;
            Nombre = null;
            Descripcion = null;
            LimiteCredito = null;
            CuentaConCredito = null;
            Propietario = null;
            TipoDeRelacion = null;
            DireccionCiudad = null;
            CorreoElectronico = null;
        }

        

        [IdentificadorCRM]
        [NombreParaMostrar("Cuenta ID")]
        [NombreEsquemaCrm("accountid")]
        public Guid? CuentaID { get; set; }
       

        [Requerido]
        [NombreParaMostrar("Nombre")]
        [NombreEsquemaCrm("name")]
        public string Nombre { get; set; }

        [NombreParaMostrar("Descripción")]
        [NombreEsquemaCrm("description")]
        public string Descripcion { get; set; }

        [NombreParaMostrar("Cuenta con crédito")]
        [NombreEsquemaCrm("creditonhold")]
        public bool? CuentaConCredito { get; set; }

        [NombreParaMostrar("Límite de crédito")]
        [NombreEsquemaCrm("creditlimit")]
        [TipoDatoCrm(eTipoDatoCRM.Money, null)]
        public Decimal? LimiteCredito { get; set; }


        [NombreParaMostrar("Propietario")]
        [NombreEsquemaCrm("ownerid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "systemuser")]
        public CrmLookup Propietario { get; set; }

        [NombreParaMostrar("Tipo de relación")]
        [NombreEsquemaCrm("customertypecode")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist TipoDeRelacion { get; set; }

        [NombreParaMostrar("Ciudad")]
        [NombreEsquemaCrm("address1_city")]
        public string DireccionCiudad { get; set; }

        [NombreParaMostrar("Correo electrónico")]
        [NombreEsquemaCrm("emailaddress1")]
        public string CorreoElectronico { get; set; }


       

       
     



        #region Vistas


        public static string[] NombreyDescripcion()
        {
            Cuenta entidad = new Cuenta()
            {
                Nombre = "",
                Descripcion = ""
            };

            return entidad.AtributosConValor();
        }

        #endregion


    }
}