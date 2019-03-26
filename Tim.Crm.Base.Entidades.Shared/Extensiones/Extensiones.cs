using System;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensiones
    {

        public static bool IsGuid(this string str)
        {
            Guid guid;
            return Guid.TryParse(str, out guid);
        }

        

    }
}
