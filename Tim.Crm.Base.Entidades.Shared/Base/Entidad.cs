using System;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tim.Crm.Base.Entidades.Enumeraciones;

namespace Tim.Crm.Base.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Entidad
    {
        /// <summary>
        /// Serializa el objeto en una cadena XML
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el XML formateado.</param>
        /// <returns></returns>
        public String XML(Boolean ConFormato = false)
        {
            return Serializar.XML(this, ConFormato);
        }


        /// <summary>
        /// Serializa el objeto en una cadena JSON
        /// </summary>
        /// <param name="ConFormato">Especifica si devuelve el JSON formateado.</param>
        /// <returns></returns>
        public String JSON(Boolean ConFormato = false)
        {
            return Serializar.JSON(this, ConFormato);
        }

        ///Gris
        /// <summary>
        /// Deserializa la cadena serializada enviada, dependiendo del tipo de objeto enviado.
        /// </summary>
        /// <typeparam name="T">Tipo a deserializar.</typeparam>
        /// <param name="Tipo">tipo de serialización, xml, o json.</param>
        /// <param name="CadenaSerializada">String serializado.</param>
        /// <returns name="T">Tipo deserializado</returns>
        public static T DeserializarA<T>(eTipoObjeto Tipo, string CadenaSerializada)
        {
            T item;
            string cadSerializada = CadenaSerializada;
            try
            {

                /****************************************************************************************************
                //Se eliminó el uso de System.IO por que no es permitido en los Plugins o WA en Sandbox
                //Usando System.IO
                if (Tipo == eTipoObjeto.XML)
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));

                    CadenaSerializada = EliminarValoresNulos(CadenaSerializada); //.Replace(" xsi:nil=\"true\"", "");

                    TextReader textReader = new StringReader(CadenaSerializada);
                    item = (T)deserializer.Deserialize(textReader);
                    textReader.Close();
                }
                else
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    item = js.Deserialize<T>(CadenaSerializada);
                }
                ****************************************************************************************************/



                if (Tipo == eTipoObjeto.XML)
                {

                    XmlSerializer deserializer = new XmlSerializer(typeof(T));

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(cadSerializada);

                    cadSerializada = JsonConvert.SerializeObject(xmlDoc);

                }


                JavaScriptSerializer js = new JavaScriptSerializer();
                item = js.Deserialize<T>(cadSerializada);

            }
            catch (Exception ex)
            {
                return default(T);
            }

            return item;
        }

        ///Gris
        /// <summary>
        /// Substrae los valores nulos de la cadena serializada.
        /// </summary>
        /// <param name="cadena">Cadena string serializada xml.</param>
        /// <returns name="String">Cadena serializada xml sin valores nulos.</returns>
        private static string EliminarValoresNulos(String cadena)
        {
            String strRespuesta = cadena;
            StringBuilder sb = null;

            bool contieneNulos = strRespuesta.Contains("xsi:nil=\"true\"");

            while (contieneNulos)
            {
                sb = new StringBuilder();

                int indiceNull = strRespuesta.IndexOf("xsi:nil=\"true\"");
                string primeraMitad = strRespuesta.Substring(0, indiceNull);
                string segundaMitad = strRespuesta.Substring(indiceNull + 1);

                int indiceNodoNull = primeraMitad.LastIndexOf("<");
                primeraMitad = primeraMitad.Substring(0, indiceNodoNull);

                indiceNodoNull = segundaMitad.IndexOf("/");
                segundaMitad = segundaMitad.Substring(indiceNodoNull + 2);

                sb.Append(primeraMitad);
                sb.Append(segundaMitad);

                strRespuesta = sb.ToString();

                contieneNulos = strRespuesta.Contains("xsi:nil=\"true\"");
            }

            return strRespuesta;
        }





    }
}
