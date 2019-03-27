using System;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Orden
    {
        /// <summary>
        /// 
        /// </summary>
        public Orden()
        {
            Propiedad = null;
            EsAscendente = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Propiedad { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Boolean EsAscendente { get; set; }

    }
}
