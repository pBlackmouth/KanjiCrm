using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Sistema;

namespace Tim.Crm.Base.Entidades.Interfases
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObservador
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        void ReportarError(Exception ex);

    }
}
