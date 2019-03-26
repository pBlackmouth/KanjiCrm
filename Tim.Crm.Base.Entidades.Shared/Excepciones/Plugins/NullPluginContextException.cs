using System;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Excepciones;

namespace im.Crm.Base.Entidades.Excepciones
{
    /// <summary>
    /// 
    /// </summary>
    public class NullPluginContextException : TIMException
    {

        private static int CodigoError()
        {
            return 3003;
        }

        private static string Mensaje()
        {
            return "The Kanji Plugin Context is null.";
        }


        public override string Message
        {
            get
            {
                return Mensaje();
            }
        }

        #region CONSTRUCTORES
        public NullPluginContextException(string message,

            [CallerLineNumber] int NumeroLinea = 0,

            [CallerMemberName] string Metodo = "",

            [CallerFilePath] string ArchivoOrigen = "")
                    : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
            Detalle = message;
        }

        public NullPluginContextException(
                    string message,
                    Exception inner,
                    [CallerLineNumber]
int NumeroLinea = 0,
                    [CallerMemberName] string Metodo = "",
                    [CallerFilePath] string ArchivoOrigen = "")
                    : base(Mensaje(), inner, CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
            Detalle = message;
        }

        public NullPluginContextException(
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
