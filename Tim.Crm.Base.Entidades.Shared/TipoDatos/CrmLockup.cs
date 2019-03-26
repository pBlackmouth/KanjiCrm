using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmLookup
    {
        /// <summary>
        /// 
        /// </summary>
        public CrmLookup()
        {
            ID = null;
            Nombre = null;
            TipoEntidad = null;
            TipoObjeto = "EntityReference";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public CrmLookup(Guid ID)
        {
            this.ID = ID;
            Nombre = null;
            TipoEntidad = null;
            TipoObjeto = "EntityReference";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="NombreEsquema"></param>
        public CrmLookup(Guid ID, String NombreEsquema)
        {
            this.ID = ID;
            Nombre = null;
            TipoEntidad = NombreEsquema;
            TipoObjeto = "EntityReference";
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TipoEntidad { get; set; }


        public String TipoObjeto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static CrmLookup Empty
        {
            get { return new CrmLookup() { ID = Guid.Empty, Nombre = String.Empty, TipoEntidad = String.Empty }; }
        }
    }
}
