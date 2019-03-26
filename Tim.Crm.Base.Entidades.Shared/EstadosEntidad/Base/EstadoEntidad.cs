using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Interfases;

namespace Tim.Crm.Base.Entidades
{
    public class EstadoEntidad : IEstadoEntidad

    {
        int? estado = null;

        public EstadoEntidad(int value)
        {
            estado = value;
        }

        public int? ObtenerEstadoEntidad()
        {
            return estado;
        }
        
    }
}
