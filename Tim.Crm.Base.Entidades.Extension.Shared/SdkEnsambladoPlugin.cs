using System;
using System.Collections.Generic;
using System.Text;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Entidades.Extension
{
    public partial class SdkEnsambladoPlugin: EntidadCrmExtended
    {
        public SdkEnsambladoPlugin(String ObjectoSerializado, bool ObtenerValoresDesdeString = false)
            : base(ObjectoSerializado, ObtenerValoresDesdeString)
        {

        }
    }
}
