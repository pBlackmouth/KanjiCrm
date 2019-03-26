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
    public interface IAsignable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string NombreLogico();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Guid? Id();

    }
}
