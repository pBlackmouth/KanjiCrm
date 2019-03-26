using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.TipoDatos
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmLinkEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public String EntidadOrigen { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String EntidadDestino { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? AtributoOrigen { get; set; }        
        
        /// <summary>
        /// 
        /// </summary>
        public Guid? AtributoDestino { get; set; }

    }
}
