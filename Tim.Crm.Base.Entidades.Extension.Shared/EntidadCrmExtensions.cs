using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tim.Crm.Base.Entidades;

namespace Tim.Crm.Base.Entidades.Extension
{
    public static class EntidadCrmExtensions
    {

        public static string JSON(this EntidadCrm entidadCrm)
        {
            EntidadCrmExtended eCrmMetadata = new EntidadCrmExtended();

            Type baseType = entidadCrm.GetType();
            foreach (PropertyInfo prop in baseType.GetProperties())
            {
                PropertyInfo propMeta = eCrmMetadata.GetType().GetProperty(prop.Name);
                propMeta.SetValue(eCrmMetadata, prop.GetValue(entidadCrm));
            }
            return JsonConvert.SerializeObject(eCrmMetadata);
        }

    }
}
