using System;
using System.Collections.Generic;
using Microsoft.Crm.Sdk.Messages;

namespace Tim.Crm.Base.Logica
{
    /// <summary>
    /// 
    /// </summary>
    public class ComparadorDia : IComparer<TimeInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(TimeInfo x, TimeInfo y)
        {
            int value = 0;

            if (x.Start == null)
            {
                if (y.Start == null)
                {
                    // If x is null and y is null, they're 
                    // equal.  
                    value = 0;
                }
                else
                {
                    // If x is null and y is not null, y 
                    // is greater.  
                    value = - 1;
                }
            }
            else
            {
                // If x is not null... 
                // 
                if (y.Start == null)                
                {
                    // ...and y is null, x is greater.
                    value = 1;
                }
                else
                {
                    value = x.Start.Value.CompareTo(y.Start.Value);
                }
            }


            return value;
        }
    }
}
