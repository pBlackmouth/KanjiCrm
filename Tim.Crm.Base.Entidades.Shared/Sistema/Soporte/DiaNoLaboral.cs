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
    public class DiaNoLaboral
    {
        /// <summary>
        /// 
        /// </summary>
        public DiaNoLaboral()
        {
            EsDiaNoLaboral = false;
            EmpresaCerrada = false;
            Nombre = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public Boolean EsDiaNoLaboral { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean EmpresaCerrada { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }
    }
}
