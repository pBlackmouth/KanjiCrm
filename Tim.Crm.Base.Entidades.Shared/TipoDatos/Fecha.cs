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
    public class Fecha : OperadoresComparacion
    {
        /// <summary>
        /// 
        /// </summary>
        private static Fecha _fecha = null;

        /// <summary>
        /// 
        /// </summary>
        public Fecha()
            :base()
        {
            TipoDato = DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        public Fecha(DateTime fecha)
            : base()
        {
            Valor = fecha;
        }

        /// <summary>
        /// 
        /// </summary>
        new public DateTime Valor
        {
            get
            {
                return (DateTime)base.Valor;
            }
            set
            {
                base.Valor = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static Fecha MayorQue(DateTime fecha)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.MayorQue<Fecha, DateTime>(fecha);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static Fecha MayorIgualQue(DateTime fecha)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.MayorIgualQue<Fecha, DateTime>(fecha);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static Fecha MenorQue(DateTime fecha)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.MenorQue<Fecha, DateTime>(fecha);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static Fecha MenorIgualQue(DateTime fecha)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.MenorIgualQue<Fecha, DateTime>(fecha);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public static Fecha Entre(DateTime FechaInicio, DateTime FechaFin, eRango rango)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.Entre<Fecha, DateTime>(FechaInicio, FechaFin, rango);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="rango"></param>
        /// <returns></returns>
        public static Fecha NoEsteEntre(DateTime FechaInicio, DateTime FechaFin, eRango rango)
        {
            using (_fecha = new Fecha())
            {
                return _fecha.NoEsteEntre<Fecha, DateTime>(FechaInicio, FechaFin, rango);
            }
        }


    }
}
