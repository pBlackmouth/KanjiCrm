using System;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmUser
    {
        /// <summary>
        /// 
        /// </summary>
        public CrmUser()
        {
            Contrasena = null;
            Nombre = null;
            Dominio = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Contrasena { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Dominio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static CrmUser Empty
        {
            get { return new CrmUser() { Nombre = String.Empty, Contrasena = String.Empty, Dominio = String.Empty }; }
        }
    }
}
