using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades.Interfases
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegistroError  
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        void RegistrarError(string Mensaje, Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        void RegistrarError(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "");
    }
}
