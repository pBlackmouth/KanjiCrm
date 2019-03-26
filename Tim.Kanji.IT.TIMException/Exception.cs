using System;
using System.Runtime.CompilerServices;
using Tim.Crm.Base.Entidades.Excepciones;

namespace $rootnamespace$
{
    /// <summary>
    /// 
    /// </summary>
    public class $safeitemname$ : TIMException
    {
    
        private static int CodigoError()
{
    //TODO: Especificar código de error.
    //return 1000;
    throw new NotImplementedException();

}

private static string Mensaje()
{
    //TODO: Especificar mensaje personalizado.
    //return "<<<Especificar mensaje de error para esta excepción>>>>";
    throw new NotImplementedException();

}


public override string Message
{
    get
    {
        return Mensaje();
    }
}

#region CONSTRUCTORES
public $safeitemname$(string message,

    [CallerLineNumber] int NumeroLinea = 0,

    [CallerMemberName] string Metodo = "",

    [CallerFilePath] string ArchivoOrigen = "")
            : base(Mensaje(), CodigoError(), NumeroLinea, Metodo, ArchivoOrigen)
        {
            Descripcion = InnerException != null ? InnerException.Message : Mensaje();
Detalle = message;
        }

public $safeitemname$(
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

        public $safeitemname$(
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
