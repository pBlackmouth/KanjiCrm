using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class OperadoresComparacion : IDisposable
    {
       
        /// <summary>
        /// 
        /// </summary>
        public OperadoresComparacion()
        {
           Valor = null;
           Rango = null;
           TipoComparacion = eComparacion.Ninguno;
           Operador = eOperador.Ninguno;
           OperadorRango = eRango.Ninguno;
           TipoRango = eTipoRango.Ninguno;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        public OperadoresComparacion(Object valor)
        {
            Valor = valor;
            Rango = null;
            TipoComparacion = eComparacion.Ninguno;
            Operador = eOperador.Ninguno;
            OperadorRango = eRango.Ninguno;
            TipoRango = eTipoRango.Ninguno;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Object Valor { get; set; }

        
        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public virtual Object TipoDato { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public List<Object> Rango { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public eComparacion TipoComparacion { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public eOperador Operador { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public eRango OperadorRango { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        /// <summary>
        /// 
        /// </summary>
        public eTipoRango TipoRango { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <returns></returns>
        public S MayorQue<S, T>(T valor) where S : OperadoresComparacion
        {
            
            S _item = AsignarValor<S, T>(valor);
            _item.Operador = eOperador.MayorQue;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <returns></returns>
        public S MayorIgualQue<S, T>(T valor) where S : OperadoresComparacion
        {
            S _item = AsignarValor<S, T>(valor);
            _item.Operador = eOperador.MayorIgualQue;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <returns></returns>
        public S MenorQue<S, T>(T valor) where S : OperadoresComparacion
        {
            S _item = AsignarValor<S, T>(valor);
            _item.Operador = eOperador.MenorQue;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <returns></returns>
        public S MenorIgualQue<S, T>(T valor) where S : OperadoresComparacion
        {
            S _item = AsignarValor<S, T>(valor);
            _item.Operador = eOperador.MenorIgualQue;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="Inicio"></param>
        /// <param name="Fin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public S Entre<S, T>(T Inicio, T Fin, eRango rango) where S : OperadoresComparacion
        {
            S _item = AsignarRango<S, T>(Inicio, Fin, rango);
            _item.TipoRango = eTipoRango.EntreRango;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="Inicio"></param>
        /// <param name="Fin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public S NoEsteEntre<S, T>(T Inicio, T Fin, eRango rango) where S : OperadoresComparacion
        {
            S _item = AsignarRango<S, T>(Inicio, Fin, rango);
            _item.TipoRango = eTipoRango.FueraRango;
            return (S) _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <returns></returns>
        private S AsignarValor<S, T>(T valor) where S : OperadoresComparacion
        {
            S _item = (S)Activator.CreateInstance(typeof(S));  
          
            //Originalmente llevaba Comparacion
            _item.Valor = valor;
            _item.TipoComparacion = eComparacion.Simple;
            return _item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="Inicio"></param>
        /// <param name="Fin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        private S AsignarRango<S, T>(T Inicio, T Fin, eRango rango) where S : OperadoresComparacion
        {
            S _item = (S)Activator.CreateInstance(typeof(S));
            _item.TipoComparacion = eComparacion.Rango;
            _item.OperadorRango = rango;
            _item.Rango = new List<Object>();
            _item.Rango.Add(Inicio);
            _item.Rango.Add(Fin);
            return _item;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String cadena = String.Empty;

            if (Valor.GetType().FullName.Contains("DateTime"))
                cadena = ((DateTime)Valor).ToString("s");
            else
                cadena = Valor.ToString();


            return Valor != null ? cadena : "";
        }



        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        //Implement IDisposable.
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                

                // Set large fields to null.
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        /// <summary>
        /// 
        /// </summary>
        ~OperadoresComparacion()
        {
            // Simply call Dispose(false).
            Dispose (false);
        }
    }
}
