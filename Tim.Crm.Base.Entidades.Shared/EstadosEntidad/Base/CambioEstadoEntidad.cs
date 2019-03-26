using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim.Crm.Base.Entidades
{
    public class CambioEstadoEntidad
    {

        EstadoEntidad activo = null;
        EstadoEntidad inactivo = null;
        EstadoEntidad enviado = null;

        public CambioEstadoEntidad(int Activo, int Inactivo)
        {
            activo = new EstadoEntidad(Activo);
            inactivo = new EstadoEntidad(Inactivo);
        }

        //public CambioEstadoEntidad(int Activo, int Inactivo, int Enviado)
        //{
        //    activo = new EstadoEntidad(Activo);
        //    inactivo = new EstadoEntidad(Inactivo);
        //    enviado = new EstadoEntidad(Enviado);
        //}

        public EstadoEntidad Activo { get { return activo; } }
        public EstadoEntidad Inactivo { get { return inactivo; } }

  

    }
}
