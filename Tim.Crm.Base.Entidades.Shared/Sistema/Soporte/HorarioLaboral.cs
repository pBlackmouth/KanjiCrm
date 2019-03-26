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
    public class HorarioLaboral
    {

        /// <summary>
        /// 
        /// </summary>
        public HorarioLaboral()
        {
            NoDias = null;
            FechaInicio = null;
            FechaFin = null;
            Periodo = new List<Dia>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int? NoDias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? FechaInicio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? FechaFin { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        public List<Dia> Periodo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Calcula una fecha tomando en cuenta el horario laboral de un usuario o por tiempo natural.
        /// </summary>
        /// <param name="Fecha">La fecha a partir de donde se hará el cálculo.</param>
        /// <param name="Minutos">El tiempo que se añadirá a la Fecha para hacer el cálculo.</param>
        /// <param name="TipoCalculo">Días u horas hábiles o naturales.</param>
        /// <returns>Fecha calculada.</returns>
        public DateTime? CalcularFechaProgramada(DateTime FechaOrigen, int Minutos, eDias TipoCalculo)
        {
            DateTime? fecha = null;
            TimeSpan horaFecha = FechaOrigen.TimeOfDay;

            //Verifica que el periodo contiene días.
            if (Periodo.Count > 0)
            {
                //Si el tiempo es natural, solo se agrega a la fecha.
                if(TipoCalculo == eDias.Naturales)
                {
                    fecha = FechaOrigen.AddMinutes(Minutos);
                }
                else
                {
                    DateTime? fechaTemp = null;

                    //Recorre cada día del periodo.
                    foreach(Dia dia in Periodo)
                    {
                        //Se valida que sea un día laboral.
                        if (dia.EsDiaLaboral)
                        {
                            fechaTemp = dia.Fecha.Value;

                            //El tiempo de programación se cumple dentro del primer día del periodo.
                            if (Minutos <= dia.TiempoLaboralRestante.Value)
                            {
                                if (fechaTemp.Value.Date > FechaOrigen.Date)
                                {
                                    fechaTemp = CalculaHoraDelDia(fechaTemp.Value, Minutos, dia);
                                }
                                else
                                {
                                    if(horaFecha < dia.HoraLaboralInicio.Value)
                                    {
                                        fechaTemp = CalculaHoraDelDia(fechaTemp.Value, Minutos, dia);
                                    }

                                    if(horaFecha >= dia.HoraLaboralInicio.Value && horaFecha <= dia.HoraLaboralFin.Value)
                                    {
                                        if (dia.HayReceso)
                                        {
                                            fechaTemp = fechaTemp.Value.AddMinutes(horaFecha.TotalMinutes);
                                            fechaTemp = fechaTemp.Value.AddMinutes((double)Minutos);

                                            if(fechaTemp.Value.TimeOfDay >= dia.HoraRecesoInicio.Value)
                                            {
                                                TimeSpan desfaseMinutos = fechaTemp.Value.TimeOfDay.Subtract(dia.HoraRecesoInicio.Value);
                                                fechaTemp = fechaTemp.Value.Date;
                                                fechaTemp = fechaTemp.Value.AddMinutes(dia.HoraRecesoFin.Value.TotalMinutes);
                                                fechaTemp = fechaTemp.Value.AddMinutes(desfaseMinutos.TotalMinutes);
                                            }                                            
                                        }
                                        else
                                        {
                                            fechaTemp = fechaTemp.Value.AddMinutes(horaFecha.TotalMinutes);
                                            fechaTemp = fechaTemp.Value.AddMinutes((double)Minutos);
                                        }
                                    }
                                }

                                fecha = fechaTemp;
                                break;

                            }
                            else
                            {
                                Minutos = Minutos - dia.TiempoLaboralRestante.Value;
                            }
                        }
                    }
                }
            }

            return fecha;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="Minutos"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        private DateTime? CalculaHoraDelDia(DateTime fecha, int Minutos, Dia dia )
        {
            DateTime? fechaTemp = fecha;

            if (dia.HayReceso)
            {
                if (Minutos <= dia.MinutosAntesReceso.Value)
                {
                    fechaTemp = fechaTemp.Value.AddMinutes(dia.HoraLaboralInicio.Value.TotalMinutes);
                    fechaTemp = fechaTemp.Value.AddMinutes((double)Minutos);
                }
                else
                {
                    Minutos = Minutos - dia.MinutosAntesReceso.Value;
                    fechaTemp = fechaTemp.Value.AddMinutes(dia.HoraRecesoFin.Value.TotalMinutes);
                    fechaTemp = fechaTemp.Value.AddMinutes((double)Minutos);
                }
            }
            else
            {
                fechaTemp = fechaTemp.Value.AddMinutes(dia.HoraLaboralInicio.Value.TotalMinutes);
                fechaTemp = fechaTemp.Value.AddMinutes((double)Minutos);
            }

            return fechaTemp;

        }



    }
}
