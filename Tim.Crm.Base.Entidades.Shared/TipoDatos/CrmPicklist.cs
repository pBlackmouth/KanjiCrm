using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmPicklist
    {

        /// <summary>
        /// 
        /// </summary>
        public CrmPicklist()
        {
            ID = null;
            Nombre = null;
            TipoObjeto = "OptionSetValue";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public CrmPicklist(int? ID)
        {
            this.ID = ID;
            Nombre = null;
            TipoObjeto = "OptionSetValue";
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }

        public String TipoObjeto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static CrmPicklist Empty
        {
            get { return new CrmPicklist() { ID = -1, Nombre = String.Empty }; }
        }
    }
}