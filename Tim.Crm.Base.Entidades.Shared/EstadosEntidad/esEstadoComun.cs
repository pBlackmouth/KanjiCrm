using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades
{
    public class esEstadoComun : CambioEstadoEntidad
    {

        public esEstadoComun() : base((int)eEstadoComun.Activo,(int)eEstadoComun.Inactivo)
        {

        }
    }
}
