using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Tim.Crm.Base.Entidades.Excepciones;

namespace Tim.Crm.Base.Entidades.Excepciones
{
    public class NullWorkflowActivityContextException : TIMException
    {
       

            private static int CodigoError()
            {
                return 3004;
            }

            private static string Mensaje()
            {
                return "The Kanji Workflow Activity Context is null.";
            }


            public override string Message
            {
                get
                {
                    return Mensaje();
                }
            }

            #region CONSTRUCTORES
            public NullWorkflowActivityContextException(string message,

                [CallerLineNumber] int NumeroLinea = 0,

                [CallerMemberName] string Metodo = "",

                [CallerFilePath] string ArchivoOrigen = "")
                        : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
            {
                Descripcion = InnerException != null ? InnerException.Message : Mensaje();
                Detalle = message;
            }

            public NullWorkflowActivityContextException(
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

            public NullWorkflowActivityContextException(
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
