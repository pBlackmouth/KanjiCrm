using System;
using System.Runtime.CompilerServices;

namespace Tim.Crm.Base.Entidades.Excepciones
{
    /// <summary>
    /// 
    /// </summary>
    public class ConexionCRMException : TIMException
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int CodigoError()
        {
            return 1201;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string Mensaje()
        {
            return "Sucedió un error al tratar de conectarse a CRM.";
        }

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public ConexionCRMException(string message,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public ConexionCRMException(
            string message,
            Exception inner,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), inner, CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public ConexionCRMException(
            Exception inner,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), inner, CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = Mensaje();
        }

        #endregion
    }
}
