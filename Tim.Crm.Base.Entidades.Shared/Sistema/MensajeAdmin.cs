using System;
using Tim.Crm.Base.Entidades.Atributos;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [Obsolete("Esta entidad ya no es utilizada para la versión 2013 de CRM. Utilize MensajeError.")]
    [NombreEsquemaCrm("new_mensajeadmin")]
    public partial class MensajeAdmin : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public MensajeAdmin()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public MensajeAdmin(Entity entidad)
            : base(entidad)
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public MensajeAdmin(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
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
        [NombreEsquemaCrm("new_mensajeadminid")]
        public Guid? MensajeAdminID { get; set; }

        //TODO: Definir las propiedades todos los tipos de datos deben ser nullables, a excepción de String y Objetos.

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            MensajeAdminID = null;
            //TODO: Inicializar todas las propiedades en NULL.
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
