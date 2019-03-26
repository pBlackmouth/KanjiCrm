using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Dia
    {
        /// <summary>
        /// 
        /// </summary>
        public Dia()
        {
            Inicializar();
        }



        /// <summary>
        /// Indica si es un día laboral.
        /// </summary>
        public Boolean EsDiaLaboral { get; set; }

        /// <summary>
        /// Indica si es un día laboral.
        /// </summary>
        public Boolean EmpresaCerrada { get; set; }

        /// <summary>
        /// Indica si en el día hay receso en la jornada laboral.
        /// </summary>
        public Boolean HayReceso { get; set; }


        /// <summary>
        /// Indica la fecha en cuestión.
        /// </summary>
        public DateTime? Fecha { get; set; }


        /// <summary>
        /// Indica la hora de inicio de la jornada laboral.
        /// </summary>
        public TimeSpan? HoraLaboralInicio { get; set; }


        /// <summary>
        /// Indica la hora de fin de la jornada laboral.
        /// </summary>
        public TimeSpan? HoraLaboralFin { get; set; }


        /// <summary>
        /// Indica la hora de inicio del receso.
        /// </summary>
        public TimeSpan? HoraRecesoInicio { get; set; }

        /// <summary>
        /// indica la hora de fin del receso.
        /// </summary>
        public TimeSpan? HoraRecesoFin { get; set; }

        /// <summary>
        /// Indica la duración del receso en minutos.
        /// </summary>
        public int? DuracionReceso { get; set; }

        /// <summary>
        /// Indica los minutos totales antes del receso del día.
        /// </summary>
        public int? MinutosAntesReceso { get; set; }


        /// <summary>
        /// Indica los minutos totales después del receso del día.
        /// </summary>
        public int? MinutosDespuesReceso { get; set; }

        /// <summary>
        /// Indica el tiempo laboral total del día, en minutos.
        /// </summary>
        public int? TiempoLaboralTotal { get; set; }

        /// <summary>
        /// Indica el tiempo restante laboral en minutos.
        /// </summary>
        public int? TiempoLaboralRestante { get; set; }

        //Indica el motivo del por que no es día laboral.
        public string MotivoNoLaboral { get; set; }


        //Muestra el nombre del día en cuestión.
        public String NombreDia { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void Inicializar()
        {
            EsDiaLaboral = false;
            EmpresaCerrada = false;
            HayReceso = false;
            Fecha = null;
            HoraLaboralInicio = null;
            HoraLaboralFin = null;
            HoraRecesoInicio = null;
            HoraRecesoFin = null;
            DuracionReceso = null;
            TiempoLaboralRestante = null;
            MinutosAntesReceso = null;
            MinutosDespuesReceso = null;
        }




    }
}
