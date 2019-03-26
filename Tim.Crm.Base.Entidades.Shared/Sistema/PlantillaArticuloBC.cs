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
    [NombreEsquemaCrm("kbarticletemplate")]
    public partial class PlantillaArticuloBC : EntidadCrm
    {

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        public PlantillaArticuloBC()
            : base()
        {
            Inicializar();
        }

        /// <summary>
        /// Constructor que permite hacer el mapeo de una clase Entity de CRM en nuestro objeto.
        /// </summary>
        /// <param name="entidad"></param>
        public PlantillaArticuloBC(Entity entidad)
            : base(entidad)
        {

        }

        //TODO: Definición de los constructores adicionales.

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// 
        /// </summary>
        [IdentificadorCRM]
        [NombreEsquemaCrm("kbarticletemplateid")]
        public Guid? PlantillaArticuloBCID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("title")]
        public String Titulo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("description")]
        public String Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("structurexml")]
        public String EstructuraXML { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("formatxml")]
        public String FormatoXML { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NombreEsquemaCrm("languagecode")]
        public int? Idioma { get; set; }

        
        
        #endregion

        #region MÉTODOS PRIVADOS

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve += new ResolveEventHandler(Handlers.ResolveAssemblyEventHandler);
            PlantillaArticuloBCID = null;
            Titulo = null;
            Descripcion = null;
            EstructuraXML = null;
            FormatoXML = null;
        }

        #endregion

        #region MÉTODOS PÚBLICOS

        //TODO: Definición de métodos públicos.

        #endregion

    }
}
