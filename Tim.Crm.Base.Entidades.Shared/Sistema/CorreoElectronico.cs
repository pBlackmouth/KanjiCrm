using System;
using Tim.Crm.Base.Entidades.Atributos;
using Tim.Crm.Base.Entidades.Enumeraciones;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("email")]
    public partial class CorreoElectronico : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public CorreoElectronico()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public CorreoElectronico(Guid ID)
        {
            this.ID = ID;
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public CorreoElectronico(Entity entidad)
            : base(entidad)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public CorreoElectronico(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("activityid")]
        public Guid? CorreoElectronicoID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("from")]
        [TipoDatoCrm(eTipoDatoCRM.ActivityParty, null)]
        public CrmActivityParty De { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("to")]
        [TipoDatoCrm(eTipoDatoCRM.ActivityParty, null)]
        public CrmActivityParty Para { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("subject")]
        public String Asunto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("directioncode")]
        public Boolean? CorreoDeSalida { get; set; }
        


        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            CorreoElectronicoID = null;
            De = null;
            Para = null;
            Asunto = null;
            Descripcion = null;
            CorreoDeSalida = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
