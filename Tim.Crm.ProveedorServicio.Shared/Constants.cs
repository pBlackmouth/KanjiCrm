using System.Configuration;

namespace Tim.Crm.ProveedorServicio
{
    internal static class Constants
    {
        public static string ActualConnectionString = ConfigurationManager.AppSettings["ActualConnection"];
    }
}
