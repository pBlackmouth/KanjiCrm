using System;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.Excepciones
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificacionCorreoException : TIMException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int CodigoError()
        {
            return 1602;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string Mensaje()
        {
            return "Sucedio un error al tratar de notificar un error de Evento de Sistema";
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return Mensaje();
            }
        }

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public NotificacionCorreoException(string message,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
            Detalle = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public NotificacionCorreoException(
            string message,
            Exception inner,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), inner, CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
            Detalle = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public NotificacionCorreoException(
            Exception inner,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), inner, CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = inner != null ? inner.Message : Mensaje();
        }

        #endregion
    }
}
