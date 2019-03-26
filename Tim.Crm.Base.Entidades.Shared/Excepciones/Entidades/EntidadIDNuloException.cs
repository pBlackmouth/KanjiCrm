using System;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.Excepciones
{
    /// <summary>
    /// 
    /// </summary>
    public class EntidadIDNuloException : TIMException
    {

        private static int CodigoError()
        {
            return 1006;

        }

        private static string Mensaje()
        {
            return "La propiedad ID de la entidad no ha sido especificado.";
        }


        public override string Message
        {
            get
            {
                return Mensaje();
            }
        }

        #region CONSTRUCTORES
        public EntidadIDNuloException(string message,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
            Detalle = message;
        }

        public EntidadIDNuloException(
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

        public EntidadIDNuloException(
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
