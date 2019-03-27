using System;
using System.Collections.Generic;
using System.Text;

namespace Tim.Crm.Base.Entidades.Extension.Shared
{
    public partial class Contacto: EntidadCrmExtended
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectoSerializado"></param>
        /// <param name="ObtenerValoresDesdeString"></param>
        public Contacto(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }
    }
}
