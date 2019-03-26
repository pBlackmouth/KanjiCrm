using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PruebasWeb
{
    public class Constantes
    {
        public static string ConfiguracionActual = ConfigurationManager.AppSettings.Get("ConfiguracionActual");        
    }
}