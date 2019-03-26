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
    public class PeriodoVacacional
    {

        /// <summary>
        /// 
        /// </summary>
        public PeriodoVacacional()
        {
            Nombre = null;
            Periodo = new List<DateTime>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DateTime> Periodo { get; set; }
    }
}
