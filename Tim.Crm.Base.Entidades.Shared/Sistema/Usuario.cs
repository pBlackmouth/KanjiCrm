using System;
using Tim.Crm.Base.Entidades.Atributos;
using Microsoft.Xrm.Sdk;
using Tim.Crm.Base.Entidades.Enumeraciones;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("systemuser")]
    public partial class Usuario : EntidadCrm, IAsignable
    {
        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public Usuario()
            :base()
        {
            Inicializar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public Usuario(Guid ID)
        {
            Inicializar();
            this.ID = ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidad"></param>
        public Usuario(Entity entidad)
            :base(entidad)
        {

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ObjectoSerializado"></param>
        ///// <param name="ObtenerValoresDesdeString"></param>
        //public Usuario(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
        //    : base(ObjectoSerializado, ObtenerValoresDesdeString)
        //{

        //}

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("systemuserid")]
        public Guid? UsuarioID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("domainname")]
        public String NombreDominioUsuario { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("firstname")]
        public String Nombres { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("lastname")]
        public String Apellidos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Requerido]
        [NombreEsquemaCrm("businessunitid")]
        [TipoDatoCrm(eTipoDatoCRM.EntityReference, "businessunit")]
        public CrmLookup UnidadDeNegocio { get; set; }

        #endregion

        #region MÉTODOS PÚBLICOS

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string NombreLogico()
        {
            return this.NombreEsquema;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new Guid? Id()
        {
            //TODO: Validar que retorne el ID de la entidad.
            return base.ID;;
        }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            UsuarioID = null;
            NombreDominioUsuario = null;
            Nombres = null;
            Apellidos = null;
        }

        #endregion
    }
}
