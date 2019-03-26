using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Logica
{
    public partial class UtileriasDiasLaborales
    {

        /// <summary>
        /// 
        /// </summary>
        IOrganizationService service = null;

        /// <summary>
        /// 
        /// </summary>
        private Acciones acciones = null;

        /// <summary>
        /// 
        /// </summary>
        List<TimeInfo> PeriodoLaboralUsuarioVacaciones = null;


        public UtileriasDiasLaborales(IOrganizationService service)
        {
            this.service = service;
            this.acciones = new Acciones(service);
        }


        /// <summary>
        /// Identifíca si una fecha es día no laboral para la empresa.
        /// </summary>
        /// <param name="fecha">Fecha a verificar</param>
        /// <returns>Objeto DiaNoLaboral. Se asigna a EsDiaLaboral (true o false).</returns>
        public DiaNoLaboral EsDiaNoLaboral(DateTime fecha)
        {
            DiaNoLaboral diaNoLaboral = new DiaNoLaboral();

            QueryExpression query = new QueryExpression("calendar");
            query.ColumnSet = new ColumnSet(true);
            ConditionExpression condition = new ConditionExpression();
            condition.AttributeName = "name";
            condition.Operator = ConditionOperator.Equal;
            condition.Values.Add("Business Closure Calendar");
            query.Criteria.Conditions.Add(condition);
            EntityCollection calendars = service.RetrieveMultiple(query);
            EntityCollection calendarrule = calendars[0].GetAttributeValue<EntityCollection>("calendarrules");
            Entity entidad = calendarrule.Entities.Where(e => ((DateTime)e["starttime"]).Date == fecha).FirstOrDefault();

            if (entidad != null)
            {
                diaNoLaboral.EsDiaNoLaboral = true;
                diaNoLaboral.EmpresaCerrada = true;
                diaNoLaboral.Nombre = entidad["name"].ToString();
            }

            return diaNoLaboral;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fecha"></param>
        /// <param name="ConsiderarIndisponibilidad"></param>
        /// <param name="NoDias"></param>
        /// <returns></returns>
        public HorarioLaboral ObtenerHorarioLaboralPorUsuario(Usuario user, DateTime fecha, bool ConsiderarIndisponibilidad = true, int NoDias = 7)
        {
            HorarioLaboral horario = null;
            List<ReglaCalendario> listaReglas = null;
            ReglaCalendario pausaLaboral = null;
            ReglaCalendario tiempoLaboral = null;

            List<PeriodoVacacional> vacacionesUsuario = null;

            Usuario usuario = acciones.ObtenerElemento<Usuario>(user);

            // Retrieve the working hours of the current user.                                              
            QueryScheduleRequest scheduleRequest = new QueryScheduleRequest
            {
                ResourceId = user.UsuarioID.Value,
                Start = fecha,
                End = fecha.AddDays(NoDias),
                TimeCodes = new TimeCode[] { TimeCode.Available, TimeCode.Unavailable }

            };

            QueryScheduleResponse scheduleResponse = (QueryScheduleResponse)service.Execute(scheduleRequest);

            if (scheduleResponse != null && scheduleResponse.TimeInfos != null && scheduleResponse.TimeInfos.Length > 0)
            {
                TimeInfo[] diasLaborales = scheduleResponse.TimeInfos.ToList().Where(ti => ti.SubCode == SubCode.Vacation || ti.SubCode == SubCode.Schedulable).ToArray();

                if (diasLaborales != null && diasLaborales.Length > 0)
                {
                    ComparadorDia cd = new ComparadorDia();
                    List<TimeInfo> listaDiasLaborales = diasLaborales.ToList();
                    listaDiasLaborales.Sort(cd);

                    horario = new HorarioLaboral();

                    horario.Usuario = usuario;
                    horario.NoDias = NoDias;
                    horario.FechaInicio = fecha.Date;
                    horario.FechaFin = horario.FechaInicio.Value.AddDays(NoDias);

                    List<DateTime> diasPeriodo = new List<DateTime>();

                    for (int i = 0; i < NoDias; i++)
                    {
                        if (i == 0)
                            diasPeriodo.Add(fecha.Date);
                        else
                            diasPeriodo.Add(fecha.Date.AddDays(i));
                    }


                    foreach (DateTime diaPeriodo in diasPeriodo)
                    {
                        TimeInfo diaLaboral = listaDiasLaborales.Find(d => d.Start.Value.Date.Equals(diaPeriodo));
                        DiaNoLaboral diaNoLaboral = new DiaNoLaboral();

                        Dia dia = new Dia();
                        dia.Fecha = diaPeriodo;


                        if (diaLaboral != null && !ConsiderarIndisponibilidad)
                        {
                            if (diaLaboral.SubCode == SubCode.Vacation)
                            {
                                diaLaboral = ObtenerDiaSinConsiderarIndisponibilidad(user, diaLaboral, diaPeriodo);
                            }
                        }

                        if (diaLaboral == null)
                            diaNoLaboral = EsDiaNoLaboral(diaPeriodo);

                        if (diaLaboral != null && !diaNoLaboral.EsDiaNoLaboral)
                        {

                            dia.EsDiaLaboral = true;

                            DateTime? inicio = null;
                            DateTime? fin = null;
                            TimeSpan? horaInicio = null;
                            TimeSpan? horaFin = null;

                            if (diaLaboral.Start != null)
                                inicio = diaLaboral.Start.Value.ToLocalTime();

                            if (diaLaboral.End != null)
                                fin = diaLaboral.End.Value.ToLocalTime();

                            if (inicio != null)
                                horaInicio = inicio.Value.TimeOfDay;

                            if (fin != null)
                                horaFin = fin.Value.TimeOfDay;

                            QueryExpression query = new QueryExpression("calendar");
                            query.ColumnSet = new ColumnSet(true);
                            ConditionExpression condition = new ConditionExpression();
                            condition.AttributeName = "calendarid";
                            condition.Operator = ConditionOperator.Equal;
                            condition.Values.Add(diaLaboral.CalendarId.ToString());
                            query.Criteria.Conditions.Add(condition);
                            EntityCollection calendars = service.RetrieveMultiple(query);

                            if (calendars != null)
                            {
                                listaReglas = new List<ReglaCalendario>();
                                foreach (Entity ent in calendars.Entities)
                                {
                                    EntityCollection reglasCalendario = (EntityCollection)ent["calendarrules"];

                                    if (reglasCalendario != null)
                                    {
                                        foreach (Entity regla in reglasCalendario.Entities)
                                        {

                                            if (regla["timecode"].ToString() == "0" && regla["subcode"].ToString() == ((int)SubCode.Schedulable).ToString())
                                            {
                                                tiempoLaboral = new ReglaCalendario(regla);
                                            }

                                            if (regla["timecode"].ToString() == "2" && (regla["subcode"].ToString() == ((int)SubCode.Break).ToString() || regla["subcode"].ToString() == ((int)SubCode.Vacation).ToString()))
                                            {

                                                if (regla["subcode"].ToString() == ((int)SubCode.Break).ToString())
                                                {
                                                    pausaLaboral = new ReglaCalendario(regla);
                                                    dia.HayReceso = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!ConsiderarIndisponibilidad)
                            {
                                if (horario != null && horario.Periodo.Count > 0)
                                {

                                }
                            }

                            dia.Fecha = diaLaboral.Start.Value.Date;
                            dia.HoraLaboralInicio = horaInicio;
                            dia.HoraLaboralFin = horaFin;
                            dia.NombreDia = UtileriasGenerales.ObteneNombreDiaSemana(dia.Fecha.Value.DayOfWeek);
                            dia.TiempoLaboralTotal = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;

                            //if(!ConsiderarIndisponibilidad)

                            if (pausaLaboral != null)
                            {
                                dia.DuracionReceso = pausaLaboral.Duracion;

                                if (pausaLaboral.Desfase != null)
                                {
                                    int hora = pausaLaboral.Desfase.Value / 60;
                                    int minutos = 60 * hora;
                                    int minutosRestantes = pausaLaboral.Desfase.Value - minutos;

                                    TimeSpan horaReceso = new TimeSpan(hora, minutosRestantes, 0);
                                    dia.HoraRecesoInicio = horaReceso;
                                    dia.HoraRecesoFin = dia.HoraRecesoInicio.Value.Add(new TimeSpan(0, dia.DuracionReceso.Value, 0));

                                    if (diaLaboral.Start.Value.Date == fecha.Date)
                                    {
                                        DateTime fechaActual = DateTime.Now;
                                        if (diaLaboral.Start.Value.Date == fechaActual.Date)
                                        {
                                            TimeSpan horaActual = fechaActual.TimeOfDay;

                                            if (horaActual <= dia.HoraLaboralInicio.Value)
                                            {
                                                minutosRestantes = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                            }

                                            if (horaActual > dia.HoraLaboralInicio && horaActual < dia.HoraRecesoInicio)
                                            {
                                                int minutosTotales = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                                TimeSpan minutosTranscurridos = horaActual.Subtract(dia.HoraLaboralInicio.Value);
                                                minutosRestantes = minutosTotales - (int)minutosTranscurridos.TotalMinutes;
                                            }

                                            if (horaActual >= dia.HoraRecesoInicio && horaActual < dia.HoraRecesoFin)
                                            {
                                                int minutosTotales = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                                TimeSpan minutosTranscurridos = dia.HoraRecesoInicio.Value.Subtract(dia.HoraLaboralInicio.Value);
                                                minutosRestantes = minutosTotales - (int)minutosTranscurridos.TotalMinutes;
                                            }

                                            if (horaActual >= dia.HoraRecesoFin && horaActual <= dia.HoraLaboralFin)
                                            {
                                                int minutosTotales = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                                TimeSpan minutosTranscurridos = dia.HoraRecesoInicio.Value.Subtract(dia.HoraLaboralInicio.Value);
                                                int minutosRestantesMañana = minutosTotales - (int)minutosTranscurridos.TotalMinutes;

                                                TimeSpan minutosTranscurridosTarde = horaActual.Subtract(dia.HoraRecesoFin.Value);
                                                minutosRestantes = minutosRestantesMañana - (int)minutosTranscurridosTarde.TotalMinutes;
                                            }

                                        }
                                        else
                                        {
                                            minutosRestantes = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                        }

                                        dia.TiempoLaboralRestante = minutosRestantes;
                                    }
                                    else
                                    {
                                        minutosRestantes = tiempoLaboral.Duracion.Value - pausaLaboral.Duracion.Value;
                                        dia.TiempoLaboralRestante = minutosRestantes;
                                    }
                                }
                            }


                        }
                        else
                        {

                            string NombreNoLaboral = "";
                            if (diaLaboral != null)
                            {
                                if (diaPeriodo.Date >= diaLaboral.Start.Value.Date && diaPeriodo.Date < diaLaboral.End.Value.Date)
                                {
                                    DiaNoLaboral diaNL = new DiaNoLaboral();

                                    QueryExpression query = new QueryExpression("calendar");
                                    query.ColumnSet = new ColumnSet(true);
                                    ConditionExpression condition = new ConditionExpression();
                                    condition.AttributeName = "calendarid";
                                    condition.Operator = ConditionOperator.Equal;
                                    condition.Values.Add(diaLaboral.CalendarId.ToString());
                                    query.Criteria.Conditions.Add(condition);
                                    EntityCollection calendars = service.RetrieveMultiple(query);


                                    if (vacacionesUsuario == null)
                                        vacacionesUsuario = new List<PeriodoVacacional>();

                                    DateTime diaPeriodoVacacional = diaLaboral.Start.Value.Date;
                                    PeriodoVacacional pv = new PeriodoVacacional();

                                    if (calendars != null && calendars.Entities != null && calendars.Entities.Count > 0)
                                    {
                                        pv.Nombre = calendars[0].GetAttributeValue<string>("name").ToString();
                                        NombreNoLaboral = pv.Nombre;
                                    }

                                    while (diaPeriodoVacacional.Date < diaLaboral.End.Value.Date)
                                    {
                                        pv.Periodo.Add(diaPeriodoVacacional.Date);
                                        diaPeriodoVacacional = diaPeriodoVacacional.AddDays(1);
                                    }

                                    vacacionesUsuario.Add(pv);

                                }
                            }
                            else
                            {
                                if (vacacionesUsuario != null)
                                {
                                    foreach (PeriodoVacacional periodovacacional in vacacionesUsuario)
                                    {
                                        if (periodovacacional.Periodo.Contains(diaPeriodo))
                                        {
                                            NombreNoLaboral = periodovacacional.Nombre;
                                        }
                                    }
                                }

                            }

                            dia.EsDiaLaboral = false;
                            dia.NombreDia = UtileriasGenerales.ObteneNombreDiaSemana(dia.Fecha.Value.DayOfWeek);
                            dia.MotivoNoLaboral = NombreNoLaboral != "" ? NombreNoLaboral : diaNoLaboral.Nombre == null ? "" : diaNoLaboral.Nombre;
                            dia.EmpresaCerrada = diaNoLaboral.EmpresaCerrada;
                        }


                        if (dia.EsDiaLaboral)
                        {
                            dia.MinutosAntesReceso = ((int)dia.HoraRecesoInicio.Value.Subtract(dia.HoraLaboralInicio.Value).TotalMinutes);
                            dia.MinutosDespuesReceso = ((int)dia.HoraLaboralFin.Value.Subtract(dia.HoraRecesoFin.Value).TotalMinutes);
                        }

                        horario.Periodo.Add(dia);

                    }//foreach (TimeInfo diaLaboral in diasLaborales)
                }
            }

            return horario;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="diaLaboral"></param>
        /// <param name="diaPeriodo"></param>
        /// <returns></returns>
        private TimeInfo ObtenerDiaSinConsiderarIndisponibilidad(Usuario user, TimeInfo diaLaboral, DateTime diaPeriodo)
        {
            TimeInfo dia = null;

            DateTime inicio = diaLaboral.Start.Value.Date;
            DateTime fin = diaLaboral.End.Value.Date;

            if (PeriodoLaboralUsuarioVacaciones == null)
                PeriodoLaboralUsuarioVacaciones = ObtenerDiasLaborales(user, inicio, fin, 1);


            if (PeriodoLaboralUsuarioVacaciones != null)
            {

                foreach (TimeInfo ti in PeriodoLaboralUsuarioVacaciones)
                {

                }
            }


            return dia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fechaIncio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="SemanasSiguientes"></param>
        /// <returns></returns>
        private List<TimeInfo> ObtenerDiasLaborales(Usuario user, DateTime fechaIncio, DateTime fechaFin, int SemanasSiguientes)
        {
            List<TimeInfo> entities = null;
            // Retrieve the working hours of the current user.                                              
            QueryScheduleRequest scheduleRequest = new QueryScheduleRequest
            {
                ResourceId = user.UsuarioID.Value,
                Start = fechaIncio.AddDays(7 * SemanasSiguientes),
                End = fechaFin.AddDays(7 * SemanasSiguientes),
                TimeCodes = new TimeCode[] { TimeCode.Available, TimeCode.Unavailable }
            };

            QueryScheduleResponse scheduleResponse = (QueryScheduleResponse)service.Execute(scheduleRequest);

            if (scheduleResponse != null && scheduleResponse.TimeInfos.Length > 0)
                entities = scheduleResponse.TimeInfos.ToList();

            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diaLaboral"></param>
        /// <param name="considerarIndisponibilidad"></param>
        /// <returns></returns>
        private bool ConsiderarIndisponibilidadDeDias(TimeInfo diaLaboral, bool considerarIndisponibilidad)
        {
            bool value = false;

            if (considerarIndisponibilidad)
            {
                value = diaLaboral.SubCode == SubCode.Schedulable;
            }
            else
            {
                value = diaLaboral.SubCode == SubCode.Schedulable || diaLaboral.SubCode == SubCode.Vacation;
            }

            return value;
        }



    }
}
