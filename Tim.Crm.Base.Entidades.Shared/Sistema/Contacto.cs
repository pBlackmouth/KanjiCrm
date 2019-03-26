using System;
using Tim.Crm.Base.Entidades;
using Tim.Crm.Base.Entidades.Atributos;
using Microsoft.Xrm.Sdk;


namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// TODO: Descripción de la clase y su uso.
    /// </summary>
    [Serializable]
    [NombreEsquemaCrm("contact")]
    public partial class Contacto : EntidadCrm
    {
      

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public Contacto()
            : base()
        {
            Inicializar();
        }

        //public Contacto(Guid Id)
        //    :base(Id)
        //{

        //}

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public Contacto(Entity entidad)
            : base(entidad)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public Contacto(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
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
        [NombreEsquemaCrm("contactid")]
        [NombreParaMostrar("Contacto Id")]
        public Guid? ContactoID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("firstname")]
        [NombreParaMostrar("Nombres")]
        public String Nombres { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("lastname")]
        [NombreParaMostrar("Apellidos")]
        public String Apellidos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("fullname")]
        [NombreParaMostrar("Nombre Completo")]
        public String NombreCompleto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("birthdate")]
        [NombreParaMostrar("Fecha de Nacimiento")]
        public Fecha Cumpleanos { get; set; }

        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            ContactoID = null;
            Nombres = null;
            Apellidos = null;
            NombreCompleto = null;
            Cumpleanos = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
