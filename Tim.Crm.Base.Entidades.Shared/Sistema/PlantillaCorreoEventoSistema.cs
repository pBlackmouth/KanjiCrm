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
    [NombreEsquemaCrm("new_plantillacorreoeventosistema")]
    public partial class PlantillaCorreoEventoSistema : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public PlantillaCorreoEventoSistema()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public PlantillaCorreoEventoSistema(Entity entidad)
            : base(entidad)
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public PlantillaCorreoEventoSistema(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
        //    : base(ObjectoSerializado, ObtenerValoresDesdeString)
        //{

        //}

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("new_plantillacorreoeventosistemaid")]
        public Guid? PlantillaCorreoEventoSistemaID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_name")]
        public String Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_asunto")]
        public String Asunto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("new_descripcion")]
        public String Descripcion { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            PlantillaCorreoEventoSistemaID = null;
            Nombre = null;
            Asunto = null;
            Descripcion = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
