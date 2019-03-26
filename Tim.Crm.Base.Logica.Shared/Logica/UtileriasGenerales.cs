using System;

namespace Tim.Crm.Base.Logica
{
    public partial class UtileriasGenerales
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        public static string ObteneNombreDiaSemana(DayOfWeek dia)
        {
            string nombre = "";

            switch (dia.ToString().ToLower())
            {
                case "monday":
                    nombre = "Lunes";
                    break;
                case "tuesday":
                    nombre = "Martes";
                    break;
                case "wednesday":
                    nombre = "Miércoles";
                    break;
                case "thursday":
                    nombre = "Jueves";
                    break;
                case "friday":
                    nombre = "Viernes";
                    break;
                case "saturday":
                    nombre = "Sábado";
                    break;
                case "sunday":
                    nombre = "Domingo";
                    break;
            }

            return nombre;

        }
    }
}
