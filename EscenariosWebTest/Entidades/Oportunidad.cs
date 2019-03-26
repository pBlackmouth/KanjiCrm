using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;

namespace PruebasWeb._1.Modelos
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("opportunity")]
    public partial class Oportunidad : EntidadCrm
    {

        #region CONSTRUCTORES

        public Oportunidad()
            : base()
        {
            Inicializar();
        }

        public Oportunidad(Guid ID)
            : base()
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Oportunidad(Entity entidad)
            : base(entidad)
        {

        }

        public Oportunidad(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        [IdentificadorCRM]
        [NombreEsquemaCrm("opportunityid")]
        public Guid? OportunidadID { get; set; }

        [NombreEsquemaCrm("name")]
        public String Nombre { get; set; }

        [NombreEsquemaCrm("parentaccountid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "account")]
        public CrmLookup Cuenta { get; set; }

        [NombreEsquemaCrm("parentcontactid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "contact")]
        public CrmLookup Contacto { get; set; }

        [NombreEsquemaCrm("purchasetimeframe")]
        [TipoDatoCrm(eTipoDatoCRM.OptionSetValue, null)]
        public CrmPicklist IntervaloTiempoCompra { get; set; }

        [NombreEsquemaCrm( "budgetamount")]
        public Decimal? ImportePresupuesto { get; set; }


        #endregion

        #region MÉTODOS PRIVADOS

        private void Inicializar()
        {
            OportunidadID = null;
            Nombre = null;
            Cuenta = null;
            Contacto = null;
            IntervaloTiempoCompra = null;
            ImportePresupuesto = null;

        }

        #endregion

        #region MÉTODOS PÚBLICOS

        public static string[] IdNombre()
        {
            Oportunidad vista = new Oportunidad()
            {
                OportunidadID = Guid.Empty,
                Nombre = ""
            };
            return vista.AtributosConValor();
        }

        #endregion

    }
}
