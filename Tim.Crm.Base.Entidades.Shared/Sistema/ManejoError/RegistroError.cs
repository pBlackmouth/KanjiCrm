using System;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.Sistema
{

    /// <summary>
    /// Esta clase sirve solo como un único punto de referencia para el registro de errores,
    /// y si se requiere un cambio al registro de errores sea por medio de esta clase.
    /// </summary>
    public class RegistroError
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
        public void Error(String Mensaje, Exception Ex, 
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            TIMException ex = new TIMException(Mensaje, Ex, EventoID, NumeroLinea, Metodo, ArchivoOrigen);
            throw ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ex"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public void Error(Exception Ex,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
        {
            //TODO: Verificar que esté bien definido este método.
            TIMException ex = new TIMException(Ex,EventoID,NumeroLinea,Metodo,ArchivoOrigen);
            throw ex;
        }

    }
}
