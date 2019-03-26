using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Entero : OperadoresComparacion
    {
        /// <summary>
        /// 
        /// </summary>
        private static Entero _entero = null;

        /// <summary>
        /// 
        /// </summary>
        public Entero()
            :base()
        {
            TipoDato = Int32.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entero"></param>
        public Entero(Int32 entero)
            : base()
        {
            Valor = entero;
        }

        /// <summary>
        /// 
        /// </summary>
        new public Int32 Valor
        {
            get
            {
                return (Int32)base.Valor;
            }
            set
            {
                base.Valor = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entero"></param>
        /// <returns></returns>
        public static Entero MayorQue(Int32 entero)
        {
            using (_entero = new Entero())
            {
                return _entero.MayorQue<Entero, Int32>(entero);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entero"></param>
        /// <returns></returns>
        public static Entero MayorIgualQue(Int32 entero)
        {
            using (_entero = new Entero())
            {
                return _entero.MayorIgualQue<Entero, Int32>(entero);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entero"></param>
        /// <returns></returns>
        public static Entero MenorQue(Int32 entero)
        {
            using (_entero = new Entero())
            {
                return _entero.MenorQue<Entero, Int32>(entero);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entero"></param>
        /// <returns></returns>
        public static Entero MenorIgualQue(Int32 entero)
        {
            using (_entero = new Entero())
            {
                return _entero.MenorIgualQue<Entero, Int32>(entero);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enteroInicio"></param>
        /// <param name="enteroFin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public static Entero Entre(Int32 enteroInicio, Int32 enteroFin, eRango rango)
        {
            using (_entero = new Entero())
            {
                return _entero.Entre<Entero, Int32>(enteroInicio, enteroFin, rango);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enteroInicio"></param>
        /// <param name="enteroFin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public static Entero NoEsteEntre(Int32 enteroInicio, Int32 enteroFin, eRango rango)
        {
            using (_entero = new Entero())
            {
                return _entero.NoEsteEntre<Entero, Int32>(enteroInicio, enteroFin, rango);
            }
        }


    }
}
