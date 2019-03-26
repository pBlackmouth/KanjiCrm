using System;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate string[] ColumnasDeMetodo();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    /// <returns></returns>
    public delegate object ConstructorEntidadCrm(object[] objects);
}
