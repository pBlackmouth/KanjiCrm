using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Serializar
    {

        /// <summary>
        /// Serializa el objeto en una cadena XML
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el XML formateado.</param>
        /// <returns></returns>
        public static String XML(Object entidad, Boolean ConFormato = false)
        {
            String strXML = null;
            XmlSerializer SerializerObj = new XmlSerializer(entidad.GetType());

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xmlSettings = null;

            xmlSettings = new XmlWriterSettings() { Indent = ConFormato, OmitXmlDeclaration = true };

            using (XmlWriter xmlw = XmlWriter.Create(sb, xmlSettings))
            {
                SerializerObj.Serialize(xmlw, entidad);
                strXML = sb.ToString();
            }

            strXML = strXML.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Trim();

            return strXML;
        }


        /// <summary>
        /// Serializa el objeto en una cadena JSON
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el JSON formateado.</param>
        /// <returns></returns>
        public static String JSON(Object entidad, Boolean ConFormato = false)
        {
            String strJSON = null;

            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            Newtonsoft.Json.Formatting formato;
            formato = ConFormato ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;

            strJSON = JsonConvert.SerializeObject(entidad, formato, jsSettings);

            return strJSON;
        }


        

    }
}
