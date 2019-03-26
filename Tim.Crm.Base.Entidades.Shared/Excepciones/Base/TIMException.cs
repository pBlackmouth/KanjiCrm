using System;
using System.Runtime.CompilerServices;
using System.Web.Services.Protocols;
using Tim.Crm.Base.Entidades.Sistema;

namespace Tim.Crm.Base.Entidades.Excepciones
{
    /// <summary>
    /// 
    /// </summary>
    public class TIMException : Exception
    {
        //private string _message = "Error no definido.";
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int CodigoError()
        {
            return 1000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string Titulo()
        {
            return "Error no definido.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public TIMException(
            string message,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = ""
            )
            : base(message)
        {
            AsignarCodigoError(EventoID);            
            AsignarDetalleError(Metodo, ArchivoOrigen, NumeroLinea);
            Descripcion = InnerException != null ? InnerException.Message : message;
            Detalle = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public TIMException(
            string message,
            Exception inner,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(message, inner)
        {
            AsignarCodigoError(EventoID);            
            AsignarDetalleError(Metodo, ArchivoOrigen, NumeroLinea);
            Descripcion = inner.Message;
            Detalle = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="EventoID"></param>
        /// <param name="NumeroLinea"></param>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        public TIMException(
            Exception inner,
            int EventoID = 1000,
            [CallerLineNumber] int NumeroLinea = 0,
            [CallerMemberName] string Metodo = "",
            [CallerFilePath] string ArchivoOrigen = "")
            : base(Titulo(), inner)
        {            
            AsignarCodigoError(EventoID);            
            AsignarDetalleError(Metodo, ArchivoOrigen, NumeroLinea);
            Descripcion = inner.Message;
            Detalle = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return Titulo();
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public void Inicializar()
        {
            
            PilaDeDescripciones = String.Empty;
            PilaDeMetodos = String.Empty;
            EsExcepcionMensajeError = false;
            
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventoID"></param>
        private void AsignarCodigoError(int eventoID)
        {
            EventoID = eventoID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Metodo"></param>
        /// <param name="ArchivoOrigen"></param>
        /// <param name="NumeroLinea"></param>
        private void AsignarDetalleError(String Metodo, String ArchivoOrigen, int NumeroLinea)
        {            
            this.Metodo = Metodo;
            this.ArchivoOrigen = ArchivoOrigen.Substring(ArchivoOrigen.LastIndexOf('\\') + 1);
            this.NumeroLinea = NumeroLinea;

            Inicializar();
            RegistrarErrorEnPilaDeDescripciones(this);
            RegistrarErrorEnPilaDeMetodos(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void RegistrarErrorEnPilaDeDescripciones(Exception ex)
        {
            string mensajeError = "";

            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    RegistrarErrorEnPilaDeDescripciones(ex.InnerException);
                    Profundidad++;
                }


                if (ex is SoapException)
                    mensajeError = ((SoapException)ex).Detail.InnerText;
                else
                    if(ex is TIMException)
                    {
                    TIMException tex = (TIMException)ex;
                    mensajeError = tex.Detalle != null ? tex.Detalle : (tex.ErrorOrigen != null ? GetErrorMessage(tex.ErrorOrigen) : tex.Message);

                    }
                    else
                        mensajeError = ex.Message;

            }


            if (PilaDeDescripciones == "")
            {
                ErrorOrigen = ex;
                PilaDeDescripciones += string.Format("{0}.- Error origen: {1}, Detalle: {2}", Profundidad, mensajeError,Detalle);
            }
            else
            {
                PilaDeDescripciones += string.Format("\r\n {0}.---Consecuencia: {1}, Detalle: {2}", Profundidad, mensajeError,Detalle);
            }
        }

        private string GetErrorMessage(Exception ex)
        {
            string message = "Sin detalle";

            if (ex != null)
            {
                if (ex is TIMException)
                {
                    TIMException tex = ((TIMException)ex);
                    if (tex.Detalle == null)
                    {
                        message = GetErrorMessage(tex.InnerException);
                    }
                    else
                    {
                        message = tex.Detalle;
                    }
                }
                else
                {
                    message = ex.Message;
                }
            }

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void RegistrarErrorEnPilaDeMetodos(Exception ex)
        {
            string mensajeError = "";

            if (ex != null)
            {
                if (ex is TIMException)
                {
                    if (ex.InnerException != null)
                    {
                        RegistrarErrorEnPilaDeMetodos(ex.InnerException);
                    }

                    mensajeError = string.Format("{0}. \r\n -En archivo: {1}. \r\n -En Linea: {2}.", ((TIMException)ex).Metodo, ((TIMException)ex).ArchivoOrigen, ((TIMException)ex).NumeroLinea);
                }
                else
                {
                    mensajeError = string.Format("{0}. \r\n -En archivo: {1}. \r\n -En Linea: {2}.", this.Metodo, this.ArchivoOrigen, this.NumeroLinea);
                }
            }

            if (PilaDeMetodos == "")
            {
                PilaDeMetodos += "Método origen: " + mensajeError;
            }
            else
            {
                PilaDeMetodos += "\r\n---Continua en: " + mensajeError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EventoID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String PilaDeDescripciones { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String PilaDeMetodos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String Metodo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public String ArchivoOrigen { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int NumeroLinea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Profundidad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Detalle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EsExcepcionMensajeError { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Exception ErrorOrigen { get; set; }






    }
}
